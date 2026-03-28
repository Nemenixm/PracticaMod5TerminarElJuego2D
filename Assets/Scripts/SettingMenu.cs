using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("GRAPHICS")]
    [SerializeField] private Toggle lowToggle;
    [SerializeField] private Toggle mediumToggle;
    [SerializeField] private Toggle highToggle;

    [Header("SHADOWS")]
    [SerializeField] private Toggle shadowsOffToggle;
    [SerializeField] private Toggle shadowsOnToggle;

    [Header("SCREEN MODE")]
    [SerializeField] private TMP_Dropdown screenModeDropdown;

    private const string QUALITY_KEY = "QualityLevel";
    private const string SHADOWS_KEY = "ShadowsEnabled";
    private const string SCREENMODE_KEY = "ScreenMode";

    private bool isInitializing = true;

    private void Start()
    {
        SetupScreenModeDropdown();
        LoadSettings();
        SetupUI();
        AddListeners();

        isInitializing = false;
    }

    private void SetupScreenModeDropdown()
    {
        if (screenModeDropdown == null) return;

        screenModeDropdown.ClearOptions();
        screenModeDropdown.AddOptions(new System.Collections.Generic.List<string>
        {
            "Pantalla Completa",
            "Ventana"
        });
    }

    private void LoadSettings()
    {
        // QUALITY
        int savedQuality = PlayerPrefs.GetInt(QUALITY_KEY, 2);

        // Solo permitimos 3 niveles: 0 = Low, 1 = Medium, 2 = High
        savedQuality = Mathf.Clamp(savedQuality, 0, 2);
        ApplyQuality(savedQuality);

        // SHADOWS
        bool shadowsEnabled = PlayerPrefs.GetInt(SHADOWS_KEY, 1) == 1;
        ApplyShadows(shadowsEnabled);

        // SCREEN MODE
        int savedScreenMode = PlayerPrefs.GetInt(SCREENMODE_KEY, 0);
        savedScreenMode = Mathf.Clamp(savedScreenMode, 0, 1);
        ApplyScreenMode(savedScreenMode);
    }

    private void SetupUI()
    {
        int currentQuality = PlayerPrefs.GetInt(QUALITY_KEY, 2);
        currentQuality = Mathf.Clamp(currentQuality, 0, 2);

        if (lowToggle != null) lowToggle.isOn = (currentQuality == 0);
        if (mediumToggle != null) mediumToggle.isOn = (currentQuality == 1);
        if (highToggle != null) highToggle.isOn = (currentQuality == 2);

        bool shadowsEnabled = PlayerPrefs.GetInt(SHADOWS_KEY, 1) == 1;
        if (shadowsOffToggle != null) shadowsOffToggle.isOn = !shadowsEnabled;
        if (shadowsOnToggle != null) shadowsOnToggle.isOn = shadowsEnabled;

        int screenMode = PlayerPrefs.GetInt(SCREENMODE_KEY, 0);
        screenMode = Mathf.Clamp(screenMode, 0, 1);
        if (screenModeDropdown != null)
        {
            screenModeDropdown.value = screenMode;
            screenModeDropdown.RefreshShownValue();
        }
    }

    private void AddListeners()
    {
        if (lowToggle != null)
            lowToggle.onValueChanged.AddListener((isOn) =>
            {
                if (isOn) SetQualityLow();
            });

        if (mediumToggle != null)
            mediumToggle.onValueChanged.AddListener((isOn) =>
            {
                if (isOn) SetQualityMedium();
            });

        if (highToggle != null)
            highToggle.onValueChanged.AddListener((isOn) =>
            {
                if (isOn) SetQualityHigh();
            });

        if (shadowsOffToggle != null)
            shadowsOffToggle.onValueChanged.AddListener((isOn) =>
            {
                if (isOn) SetShadows(false);
            });

        if (shadowsOnToggle != null)
            shadowsOnToggle.onValueChanged.AddListener((isOn) =>
            {
                if (isOn) SetShadows(true);
            });

        if (screenModeDropdown != null)
            screenModeDropdown.onValueChanged.AddListener(SetScreenMode);
    }

    public void SetQualityLow()
    {
        if (isInitializing) return;
        SaveAndApplyQuality(0);
    }

    public void SetQualityMedium()
    {
        if (isInitializing) return;
        SaveAndApplyQuality(1);
    }

    public void SetQualityHigh()
    {
        if (isInitializing) return;
        SaveAndApplyQuality(2);
    }

    private void SaveAndApplyQuality(int qualityIndex)
    {
        ApplyQuality(qualityIndex);
        PlayerPrefs.SetInt(QUALITY_KEY, qualityIndex);
        PlayerPrefs.Save();

        Debug.Log("Graphics cambiado a: " + qualityIndex);
    }

    private void ApplyQuality(int qualityIndex)
    {
        // Mapeo:
        // 0 -> usa nivel de calidad 0 de Unity
        // 1 -> usa nivel de calidad 1 de Unity
        // 2 -> usa nivel de calidad 2 de Unity

        int maxQualityIndex = QualitySettings.names.Length - 1;
        int unityQualityIndex = Mathf.Clamp(qualityIndex, 0, maxQualityIndex);

        QualitySettings.SetQualityLevel(unityQualityIndex, true);
    }

    public void SetShadows(bool enabled)
    {
        if (isInitializing) return;

        ApplyShadows(enabled);
        PlayerPrefs.SetInt(SHADOWS_KEY, enabled ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("Shadows: " + (enabled ? "On" : "Off"));
    }

    private void ApplyShadows(bool enabled)
    {
        QualitySettings.shadows = enabled ? ShadowQuality.All : ShadowQuality.Disable;
    }

    public void SetScreenMode(int modeIndex)
    {
        if (isInitializing) return;

        ApplyScreenMode(modeIndex);
        PlayerPrefs.SetInt(SCREENMODE_KEY, modeIndex);
        PlayerPrefs.Save();

        Debug.Log("Screen Mode cambiado a: " + modeIndex);
    }

    private void ApplyScreenMode(int modeIndex)
    {
        switch (modeIndex)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;

            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }
}