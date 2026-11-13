using UnityEngine;
using UnityEngine.UI;
public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider mouseSensitivitySlider;

    [SerializeField] private GameObject controlsPanel;
   

    void Start()
    {
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(false);
        }

        // saving defult values 
        float defaultVolume = 1f;
        float defaultSensitivity = 2f; 

        float currentVolume = PlayerPrefs.GetFloat("MasterVolume", defaultVolume);
        float currentSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", defaultSensitivity);

        AudioListener.volume = currentVolume;

        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = currentVolume;
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        }

        if (mouseSensitivitySlider != null)
        {
            mouseSensitivitySlider.value = currentSensitivity;
            mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
        }
    }
    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetMouseSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
        PlayerPrefs.Save();
    }

    public void OpenControlsPanel()
    {
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(true);
        }
    }

    public void CloseControlsOverlay()
    {
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(false);
        }
    }
}