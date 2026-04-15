using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // Установить текущие значения

        musicSlider.value =
            SoundManager.Instance
                .GetMusicVolume();

        sfxSlider.value =
            SoundManager.Instance
                .GetSFXVolume();

        // Подписка на изменения

        musicSlider.onValueChanged
            .AddListener(SetMusicVolume);

        sfxSlider.onValueChanged
            .AddListener(SetSFXVolume);
    }

    private void SetMusicVolume(float volume)
    {
        SoundManager.Instance
            .SetMusicVolume(volume);
    }

    private void SetSFXVolume(float volume)
    {
        SoundManager.Instance
            .SetSFXVolume(volume);
    }
}