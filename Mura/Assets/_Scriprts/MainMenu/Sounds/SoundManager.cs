using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("SFX")]
    [SerializeField] private AudioClip clickSound;

    private Coroutine musicCoroutine;
    private AudioClip currentMusic;

    private const string MUSIC_KEY = "MusicVolume";
    private const string SFX_KEY = "SFXVolume";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadVolume();
    }

    // =========================
    // SFX
    // =========================

    public void PlayClick()
    {
        sfxSource.PlayOneShot(clickSound);
    }

    // =========================
    // MUSIC
    // =========================

    public void PlayMusic(AudioClip newMusic)
    {
        if (newMusic == null)
            return;

        if (currentMusic == newMusic)
            return;

        currentMusic = newMusic;

        if (musicCoroutine != null)
            StopCoroutine(musicCoroutine);

        musicCoroutine =
            StartCoroutine(FadeMusic(newMusic));
    }

    private IEnumerator FadeMusic(AudioClip newClip)
    {
        float startVolume = musicSource.volume;

        // Fade out

        while (musicSource.volume > 0)
        {
            musicSource.volume -=
                startVolume * Time.deltaTime;

            yield return null;
        }

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();

        // Fade in

        while (musicSource.volume < startVolume)
        {
            musicSource.volume +=
                startVolume * Time.deltaTime;

            yield return null;
        }
    }

    // =========================
    // VOLUME CONTROL
    // =========================

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;

        PlayerPrefs.SetFloat(MUSIC_KEY, volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;

        PlayerPrefs.SetFloat(SFX_KEY, volume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume()
    {
        return musicSource.volume;
    }

    public float GetSFXVolume()
    {
        return sfxSource.volume;
    }

    private void LoadVolume()
    {
        float musicVolume =
            PlayerPrefs.GetFloat(MUSIC_KEY, 1f);

        float sfxVolume =
            PlayerPrefs.GetFloat(SFX_KEY, 1f);

        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }
}