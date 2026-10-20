using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

   public AudioSource audioManagerObject;

    public void Awake()
    {
        if (instance == null )
        { 
        instance = this;
        }
    }

    public void PlaySFX(AudioClip audioClip, Transform spawnTransform, float volume = 1f)
    {
        AudioSource audioSource = Instantiate(audioManagerObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

   
}
