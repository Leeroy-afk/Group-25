using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PatientAI : MonoBehaviour
{
    private GameObject destination;
    private NavMeshAgent agent;

    public AudioSource patientSteps;

    private bool isFollowing = true;

    void Start()
    {
        destination = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (agent == null || destination == null)
            return;

        if (isFollowing)
        {
            agent.SetDestination(destination.transform.position);
        }

        if (agent.velocity.magnitude > 0.1f)
        {
            if (!patientSteps.isPlaying)
            {
                patientSteps.Play();
            }
        }
        else
        {
            patientSteps.Stop();
        }
    }
    public void ToggleFollowing()
    {
        isFollowing = !isFollowing;

        if (isFollowing)
        {
            agent.isStopped = false;
            Debug.Log("Patient is following the player");
        }
        else
        {
            agent.isStopped = true;
            agent.ResetPath();
            patientSteps.Stop();

            Debug.Log("Patient is staying in place");
        }
    }
}
