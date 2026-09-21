using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System.Runtime.CompilerServices;
using Assets.Scripts;
using UnityEngine.Events;

public class PatientStayCommand : MonoBehaviour, IInteractable1
{

    [SerializeField] private MonoBehaviour patientFollow;
    [SerializeField] private NavMeshAgent agent;


    private bool playerInSafeZone = false;
    private bool patientIsStaying = false;

    [SerializeField] private UnityEvent _onInteract;

    UnityEvent IInteractable1.onInteract { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerInSafeZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInSafeZone = false;
    }

    public void PatientCommand()
    {
        Debug.Log("PatientCommand() called");
        Debug.Log("Player in safe zone: " + playerInSafeZone);
        Debug.Log("Patient staying: " + patientIsStaying);

        if (!playerInSafeZone)
        {
            Debug.Log("Player is NOT in safe zone!");
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
        if (patientFollow != null) patientFollow.enabled = false;

        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        agent.ResetPath();

        patientIsStaying = true;
        Debug.Log("Patient stops following");
    }

    private void CommandToFollow()
    {
        patientIsStaying = false;
        agent.isStopped = false;

        if (patientFollow != null) patientFollow.enabled = true;
        Debug.Log("Patient starys following");
    }

    public void Interact() => _onInteract.Invoke();

    void IInteractable1.Interact()
    {
        throw new System.NotImplementedException();
    }
    public void SetPlayerInSafeZone(bool value)
    {
        playerInSafeZone = value;

        Debug.Log("playerInSafeZone = " + value);
    }
}
