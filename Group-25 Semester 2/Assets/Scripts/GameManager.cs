using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string deathSceneName = "DeathScene";

    public void PlayerDied()
    {
        Debug.Log("PLAYER DIED: Loading Death Scene!");

        Time.timeScale = 1f;

        SceneManager.LoadScene(deathSceneName);
    }
}