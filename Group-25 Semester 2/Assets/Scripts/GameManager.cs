using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject deathScreen;

    [Header("Player")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerSanity playerSanity;
    [SerializeField] private Flashlight flashlight;

    private void Start()
    {
        deathScreen.SetActive(false);
    }

    public void PlayerDied()
    {
        Debug.Log("GAME MANAGER: Showing death screen!");

        deathScreen.SetActive(true);

        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResetAndContinue()
    {
        Debug.Log("RESET BUTTON CLICKED!");

        // Reset player systems
        playerHealth.ResetHealth();
        playerSanity.ResetSanity();
        flashlight.ResetFlashlight();

        // Hide death screen
        deathScreen.SetActive(false);

        // Resume game
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Debug.Log("PLAYER FULLY RESET!");
    }
}