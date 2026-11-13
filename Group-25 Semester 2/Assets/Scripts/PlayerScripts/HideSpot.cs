using UnityEngine;

public class HideSpot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerHide hiding = other.GetComponent<PlayerHide>();

        if (hiding != null)
            hiding.SetCanHide(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerHide hiding = other.GetComponent<PlayerHide>();

        if (hiding != null)
            hiding.SetCanHide(false);
    }
}