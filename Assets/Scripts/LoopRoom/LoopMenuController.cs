using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LoopMenuController : MonoBehaviour
{
    public CanvasTriggerZone loopcanvas;
    public GameDisplayManager displayManager;
    public GameObject ScoreCanvas;

    [Header("Menu Canvas")]
    public GameObject menuCanvas;

    [Header("Menu Pages")]
    public GameObject[] pages;
    public Button leftArrow;
    public Button rightArrow;

    [Header("Input")]
    public InputActionReference menuToggleAction; // assign thumbstick press

    [Header("settings")]
    public Slider MasterVolumeSlider;
    public AudioMixer masterMixer;
    public Slider MusicSlider;
    public AudioSource MusicSource;

    public TMP_Dropdown timerDropdown;

    private int currentPage = 0;
    public bool isMenuOpen = false;

    private void OnEnable()
    {
        if (menuToggleAction != null)
        {
            menuToggleAction.action.performed += ToggleMenu;
            menuToggleAction.action.Enable();
        }

        if (leftArrow != null)
            leftArrow.onClick.AddListener(PrevPage);
        if (rightArrow != null)
            rightArrow.onClick.AddListener(NextPage);
    }

    private void OnDisable()
    {
        if (menuToggleAction != null)
        {
            menuToggleAction.action.performed -= ToggleMenu;
            menuToggleAction.action.Disable();
        }

        if (leftArrow != null)
            leftArrow.onClick.RemoveListener(PrevPage);
        if (rightArrow != null)
            rightArrow.onClick.RemoveListener(NextPage);
    }

    private void Start()
    {
        if (MasterVolumeSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("LoopMasterVolume", 0.75f);
            MasterVolumeSlider.value = savedVolume;

            SetMasterVolume(savedVolume);

            MasterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        }

        if (MusicSlider != null)
        {
            MusicSlider.onValueChanged.AddListener(SetMusicVolume);
            MusicSlider.value = MusicSource.volume;
        }

        timerDropdown.onValueChanged.AddListener(OnTimerSelected);
    }

    private void OnDestroy()
    {
        timerDropdown.onValueChanged.RemoveListener(OnTimerSelected);
    }

    private void OnTimerSelected(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("No timer selected yet.");
                break;
            case 1:
                StartTimerAndHide(2);
                break;
            case 2:
                StartTimerAndHide(5);
                break;
            case 3:
                StartTimerAndHide(10);
                break;
            case 4:
                StartTimerAndHide(0);
                break;
            default:
                Debug.LogWarning("Unknown dropdown index: " + index);
                break;
        }
    }

    private void StartTimerAndHide(float minutes)
    {
        if (ScoreCanvas != null)
            ScoreCanvas.SetActive(true);

        if (displayManager != null)
            displayManager.StartTimer(minutes);
    }

    public void SetMasterVolume(float value)
    {
        if (masterMixer != null)
        {
            masterMixer.SetFloat(
                "LoopMasterVolume",
                Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20
            );
        }

        PlayerPrefs.SetFloat("LoopMasterVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        if (MusicSource != null)
        {
            MusicSource.volume = value;
        }
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        Debug.Log("Active Zone from : " + ZoneManager.Instance.activeZone);
        if (ZoneManager.Instance.activeZone == "LoopRoom")
        {
            ShowMainCanvas();
        }
    }

    public void ShowMainCanvas()
    {
        isMenuOpen = !isMenuOpen;
        if (menuCanvas != null)
            menuCanvas.SetActive(isMenuOpen);

        if (isMenuOpen)
        {
            ShowPage(currentPage);
        }
        else
        {
            for (int i = 0; i < pages.Length; i++)
                pages[i].SetActive(false);
        }
    }

    private void ShowPage(int index)
    {
        if (pages == null || pages.Length == 0)
            return;

        for (int i = 0; i < pages.Length; i++)
            pages[i].SetActive(i == index);
    }

    public void NextPage()
    {
        if (pages.Length == 0)
            return;
        currentPage = (currentPage + 1) % pages.Length;
        ShowPage(currentPage);
    }

    public void PrevPage()
    {
        if (pages.Length == 0)
            return;
        currentPage = (currentPage - 1 + pages.Length) % pages.Length;
        ShowPage(currentPage);
    }
}
