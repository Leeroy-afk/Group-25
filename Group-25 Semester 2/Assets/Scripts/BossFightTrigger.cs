using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    [Header("Encounter")]
    public GameObject encounterZone4;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated || !other.CompareTag("Player"))
            return;

        activated = true;

        if (encounterZone4 != null)
        {
            encounterZone4.SetActive(true);
            Debug.Log("ENCOUNTER 4 ACTIVATED - BOSS FIGHT!");
        }
        else
        {
            Debug.LogError("Encounter Zone 4 is not assigned!");
        }

        GetComponent<Collider>().enabled = false;
    }
}