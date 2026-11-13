using UnityEngine;
using UnityEngine.SceneManagement;

public class GoodEndingController : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void OnBackClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}


