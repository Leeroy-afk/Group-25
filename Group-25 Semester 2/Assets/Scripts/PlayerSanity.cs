using UnityEngine;
using UnityEngine.UI;

public class PlayerSanity : MonoBehaviour
{
    [Header("Sanity")]
    [SerializeField] private float maxSanity = 100f;
    [SerializeField] private float currentSanity = 100f;
    [SerializeField] private float drainRate = 10f;

    [Header("UI")]
    [SerializeField] private Slider sanityBar;

    [Header("Light Status")]
    public bool flashlightOn = false;

    private int lightZoneCount = 0;

    public float CurrentSanity => currentSanity;

    private void Awake()
    {
        currentSanity = maxSanity;

        if (sanityBar != null)
        {
            sanityBar.maxValue = maxSanity;
            sanityBar.value = currentSanity;
        }
    }

    private void Update()
    {
        bool isSafe = flashlightOn || lightZoneCount > 0;

        if (!isSafe)
        {
            currentSanity -= drainRate * Time.deltaTime;
            currentSanity = Mathf.Max(currentSanity, 0f);
        }

        if (sanityBar != null)
        {
            sanityBar.value = currentSanity;
        }
    }

    public void SetFlashlight(bool isOn)
    {
        flashlightOn = isOn;
    }

    public void EnterLight()
    {
        lightZoneCount++;
    }

    public void ExitLight()
    {
        lightZoneCount = Mathf.Max(0, lightZoneCount - 1);
    }
    public void ResetSanity()
    {
        currentSanity = maxSanity;

        if (sanityBar != null)
        {
            sanityBar.value = currentSanity;
        }

        SetFlashlight(false);
    }
}