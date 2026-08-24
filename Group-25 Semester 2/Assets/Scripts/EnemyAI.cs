using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    [Header("Distance")]
    public float stopDistance = 5f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.stoppingDistance = stopDistance;
    }

    void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }
}
