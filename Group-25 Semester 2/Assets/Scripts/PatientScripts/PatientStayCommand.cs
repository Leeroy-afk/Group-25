using UnityEngine;
using UnityEngine.AI;

public class PatientStayCommand : MonoBehaviour
{
    [SerializeField] private MonoBehaviour patientFollow;
    [SerializeField] private NavMeshAgent agent;
    private bool patientIsStaying = false;

    public void PatientCommand()
    {
        Debug.Log("PatientCommand reached!");

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
