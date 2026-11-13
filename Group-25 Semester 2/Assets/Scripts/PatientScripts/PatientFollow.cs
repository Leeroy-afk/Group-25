using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using TMPro;

public class PatientAI : MonoBehaviour
{
    private GameObject destination;
    private NavMeshAgent agent;

    public AudioSource patientSteps;

    [Header("UI Prompt")]
    public TMP_Text promptText;


    private bool isFollowing = true;

    void Start()
    {
        destination = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        UpdatePrompt();
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

        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
        {
            ToggleFollowing();
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

        UpdatePrompt();
    }
    private void UpdatePrompt()
    {
        if (promptText == null)
            return;

        if (isFollowing)
        {
            promptText.text = "[Q] Ask to stop";
        }
        else
        {
            promptText.text = "[Q] Ask to follow";
        }
    }
}
