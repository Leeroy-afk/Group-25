using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{
    [Header("Supply Room Lights")]
    [SerializeField] private Light spLight1;
    [SerializeField] private Light spLight2;
    [SerializeField] private Light spLight3;
    [SerializeField] private Light spLight4;

    public int flickerCount = 3;
    public float flickerSpeed = 0.15f;

    private bool hasTriggered = false;
    private Coroutine flickerCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Only flicker on the first entry
        if (!hasTriggered)
        {
            hasTriggered = true;
            flickerCoroutine = StartCoroutine(FlickerLight());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        Debug.Log("Player has entered");

        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
            flickerCoroutine = null;
        }

        // Turn light back on when player leaves
        spLight1.enabled = true;
        spLight2.enabled = true;
        spLight3.enabled = true;
        spLight4.enabled = true;
    }
    IEnumerator FlickerLight()
    {
        for (int i = 0; i < 3; i++)
        {
            spLight1.enabled = false;
            yield return new WaitForSeconds(flickerSpeed);
            spLight2.enabled = false;
            yield return new WaitForSeconds(flickerSpeed);
            spLight3.enabled = false;
            yield return new WaitForSeconds(flickerSpeed);
            spLight4.enabled = false;
            yield return new WaitForSeconds(flickerSpeed);

           spLight1.enabled = true;
            yield return new WaitForSeconds(flickerSpeed);
            spLight2.enabled = true;
            yield return new WaitForSeconds(flickerSpeed);
            spLight3.enabled = true;
            yield return new WaitForSeconds(flickerSpeed);
            spLight4.enabled = true;
            yield return new WaitForSeconds(flickerSpeed);
        }

        // Switch off after 3 flickers
        spLight1.enabled = false;
        spLight2.enabled = false;
        spLight3.enabled = false;
        spLight4.enabled = false;

        flickerCoroutine = null;
    }





}
