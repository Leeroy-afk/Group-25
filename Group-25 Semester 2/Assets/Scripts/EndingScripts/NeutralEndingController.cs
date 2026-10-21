using UnityEngine;
using UnityEngine.SceneManagement;

public class NeutralEndingController : MonoBehaviour
{
    public void OnBackClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

