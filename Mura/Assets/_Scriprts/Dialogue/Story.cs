using System;
using UnityEngine;

public class Story : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public Dialogue dialogue;                   // Твоя история / cutscene
    public DialogueManager storyManager;        // Привязанный DialogueManager для этого Story

    [HideInInspector]
    public Action onStoryEnd;                   // Событие конца истории

    /// <summary>
    /// Запуск истории
    /// </summary>
    public void TriggerStory()
    {
        if (storyManager == null)
        {
            Debug.LogError($"Story {name} не имеет назначенного DialogueManager!");
            return;
        }

        storyManager.onDialogueEnd = () => onStoryEnd?.Invoke();
        storyManager.StartDialogue(dialogue);
    }

    /// <summary>
    /// Показать UI истории
    /// </summary>
    public void ShowStory()
    {
        if (storyManager != null)
            storyManager.gameObject.SetActive(true);
        else
            Debug.LogWarning($"Story {name}: ShowStory - DialogueManager не назначен!");
    }

    /// <summary>
    /// Скрыть UI истории
    /// </summary>
    public void HideStory()
    {
        if (storyManager != null)
            storyManager.gameObject.SetActive(false);
    }
}