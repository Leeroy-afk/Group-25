using UnityEngine;

public class SafeZoneStay : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PatientStay patientStay = other.GetComponent<PatientStay>();

            if (patientStay != null)
            {
                patientStay.SetPlayerInSafeZone(true);
                Debug.Log("Player entered safe zone.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PatientStay patientStay = other.GetComponent<PatientStay>();

            if (patientStay != null)
            {
                patientStay.SetPlayerInSafeZone(false);
                Debug.Log("Player left safe zone.");
            }
        }
    }
}