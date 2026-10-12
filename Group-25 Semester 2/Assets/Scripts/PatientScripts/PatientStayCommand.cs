using UnityEngine;
using UnityEngine.AI;

public class PatientStayCommand : MonoBehaviour
{
    [SerializeField] private MonoBehaviour patientFollow;
    [SerializeField] private NavMeshAgent agent;

    private bool playerInSafeZone = false;
    private bool patientIsStaying = false;

    public void SetPlayerInSafeZone(bool value)
    {
        playerInSafeZone = value;

        Debug.Log("playerInSafeZone = " + value);
    }

    public void PatientCommand()
    {
        Debug.Log("PatientCommand reached!");
        Debug.Log("Player in safe zone = " + playerInSafeZone);

        if (!playerInSafeZone)
        {
            Debug.Log("Player is NOT in safe zone.");
            return;
        }

        if (patientIsStaying)
        {
            CommandToFollow();
        }
        else
        {
            CommandToStay();
        }
    }

    private void CommandToStay()
    {
        Debug.Log("COMMAND: PATIENT STAY");

        if (patientFollow != null)
            patientFollow.enabled = false;

        if (agent != null)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.ResetPath();
        }

        patientIsStaying = true;
    }

    private void CommandToFollow()
    {
        Debug.Log("COMMAND: PATIENT FOLLOW");

        patientIsStaying = false;

        if (agent != null)
            agent.isStopped = false;

        if (patientFollow != null)
            patientFollow.enabled = true;
    }
}
