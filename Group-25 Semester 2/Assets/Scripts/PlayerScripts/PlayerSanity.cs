using UnityEngine;
using UnityEngine.UI;

public class PlayerSanity : MonoBehaviour
{
    [Header("Sanity")]
    [SerializeField] private float maxSanity = 100f;
    [SerializeField] private float currentSanity = 100f;
    [SerializeField] private float focusDrainRate = 10f;

    [Header("UI")]
    [SerializeField] private Slider sanityBar;

    [Header("Light Status")]
    public bool focusedLightOn = false;

    private bool isHiding = false;

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
       if (focusedLightOn && !isHiding)
        {
            currentSanity -= focusDrainRate * Time.deltaTime;
            currentSanity = Mathf.Max(currentSanity, 0f);
        }

        if (sanityBar != null)
        {
            sanityBar.value = currentSanity;
        }
    }

    public void SetFocusedlight(bool isOn)
    {
        focusedLightOn = isOn;

        Debug.Log("Focused Light State: " + focusedLightOn);
    }

    public void SetHiding(bool hiding)
    {
        isHiding = hiding;
    }
    public void ResetSanity()
    {
        currentSanity = maxSanity;

        if (sanityBar != null)
        {
            sanityBar.value = currentSanity;
        }

        focusedLightOn = false;
        isHiding = false;
    }
}