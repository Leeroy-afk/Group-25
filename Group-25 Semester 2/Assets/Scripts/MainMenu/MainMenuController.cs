using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Animator cameraAnimator;
    private bool isOptionsOpen = false; 
   public void OnPlayClick()
    {
        SceneManager.LoadScene("InsideHostpitalSandBox");
    }

    public void OnOptionsClick()
    {
        isOptionsOpen = true;
        DoCameraPan();
    }

    public void OnBackClick()
    {
        isOptionsOpen = false;
        DoCameraPan();
    }

    private void DoCameraPan()
    {
        if (cameraAnimator != null)
        {
            cameraAnimator.SetBool("IsOpen", isOptionsOpen);
        }
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}
