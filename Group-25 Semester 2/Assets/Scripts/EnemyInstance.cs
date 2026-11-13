using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class AITriggerSequence : MonoBehaviour
{
    [Header("Dialogue Reference")]
    public DialogueTrigger dialogueTrigger;

    [Tooltip("How many conversations must be completed before the AI can appear.")]
    public int requiredConversationCount = 1;

    [Header("Enemy Character")]
    public GameObject aiCharacter;
    private NavMeshAgent agent;

    [Header("Movement Path")]
    public List<Transform> waypoints;

    [Header("UI Prompt")]
    public GameObject hidePrompt;

    private int currentWaypoint = 0;
    private bool sequenceStarted = false;

    private void Start()
    {
        // Hide the AI when the scene starts
        aiCharacter.SetActive(false);
        hidePrompt.SetActive(false);

        agent = aiCharacter.GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (sequenceStarted)
            return;

        // Check whether the required dialogue is finished
        if (dialogueTrigger.conversationCount < requiredConversationCount)
            return;

        // Start the AI sequence
        sequenceStarted = true;

        aiCharacter.SetActive(true);
        hidePrompt.SetActive(true);

        currentWaypoint = 0;

        agent.isStopped = false;

        MoveToNextWaypoint();
    }

    private void Update()
    {
        if (!sequenceStarted || !aiCharacter.activeSelf)
            return;

        // Check whether the AI has reached its waypoint
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (currentWaypoint < waypoints.Count - 1)
            {
                currentWaypoint++;
                MoveToNextWaypoint();
            }
            else
            {
                // AI has reached the end of the path
                HideEnemy();
            }
        }
    }

    private void MoveToNextWaypoint()
    {
        if (currentWaypoint < waypoints.Count)
        {
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    private void HideEnemy()
    {
        agent.isStopped = true;

        aiCharacter.SetActive(false);
        hidePrompt.SetActive(false);

        sequenceStarted = false;
    }
}