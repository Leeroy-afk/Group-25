using UnityEngine;
using UnityEngine.AI;
using System.Collections;

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

    [Header("Cooldown")]
    public float encounterCooldown = 5f;

    private bool completed = false;
    private bool playerInside = false;

    private NavMeshAgent agent;

    private float originalSpeed;
    private float originalDamage;
    private float originalCooldown;
    private bool originalLightImmunity;

    [Header("Encounter Audio")]
    public AudioSource audioSource;
    public AudioClip encounterSFX;
    public AudioClip encounterMusic;

    public bool playSFXOnEnter = true;
    public bool loopMusicDuringEncounter = true;

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

        if (audioSource != null && playSFXOnEnter && encounterSFX != null)
        {
            audioSource.PlayOneShot(encounterSFX);
        }

        if (audioSource != null && loopMusicDuringEncounter  && encounterMusic != null)
        {
            audioSource.clip = encounterMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

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

        encounterManager.ReleaseEncounter(this);

        StartCoroutine(ReturnToNaturalBehaviour());

        Debug.Log("Enemy encounter completed: " + gameObject.name);
    }

    private IEnumerator ReturnToNaturalBehaviour()
    {
        enemyAI.SetAIActive(false);

        yield return new WaitForSeconds(encounterCooldown);

        if (agent != null)
            agent.speed = originalSpeed;

        enemyAI.damage = originalDamage;
        enemyAI.attackCooldown = originalCooldown;
        enemyAI.SetLightImmunity(originalLightImmunity);

        enemyAI.StopPatrol();
        enemyAI.SetAIActive(true);

        Debug.Log("Enemy returned to natural follow-and-attack behaviour.");
    }
}