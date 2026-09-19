using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void PlayerDied()
    {
        Debug.Log("PLAYER DIED : Returning to main menu");

        Time.timeScale = 1f;

        SceneManager.LoadScene("TitleScreen");
    }
}