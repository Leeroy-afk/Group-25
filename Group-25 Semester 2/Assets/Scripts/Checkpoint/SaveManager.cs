using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    [System.Serializable]
    private class SaveEntry
    {
        public string entityId;
        public string componentType;
        public string state;
    }

    [System.Serializable]
    private class SaveData
    {
        public string sceneName;
        public List<SaveEntry> entries = new List<SaveEntry>();
    }

    private SaveData checkpointData;
    private bool restoring;
    private bool pendingRestore;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        // Use the starting game state if no checkpoint has been activated.
        if (checkpointData == null)
        {
            CaptureSnapshot();
            Debug.Log("Starting state stored in memory.");
        }
    }

    public void SaveCheckpoint()
    {
        if (restoring)
            return;

        CaptureSnapshot();
        Debug.Log("Checkpoint saved in memory!");
    }

    private void CaptureSnapshot()
    {
        SaveData data = new SaveData
        {
            sceneName = SceneManager.GetActiveScene().name
        };

        MonoBehaviour[] behaviours =
            FindObjectsOfType<MonoBehaviour>(true);

        foreach (MonoBehaviour behaviour in behaviours)
        {
            if (!(behaviour is ISaveable saveable))
                continue;

            SaveableEntity entity =
                behaviour.GetComponent<SaveableEntity>();

            if (entity == null || string.IsNullOrEmpty(entity.UniqueID))
            {
                Debug.LogWarning(
                    $"Skipping {behaviour.name}: missing SaveableEntity ID."
                );
                continue;
            }

            data.entries.Add(new SaveEntry
            {
                entityId = entity.UniqueID,
                componentType = behaviour.GetType().FullName,
                state = saveable.CaptureState()
            });
        }

        checkpointData = data;
    }

    public void RespawnFromCheckpoint()
    {
        if (restoring)
            return;

        if (checkpointData == null)
        {
            CaptureSnapshot();
        }

        restoring = true;
        pendingRestore = true;

        Time.timeScale = 1f;
        SceneManager.LoadScene(checkpointData.sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (pendingRestore)
            StartCoroutine(RestoreAfterSceneLoad());
    }

    private IEnumerator RestoreAfterSceneLoad()
    {
        yield return new WaitForEndOfFrame();

        if (checkpointData != null)
        {
            MonoBehaviour[] behaviours =
                FindObjectsOfType<MonoBehaviour>(true);

            foreach (SaveEntry entry in checkpointData.entries)
            {
                foreach (MonoBehaviour behaviour in behaviours)
                {
                    if (!(behaviour is ISaveable saveable))
                        continue;

                    SaveableEntity entity =
                        behaviour.GetComponent<SaveableEntity>();

                    if (entity == null)
                        continue;

                    if (entity.UniqueID != entry.entityId)
                        continue;

                    if (behaviour.GetType().FullName != entry.componentType)
                        continue;

                    saveable.RestoreState(entry.state);
                    break;
                }
            }
        }

        yield return null;

        pendingRestore = false;
        restoring = false;

        Debug.Log("Checkpoint restored from memory.");
    }

    private void OnDestroy()
    {
        if (Instance != this)
            return;

        SceneManager.sceneLoaded -= OnSceneLoaded;
        Instance = null;
    }
}