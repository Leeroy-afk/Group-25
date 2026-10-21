using UnityEngine;
using UnityEngine.SceneManagement;

public class GoodEndingController : MonoBehaviour
{
    public void OnBackClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}


