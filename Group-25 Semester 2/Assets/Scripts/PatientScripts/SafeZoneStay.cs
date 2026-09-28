using UnityEngine;

public class SafeZoneStay : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER ENTER: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER ENTERED LIGHT ZONE!");

            PatientStayCommand command = GetComponent<PatientStayCommand>();

            if (command != null)
            {
                command.SetPlayerInSafeZone(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("TRIGGER EXIT: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER LEFT LIGHT ZONE!");

            PatientStayCommand command = GetComponent<PatientStayCommand>();

            if (command != null)
            {
                command.SetPlayerInSafeZone(false);
            }
        }
    }
}