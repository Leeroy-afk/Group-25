using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PatientAI : MonoBehaviour
{
    private GameObject destination;// where the patient should stop, their final destination
    private NavMeshAgent agent;
    public AudioSource patientSteps;


    void Start()
    {
        destination = GameObject.FindGameObjectWithTag("Player"); // makes agent follow player
        agent = GetComponent<NavMeshAgent>();// // Allows script to access the navmesh on patien


    }

    private void Update()
    {  
        agent.SetDestination(destination.transform.position); // Makes patient go to players position
        if (agent)
        {
            if (agent.velocity.magnitude > 0)
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
        else
        {
            patientSteps.Stop();
        }
    }

    
}
