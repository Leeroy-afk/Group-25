using UnityEngine;
using UnityEngine.UI;

public class VignetteEffect : MonoBehaviour
{
    [Header("Vignette")]
    [SerializeField] private Image vignetteImage;

    [Header("Player")]
    [SerializeField] private PlayerSanity playerSanity;
    [SerializeField] private PlayerHealth playerHealth;

    [Header("Sanity")]
    [SerializeField] private float maxSanityAlpha = 0.7f;

    [Header("Low Health Pulse")]
    [SerializeField] private float lowHealthThreshold = 0.3f;
    [SerializeField] private float pulseSpeed = 4f;
    [SerializeField] private float pulseStrength = 0.25f;

    private void Update()
    {
        if (vignetteImage == null)
            return;

        float sanityPercent =
            playerSanity.CurrentSanity / 100f;

        float sanityAlpha =
            (1f - sanityPercent) * maxSanityAlpha;

        float healthPercent =
            playerHealth.CurrentHealth / playerHealth.MaxHealth;

        float healthAlpha = 0f;

        if (healthPercent <= lowHealthThreshold)
        {
            float pulse =
                (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;

            healthAlpha = pulse * pulseStrength;
        }

        float finalAlpha =
            Mathf.Clamp01(sanityAlpha + healthAlpha);

        Color color = vignetteImage.color;
        color.a = finalAlpha;
        vignetteImage.color = color;
    }
}
