using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GraveEasterEgg : MonoBehaviour
{
    [Header("Interaction")]
    public TextMeshProUGUI promptText;

    [Header("Flower")]
    public GameObject flowerPrefab;
    public Transform flowerSpawnPoint;

    private bool playerNearby = false;
    private bool respectPaid = false;

    private void Start()
    {
        if (promptText != null)
            promptText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!playerNearby || respectPaid)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.fKey.wasPressedThisFrame)
        {
            PayRespects();
        }
    }

    private void PayRespects()
    {
        respectPaid = true;

        if (flowerPrefab != null && flowerSpawnPoint != null)
        {
            Instantiate(
                flowerPrefab,
                flowerSpawnPoint.position,
                flowerSpawnPoint.rotation
            );
        }

        if (promptText != null)
            promptText.gameObject.SetActive(false);

        Debug.Log("Leeroy's sleep schedule has been honoured.");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the grave zone: " + other.name);

        if (!other.CompareTag("Player"))
        {
            Debug.Log("Object is not tagged Player!");
            return;
        }

        Debug.Log("PLAYER ENTERED GRAVE ZONE!");

        if (respectPaid)
            return;

        playerNearby = true;

        if (promptText != null)
        {
            promptText.text =
                "Here lies Leeroy's sleep schedule, press F to pay respect";

            promptText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerNearby = false;

        if (promptText != null)
            promptText.gameObject.SetActive(false);
    }
}