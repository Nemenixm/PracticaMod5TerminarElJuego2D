using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource flySource;

    [Header("UI Sliders")]
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSfx;

    [Header("Clips de Acción")]
    [SerializeField] private AudioClip clipVuelo;
    [SerializeField] private AudioClip clipSuelo;
    [SerializeField] private AudioClip clipAtaque;
    [SerializeField] private AudioClip clipVictoria;

    private const string MUSIC_KEY = "MUSIC_VOL";
    private const string SFX_KEY = "SFX_VOL";

    private void Awake()
    {
        if (musicSource != null && sfxSource != null && musicSource == sfxSource)
        {
            Debug.LogWarning("Music Source y Sfx Source no pueden ser el mismo AudioSource.");
        }
    }

    private void Start()
    {
        float musicVol = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVol = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        ConfigurarSources();
        ApplyMusic(musicVol);
        ApplySfx(sfxVol);

        if (sliderMusic != null)
        {
            sliderMusic.SetValueWithoutNotify(musicVol);
            sliderMusic.onValueChanged.RemoveListener(ApplyMusic);
            sliderMusic.onValueChanged.AddListener(ApplyMusic);
        }

        if (sliderSfx != null)
        {
            sliderSfx.SetValueWithoutNotify(sfxVol);
            sliderSfx.onValueChanged.RemoveListener(ApplySfx);
            sliderSfx.onValueChanged.AddListener(ApplySfx);
        }
    }

    private void OnDestroy()
    {
        if (sliderMusic != null)
            sliderMusic.onValueChanged.RemoveListener(ApplyMusic);

        if (sliderSfx != null)
            sliderSfx.onValueChanged.RemoveListener(ApplySfx);
    }

    private void ConfigurarSources()
    {
        if (musicSource != null)
        {
            musicSource.playOnAwake = true;
            musicSource.loop = true;
        }

        if (sfxSource != null)
        {
            sfxSource.playOnAwake = false;
            sfxSource.loop = false;
        }

        if (flySource != null)
        {
            flySource.playOnAwake = false;
            flySource.loop = true;
            flySource.clip = clipVuelo;
        }
    }

    public void ApplyMusic(float value)
    {
        value = Mathf.Clamp01(value);

        if (musicSource != null)
            musicSource.volume = value;

        PlayerPrefs.SetFloat(MUSIC_KEY, value);
        PlayerPrefs.Save();
    }

    public void ApplySfx(float value)
    {
        value = Mathf.Clamp01(value);

        if (sfxSource != null)
            sfxSource.volume = value;

        if (flySource != null)
            flySource.volume = value;

        PlayerPrefs.SetFloat(SFX_KEY, value);
        PlayerPrefs.Save();
    }

    public void StartFly()
    {
        if (flySource == null || clipVuelo == null)
            return;

        if (!flySource.isPlaying)
            flySource.Play();
    }

    public void StopFly()
    {
        if (flySource != null && flySource.isPlaying)
            flySource.Stop();
    }

    public void PlaySuelo()
    {
        StopFly();
        PlayEfecto(clipSuelo);
    }

    public void PlayAtaque()
    {
        PlayEfecto(clipAtaque);
    }

    public void PlayVictoria()
    {
        StopFly();

        if (musicSource != null)
            musicSource.Stop();

        PlayEfecto(clipVictoria);
    }

    private void PlayEfecto(AudioClip clip)
    {
        if (sfxSource == null || clip == null)
            return;

        sfxSource.PlayOneShot(clip);
    }
}