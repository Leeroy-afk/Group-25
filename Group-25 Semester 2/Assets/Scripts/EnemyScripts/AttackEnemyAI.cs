using UnityEngine;
using UnityEngine.AI;

public class AttackEnemyAI : MonoBehaviour
{
    public Transform player;

    [Header("Attack")]
    public float attackDistance = 2f;
    public float attackCooldown = 1f;
    public float damage = 10f;

    [Header("Light")]
    public float lightEscapeTime = 2f;

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
        if (player == null)
            return;

        // If the enemy is currently escaping light,
        // don't chase the player.
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

        if (attackTimer <= 0)
        {
            playerHealth.TakeDamage(damage);

            attackTimer = attackCooldown;
        }
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

            Debug.Log("Enemy is escaping the light!");
        }
    }
}