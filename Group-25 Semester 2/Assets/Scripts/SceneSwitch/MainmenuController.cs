using UnityEngine;
using UnityEngine.SceneManagement;


public class MainmenuController : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("DreamworldGreybox");
    }
    public void OnQuitClick()
    {
        UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }
}
