using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void PlayerDied()
    {
        Debug.Log("PLAYER DIED: Respawning at checkpoint!");

        // Make sure gameplay is not paused.
        Time.timeScale = 1f;

        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.RespawnFromCheckpoint();
        }
        else
        {
            Debug.LogError("SaveManager was not found!");
        }
    }
}