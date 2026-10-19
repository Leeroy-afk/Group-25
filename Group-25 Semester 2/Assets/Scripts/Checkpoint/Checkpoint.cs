using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (activated)
            return;

        if (SaveManager.Instance == null)
        {
            Debug.LogError("Checkpoint: SaveManager not found!");
            return;
        }

        SaveManager.Instance.SaveCheckpoint();

        activated = true;
        Debug.Log("CHECKPOINT ACTIVATED: " + transform.position);
    }
}
