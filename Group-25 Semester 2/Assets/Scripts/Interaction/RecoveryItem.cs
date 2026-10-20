using UnityEngine;

public class RecoveryItem : MonoBehaviour
{
    [Header("Recovery Amounts")]
    [SerializeField] private float healthRestore = 25f;
    [SerializeField] private float sanityRestore = 25f;

    [Header("Pickup Settings")]
    [SerializeField] private bool destroyOnPickup = true;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected || !other.CompareTag("Player"))
            return;

        PlayerHealth health = other.GetComponent<PlayerHealth>();
        PlayerSanity sanity = other.GetComponent<PlayerSanity>();

        if (health == null || sanity == null)
        {
            Debug.LogWarning(
                "Recovery item: PlayerHealth or PlayerSanity not found!"
            );
            return;
        }

        health.RestoreHealth(healthRestore);
        sanity.RestoreSanity(sanityRestore);

        collected = true;

        Debug.Log(
            $"Recovery item collected! +{healthRestore} Health, " +
            $"+{sanityRestore} Sanity"
        );

        if (destroyOnPickup)
        {
            Destroy(gameObject);
        }
    }
}