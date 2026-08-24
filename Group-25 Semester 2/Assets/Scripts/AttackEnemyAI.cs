using UnityEngine;
using UnityEngine.AI;

public class AttackEnemyAI : MonoBehaviour
{
    public Transform player;

    public float attackDistance = 2f;
    public float attackCooldown = 1f;
    public float damage = 10f;

    private NavMeshAgent agent;
    private PlayerHealth playerHealth;
    private float attackTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

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
}