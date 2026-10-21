using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Image flashImage;
    [SerializeField] private float flashDuration = 0.3f;
    [SerializeField] private float maximumAlpha = 0.35f;

    private Coroutine flashcouritine;

    private void Awake()
    {
        if (flashImage != null)
        {
            Color color = flashImage.color;
            color.a = 0f;
            flashImage.color = color;
        }
    }

    public void Flash()
    {
        if (flashImage == null) 
            return;

        if (flashcouritine != null)
            StopCoroutine(flashcouritine);

        flashcouritine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        float elapsed = 0f;

        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;

            float alpha = Mathf.Lerp(
                maximumAlpha, 0f, elapsed / flashDuration
                );

            Color color = flashImage.color;
            color.a = alpha;
            flashImage.color = color;

            yield return null;
        }
    }

}