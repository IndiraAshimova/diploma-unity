using System;
using UnityEngine;

public class ScreenStep : MonoBehaviour, ILessonStep
{
    [Header("Screens")]
    public GameObject screenToShow;
    public GameObject screenToHide;

    [Header("Optional Music")]
    [SerializeField] private AudioClip music;

    public void Execute(Action onComplete)
    {
        if (screenToHide != null)
            screenToHide.SetActive(false);
        else
            Debug.LogWarning("ScreenStep: screenToHide не назначен!");

        if (screenToShow != null)
            screenToShow.SetActive(true);
        else
            Debug.LogWarning("ScreenStep: screenToShow не назначен!");

        // музыка (если назначена)
        if (music != null)
        {
            SoundManager.Instance.PlayMusic(music);
        }

        onComplete?.Invoke();
    }
}