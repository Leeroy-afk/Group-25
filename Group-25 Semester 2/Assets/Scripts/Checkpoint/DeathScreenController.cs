using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string mainMenuSceneName = "Mainmenu";

    private void Start()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReturnToCheckpoint()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.RespawnFromCheckpoint();
        }
        else
        {
            Debug.LogError("SaveManager was not found!");
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuSceneName);
    }
}