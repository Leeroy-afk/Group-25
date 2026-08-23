using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class PatientFollow : MonoBehaviour
{
    private GameObject destination;// where the patient should stop, their final destination
    private NavMeshAgent agent; 
    
    void Start()
    {
        destination = GameObject.FindGameObjectWithTag("Player"); // makes agent follow player
        agent = GetComponent<NavMeshAgent>(); // // Allows script to access the navmesh on patient
    }

    private void Update()
    {
        agent.SetDestination(destination.transform.position); // Makes patient go to players position
    }


}
