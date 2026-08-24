using UnityEngine;

public class LightZone : MonoBehaviour
{
    [SerializeField] private float enemyPushDistance = 8f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("LightZone detected: " + other.name);

        PlayerSanity sanity = other.GetComponent<PlayerSanity>();

        if (sanity != null)
        {
            sanity.EnterLight();
        }

        AttackEnemyAI enemy = other.GetComponentInParent<AttackEnemyAI>();

        if (enemy != null)
        {
            Debug.Log("ENEMY ENTERED LIGHT!");

            enemy.EscapeLight(
                transform.position,
                enemyPushDistance
            );
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("LightZone exit: " + other.name);

        PlayerSanity sanity = other.GetComponent<PlayerSanity>();

        if (sanity != null)
        {
            sanity.ExitLight();
        }
    }
}