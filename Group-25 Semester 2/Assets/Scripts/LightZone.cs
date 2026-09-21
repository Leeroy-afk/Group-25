using UnityEngine;

public class LightZone : MonoBehaviour
{
    [SerializeField] private float enemyPushDistance = 8f;
    [SerializeField] private PatientStayCommand patientStayCommand;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("LightZone detected: " + other.name);

        PlayerSanity sanity = other.GetComponent<PlayerSanity>();

        

        if (other.CompareTag("Player"))
        {
            patientStayCommand.SetPlayerInSafeZone(true);
            Debug.Log("Player is now in the safe zone");
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

        if (other.CompareTag("Player"))
        {
            patientStayCommand.SetPlayerInSafeZone(false);
            Debug.Log("Player has left the safe zone");
        }
    }
}