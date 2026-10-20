using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour
{
    [SerializeField] private float minTimeBetweenStrikes = 5f;
    [SerializeField] private float maxTimeBetweenStrikes = 15f;
    [SerializeField] private float minFlashIntensity = 2f;
    [SerializeField] private float maxFlashIntensity = 5f;
    [SerializeField] private AudioClip[] lightningSounds; // Array of lightning sounds to choose from

    private Light lightningLight;

    private AudioSource audioSource; 

    void Start()
    {
        lightningLight = GetComponent<Light>(); 
        audioSource = GetComponent<AudioSource>();
        lightningLight.intensity = 0f;// starts the lightingLight off off
        StartCoroutine(LightningRoutine());
    }

    IEnumerator LightningRoutine()
    {
        while(true)
        {
            float waitTime = Random.Range(minTimeBetweenStrikes, maxTimeBetweenStrikes); // waits for random time between lightning strikes
            yield return new WaitForSeconds(waitTime);

            if (audioSource.clip != null && lightningSounds != null && lightningSounds.Length > 0)
            {
                AudioClip randomSound = lightningSounds[Random.Range(0, lightningSounds.Length)];
                if (randomSound != null)
                {
                    audioSource.PlayOneShot(randomSound);
                }
           
            }

            int burstCount = Random.Range(5, 10); // triggers the lightning flashes between the min and max

            for (int i = 0; i < burstCount; i++)
            {
                lightningLight.intensity = Random.Range(minFlashIntensity, maxFlashIntensity); // the flash is on
                yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
                lightningLight.intensity = Random.Range(0.2f, 1.5f); // teh flash flickers
                yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
            }

            lightningLight.intensity = 0f;// turns off the lighning once again
        }
    }
}
