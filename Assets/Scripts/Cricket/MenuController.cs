using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class XRMenuController : MonoBehaviour
{
    [Header("Menu Canvas")]
    public GameObject menuCanvas;

    [Header("Menu Pages")]
    public GameObject[] pages;
    public Button leftArrow;
    public Button rightArrow;

    [Header("Input")]
    public InputActionReference menuToggleAction; //  thumbstick press

    [Header("settings")]
    public Slider MasterVolumeSlider;
    public AudioMixer masterMixer;

    public Slider CrowdAudioSlider;
    public AudioSource CrowdAudio;

    public BallLauncher launcher;
    public Slider angleSlider;
    public Toggle RandomLaunchAngle;


    public TMP_Dropdown ballCountDropdown;

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
            float savedVolume = PlayerPrefs.GetFloat("CricketMasterVolume", 0.75f);
            MasterVolumeSlider.value = savedVolume;

            SetMasterVolume(savedVolume);

            MasterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        }

        CrowdAudioSlider.onValueChanged.AddListener(CrowdUpdateVolume);
        CrowdAudioSlider.value = CrowdAudio.volume;

        angleSlider.minValue = 5f;
        angleSlider.maxValue = 30f;

        angleSlider.onValueChanged.AddListener(val => launcher.upwardAngle = val);
        RandomLaunchAngle.onValueChanged.AddListener(val => launcher.RandomLaunchAngle = val);
        ballCountDropdown.onValueChanged.AddListener(i => launcher.maxBalls = (i + 1) * 10);
    }

    public void CrowdUpdateVolume(float value)
    {
        if (CrowdAudio != null)
            CrowdAudio.volume = value;
    }

    public void SetMasterVolume(float value)
    {
        if (masterMixer != null)
        {
            masterMixer.SetFloat(
                "CricketMasterVolume",
                Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20
            );
        }

        PlayerPrefs.SetFloat("CricketMasterVolume", value);
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        if (ZoneManager.Instance.activeZone == "Cricket")
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
