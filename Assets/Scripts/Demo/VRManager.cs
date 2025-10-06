using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VRMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject volumePanel;

    public AudioSource globalAudio;
    public Slider volumeSlider;
    public Toggle muteToggle;

    private void Start()
    {
        ShowMainMenu();

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(UpdateVolume);
            volumeSlider.value = globalAudio.volume;
        }

        if (muteToggle != null)
        {
            muteToggle.onValueChanged.AddListener(ToggleMute);
            muteToggle.isOn = !globalAudio.mute;
        }
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        volumePanel.SetActive(false);
    }

    public void ShowVolumeSettings()
    {
        mainMenuPanel.SetActive(false);
        volumePanel.SetActive(true);
    }

    public void UpdateVolume(float value)
    {
        if (globalAudio != null)
            globalAudio.volume = value;
    }

    public void ToggleMute(bool isOn)
    {
        if (globalAudio != null)
            globalAudio.mute = !isOn;
    }

    public void ExitApp()
    {
        Debug.Log("Exit button pressed");
        mainMenuPanel.SetActive(false);
        volumePanel.SetActive(false);
    }
}
