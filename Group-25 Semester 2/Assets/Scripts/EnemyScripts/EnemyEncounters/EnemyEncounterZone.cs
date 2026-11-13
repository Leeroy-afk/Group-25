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

    [Header("Enemy Spawn")]
    public bool SpawnEnemyOnEnter = false;
    public float spawnBehindDistance = 3f;

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

    [Header("Audio Fade")]
    public float musicFadeDuration = 2f;

    private Coroutine musicFadeCoroutine;

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

        if (SpawnEnemyOnEnter)
        {
            SpawnBehindPlayer(other.transform);
        }

        if (audioSource != null && playSFXOnEnter && encounterSFX != null)
        {
            audioSource.PlayOneShot(encounterSFX);
        }

        if (audioSource != null && loopMusicDuringEncounter  && encounterMusic != null)
        {
            audioSource.volume = 0.75f;
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
    private void SpawnBehindPlayer(Transform player)
    {
        Vector3 spawnPosition = player.position - player.forward * spawnBehindDistance;

        if (NavMesh.SamplePosition(spawnPosition, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            if (agent != null && agent.enabled && agent.isOnNavMesh)
            {
                agent.Warp(hit.position);
            }
            else
            {
                enemyAI.transform.position = hit.position;
            }

            Vector3 direction = player.position - hit.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.01f)
                enemyAI.transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            Debug.LogWarning("Could not find a NavMesh position behind the player!");
        }
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

        if (audioSource != null && loopMusicDuringEncounter)
        {
            if (musicFadeCoroutine != null)
                StopCoroutine(musicFadeCoroutine);

            musicFadeCoroutine = StartCoroutine(FadeOutMusic());
        }
    }
    private IEnumerator FadeOutMusic()
    {
        float startVolume = audioSource.volume;
        float elapsed = 0f;

        while (elapsed < musicFadeDuration)
        {
            elapsed += Time.deltaTime;

            audioSource.volume = Mathf.Lerp(
                startVolume,
                0f,
                elapsed / musicFadeDuration
            );

            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
        audioSource.clip = null;
        audioSource.loop = false;
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