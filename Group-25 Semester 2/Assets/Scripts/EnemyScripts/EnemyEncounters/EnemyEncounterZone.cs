using UnityEngine;
using UnityEngine.AI;

public class EnemyEncounterZone : MonoBehaviour
{
    [Header("References")]
    public EnemyEncounterManager encounterManager;
    public AttackEnemyAI enemyAI;

    [Header("Encounter Settings")]
    public bool lightImmunity = false;
    public bool startPatrolling = false;
    public float enemySpeed = 3.5f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;

    private bool completed = false;
    private bool playerInside = false;

    private NavMeshAgent agent;

    // Original enemy settings
    private float originalSpeed;
    private float originalDamage;
    private float originalCooldown;
    private bool originalLightImmunity;

    private void Start()
    {
        if (enemyAI == null)
        {
            Debug.LogError("Enemy AI is missing from " + gameObject.name);
            return;
        }

        agent = enemyAI.GetComponent<NavMeshAgent>();

        if (agent != null)
            originalSpeed = agent.speed;

        originalDamage = enemyAI.damage;
        originalCooldown = enemyAI.attackCooldown;
        originalLightImmunity = enemyAI.lightImmunity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (completed || playerInside)
            return;

        if (encounterManager == null || enemyAI == null)
            return;

        if (!encounterManager.TryActivateEncounter(this))
            return;

        playerInside = true;

        // Apply encounter settings
        if (agent != null)
            agent.speed = enemySpeed;

        enemyAI.damage = attackDamage;
        enemyAI.attackCooldown = attackCooldown;
        enemyAI.SetLightImmunity(lightImmunity);

        enemyAI.SetAIActive(true);

        if (startPatrolling)
            enemyAI.StartPatrol();
        else
            enemyAI.StopPatrol();

        Debug.Log("Enemy encounter started: " + gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") || !playerInside)
            return;

        playerInside = false;
        completed = true;

        // Deactivate enemy
        enemyAI.SetAIActive(false);

        // Restore original settings
        if (agent != null)
            agent.speed = originalSpeed;

        enemyAI.damage = originalDamage;
        enemyAI.attackCooldown = originalCooldown;
        enemyAI.SetLightImmunity(originalLightImmunity);

        encounterManager.ReleaseEncounter(this);

        Debug.Log("Enemy encounter completed: " + gameObject.name);
    }
}