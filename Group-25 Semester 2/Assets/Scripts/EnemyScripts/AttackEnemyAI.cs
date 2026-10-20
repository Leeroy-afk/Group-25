
using UnityEngine;
using UnityEngine.AI;
using System;

public class AttackEnemyAI : MonoBehaviour, ISaveable
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

    [Serializable]
    private class EnemyData
    {
        public Vector3 position;
        public Quaternion rotation;

        public bool isPatrolling;
        public float patrolTimer;

        public bool escapingLight;
        public float escapeTimer;

        public float attackTimer;

        public bool hasDestination;
        public Vector3 destination;
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    private void Update()
    {
        if (agent == null || !agent.enabled || !agent.isOnNavMesh)
            return;

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

    private void Attack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            attackTimer = attackCooldown;
        }
    }

    private void Patrol()
    {
        patrolTimer -= Time.deltaTime;

        if (patrolTimer <= 0f)
        {
            Vector3 randomDirection =
                UnityEngine.Random.insideUnitSphere * patrolRadius;

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
        escapingLight = false;

        if (agent != null && agent.enabled && agent.isOnNavMesh)
        {
            agent.isStopped = false;
            agent.ResetPath();
        }

        patrolTimer = 0f;

        Debug.Log("ENEMY STARTED PATROLLING");
    }

    public void StopPatrol()
    {
        isPatrolling = false;

        if (agent != null && agent.enabled && agent.isOnNavMesh)
        {
            agent.isStopped = false;
        }

        Debug.Log("Found you!");
    }

    public void EscapeLight(Vector3 lightPosition, float distance)
    {
        if (agent == null || !agent.enabled || !agent.isOnNavMesh)
            return;

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

    public string CaptureState()
    {
        EnemyData data = new EnemyData();

        data.position = transform.position;
        data.rotation = transform.rotation;

        data.isPatrolling = isPatrolling;
        data.patrolTimer = patrolTimer;

        data.escapingLight = escapingLight;
        data.escapeTimer = escapeTimer;

        data.attackTimer = attackTimer;

        if (agent != null &&
            agent.enabled &&
            agent.isOnNavMesh &&
            agent.hasPath)
        {
            data.hasDestination = true;
            data.destination = agent.destination;
        }

        return JsonUtility.ToJson(data);
    }

    public void RestoreState(string state)
    {
        EnemyData data = JsonUtility.FromJson<EnemyData>(state);

        if (data == null)
            return;

        // Restore the enemy's position and rotation.
        bool warped = false;

        if (agent != null && agent.enabled && agent.isOnNavMesh)
        {
            warped = agent.Warp(data.position);
        }

        if (!warped)
        {
            transform.SetPositionAndRotation(
                data.position,
                data.rotation
            );
        }
        else
        {
            transform.rotation = data.rotation;
        }

        isPatrolling = data.isPatrolling;
        patrolTimer = data.patrolTimer;

        escapingLight = data.escapingLight;
        escapeTimer = data.escapeTimer;

        attackTimer = data.attackTimer;

        if (agent != null && agent.enabled && agent.isOnNavMesh)
        {
            agent.ResetPath();
            agent.isStopped = false;

            if (data.hasDestination)
            {
                agent.SetDestination(data.destination);
            }
        }
    }
}
