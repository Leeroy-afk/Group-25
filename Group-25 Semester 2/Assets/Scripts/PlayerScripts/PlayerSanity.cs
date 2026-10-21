
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerSanity : MonoBehaviour, ISaveable
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
    public float MaxSanity => maxSanity;

    

    [Serializable]
    private class SanityData
    {
        public float currentSanity;
        public bool focusedLightOn;
        public bool isHiding;
    }

    private void Awake()
    {
        UpdateSanityUI();
    }

    private void Update()
    {
        if (focusedLightOn && !isHiding)
        {
            currentSanity -= focusDrainRate * Time.deltaTime;
            currentSanity = Mathf.Max(currentSanity, 0f);
        }

        UpdateSanityUI();
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
        focusedLightOn = false;
        isHiding = false;

        UpdateSanityUI();
    }

    private void UpdateSanityUI()
    {
        if (sanityBar != null)
        {
            sanityBar.maxValue = maxSanity;
            sanityBar.value = currentSanity;
        }
    }

    public string CaptureState()
    {
        SanityData data = new SanityData();

        data.currentSanity = currentSanity;
        data.focusedLightOn = focusedLightOn;
        data.isHiding = isHiding;

        return JsonUtility.ToJson(data);
    }

    public void RestoreState(string state)
    {
        SanityData data = JsonUtility.FromJson<SanityData>(state);

        if (data == null)
            return;

        currentSanity = Mathf.Clamp(
            data.currentSanity, 0f, maxSanity
        );

        focusedLightOn = data.focusedLightOn;
        isHiding = data.isHiding;

        UpdateSanityUI();
    }

    public void RestoreSanity(float amount)
    {
        if (amount <= 0f)
            return;

        currentSanity = Mathf.Min(currentSanity + amount, maxSanity);
        UpdateSanityUI();
    }

}
