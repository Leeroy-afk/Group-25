using UnityEngine;
using UnityEngine.AI;

public class AttackEnemyAI : MonoBehaviour
{
    public Transform player;

    [Header("Attack")]
    public float attackDistance = 2f;
    public float attackCooldown = 0.5f;
    public float damage = 10f;

    [Header("Sanity")]
    public PlayerSanity playerSanity;

    [Header("Light")]
    public float lightEscapeTime = 2f;

    [Header("Patrol")]
    [SerializeField] private float patrolRadius = 10f;
    [SerializeField] private float patrolChangeTime = 2f;

    private bool isPatrolling = false;
    private float patrolTimer;

    private NavMeshAgent agent;
    private PlayerHealth playerHealth;

    private float attackTimer;

    private bool escapingLight = false;
    private float escapeTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (isPatrolling)
        {
            Patrol();
            return;

        }

        if (player == null)
            return;

        if (escapingLight)
        {
            escapeTimer -= Time.deltaTime;

            if (escapeTimer <= 0f)
            {
                escapingLight = false;
            }

            return;
        }

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

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
            playerHealth.TakeDamage(damage);

            attackTimer = attackCooldown;
        }
    }

    void Patrol()
    {
        patrolTimer -= Time.deltaTime;

        if (patrolTimer <= 0f)
        {
            Vector3 randomDirection =
                Random.insideUnitSphere * patrolRadius;

            randomDirection.y = 0f;

            Vector3 randomPosition =
                transform.position + randomDirection;

            if (NavMesh.SamplePosition(
                randomPosition,
                out NavMeshHit hit,
                patrolRadius,
                NavMesh.AllAreas))
            {
                agent.isStopped = false;
                agent.SetDestination(hit.position);

                Debug.Log("ENEMY PATROLLING TO: " + hit.position);
            }
            else
            {
                Debug.Log("COULD NOT FIND PATROL POSITION");
            }

            patrolTimer = patrolChangeTime;
        }
    }

    public void StartPatrol()
    {
        isPatrolling = true;

        agent.isStopped = false;
        patrolTimer = 0f;

        Debug.Log("ENEMY STARTED PATROLLING");
    }

    public void StopPatrol()
    {
        isPatrolling = false;

        agent.isStopped = false;

        Debug.Log("Found you!");
    }

    public void EscapeLight(Vector3 lightPosition, float distance)
    {
        Vector3 directionAway =
            transform.position - lightPosition;

        directionAway.y = 0f;

        if (directionAway.sqrMagnitude < 0.01f)
        {
            directionAway = -transform.forward;
        }

        directionAway.Normalize();

        Vector3 escapePosition =
            transform.position + directionAway * distance;

        if (NavMesh.SamplePosition(
            escapePosition,
            out NavMeshHit hit,
            distance,
            NavMesh.AllAreas))
        {
            escapingLight = true;
            escapeTimer = lightEscapeTime;

            agent.isStopped = false;
            agent.SetDestination(hit.position);

            Debug.Log("Big enemy is escaping the light!");
        }
    }
}