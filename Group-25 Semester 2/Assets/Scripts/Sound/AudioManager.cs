using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource heartBeat;//HidingSound
    [SerializeField] private AudioSource flashlightOn;
    [SerializeField] private AudioSource flashlightOff;
    [SerializeField] private AudioSource footsteps;//Foot step sounds
    [SerializeField] private AudioSource heavyBreathing; //When Sanity is Low
    [SerializeField] private AudioSource scream;//Death sound
    [SerializeField] private AudioSource damageAudioSource;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioSource enemyAttackAudioSource;
    [SerializeField] private AudioClip enemyAttackSound;


    [Header("Player Refrences")]
    [SerializeField] private Light normalLight;
    [SerializeField] private Light focusLight;
    [SerializeField] private CharacterController playerController;
    [SerializeField] private PlayerHealth playerHealth;

    [Header("Footstep Settings")]
    [SerializeField] private float normalFootstepVolume = 1f;
    [SerializeField] private float crouchFootstepVolume = 0.25f;

    [Header("Sanity Sound Settings")]
    [SerializeField] private PlayerSanity playerSanity;
    [SerializeField] private float lowSanityThreshold = 30f;

    [Header("Interaction Sounds")]
    [SerializeField] private AudioSource interactionAudioSource;
    [SerializeField] private AudioClip[] interactionSounds;

    private bool wasHiding = false;
    private bool wasFocusedLightOn = false;
    private bool deathSoundPlayed = false;
    private float previousHealth;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        wasFocusedLightOn = !normalLight.enabled;
        if (playerHealth != null)
            previousHealth = playerHealth.CurrentHealth;
    }
    private void Update()
    {
        PlayHidingSound();
        PlayFlashLightSound();
        PlayFootSteps();
        FootStepVolume();
        PlayLowSanitySound();
        PlayDeathScream();
        PlayDamageSound();
        PlayEnemyAttackSound();
    }

    private void PlayHidingSound()
    {
        bool isHiding = !normalLight.enabled && !focusLight.enabled;

        if (isHiding && !wasHiding)
        {
            heartBeat.Play();
        }
        else if (!isHiding && wasHiding)
        {
            heartBeat.Stop();
        }

        wasHiding = isHiding;
    }

    private void PlayFlashLightSound()
    {
        bool isFocusedLightOn = focusLight.enabled;

        if (isFocusedLightOn && !wasFocusedLightOn)
        {
            flashlightOn.Play();
            Debug.Log("Flash On");
        }
        else if (!isFocusedLightOn && wasFocusedLightOn)
        {
            flashlightOff.Play();
            Debug.Log("Flash Off");
        }

        wasFocusedLightOn = isFocusedLightOn;
    }
    private void PlayFootSteps()
    {
        bool isWalking = playerController.isGrounded && playerController.velocity.magnitude > 0.1f;

        if (isWalking && !footsteps.isPlaying)
        {
            footsteps.Play();
        }
        else if (!isWalking && footsteps.isPlaying)
        {
            footsteps.Stop();
        }
    }
    private void FootStepVolume() //HAVE TO ADD ISCROUCHING 
    {
        if (playerController)
        {
            footsteps.volume = normalFootstepVolume;
        }
        else
        {
            footsteps.volume = crouchFootstepVolume;
        }
    }

    private void PlayLowSanitySound()
    {
        if (playerSanity.CurrentSanity <= lowSanityThreshold)
        {
            if (!heavyBreathing.isPlaying)
            {
                heavyBreathing.Play();
            }
        }
        else
        {
            if (heavyBreathing.isPlaying)
            {
                heavyBreathing.Stop();
            }
        }
        Debug.Log("Player is losing sanity");
    }

    private void PlayDeathScream()
    {
        if (playerHealth == null || scream == null)
            return;

        if (playerHealth.CurrentHealth <= 0f && !deathSoundPlayed)
        {
            scream.Play();
            deathSoundPlayed = true;
            Debug.Log("Player is dying");
        }
      
    }
    public void PlayInteractionSound(AudioClip sound)
    {
        if (interactionAudioSource == null || sound == null)
            return;

        interactionAudioSource.PlayOneShot(sound);
    }

    private void PlayDamageSound()
    {
        if (playerHealth == null || damageAudioSource == null || damageSound == null)
            return;

        float currentHealth = playerHealth.CurrentHealth;

        if (currentHealth < previousHealth)
        {
            damageAudioSource.PlayOneShot(damageSound);
        }

        previousHealth = currentHealth;
    }
    private void PlayEnemyAttackSound()
    {
        if (playerHealth == null || enemyAttackAudioSource == null || enemyAttackSound == null)
            return;

        float currentHealth = playerHealth.CurrentHealth;

        if (currentHealth < previousHealth)
        {
            enemyAttackAudioSource.PlayOneShot(enemyAttackSound);
        }

        previousHealth = currentHealth;
    }

}


