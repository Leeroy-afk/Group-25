using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    [Header("Normal Behaviour")]
    public float stopDistance = 5f;

    [Header("Sanity Behaviour")]
    public float attackDistance = 2f;
    public float attackCooldown = 3f;

    private NavMeshAgent agent;
    private PlayerSanity playerSanity;
    private PlayerHealth playerHealth;

    private float attackTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        playerSanity = player.GetComponent<PlayerSanity>();
        playerHealth = player.GetComponent<PlayerHealth>();

        agent.stoppingDistance = stopDistance;
    }

    void Update()
    {
        if (player == null)
            return;

        if (playerSanity.CurrentSanity <= 0)
        {
            AggressiveBehaviour();
        }
        else
        {
            NormalBehaviour();
        }
    }

    void NormalBehaviour()
    {
        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        agent.stoppingDistance = stopDistance;

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

    void AggressiveBehaviour()
    {
        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        agent.stoppingDistance = attackDistance;

        if (distance > attackDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;

            Attack();
        }
    }

    void Attack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            playerHealth.TakeHalfHealth();

            attackTimer = attackCooldown;
        }
    }
    
}