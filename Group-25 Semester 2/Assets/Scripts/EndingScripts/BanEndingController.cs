using UnityEngine;
using UnityEngine.SceneManagement;

public class BanEndingController : MonoBehaviour
{
    public void OnBackClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}


