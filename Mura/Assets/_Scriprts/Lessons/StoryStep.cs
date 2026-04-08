using System;
using UnityEngine;

public class StoryStep : MonoBehaviour, ILessonStep, ICancelableStep
{
    public Story story; // твоя система истории / cutscene

    public void Execute(Action onComplete)
    {
        if (story == null)
        {
            Debug.LogError("StoryStep: Story не назначен!");
            onComplete?.Invoke();
            return;
        }

        if (LessonProgressManager.Instance == null)
        {
            Debug.LogWarning("LessonProgressManager.Instance не найден!");
        }

        // Подписка на конец истории
        story.onStoryEnd = () =>
        {
            LessonFlowManager
                .Instance
                .Progress
                .storyCompleted = true;

            onComplete?.Invoke();
        };

        // Показываем UI и запускаем историю
        story.ShowStory();
        story.TriggerStory();
    }

    public void CancelStep()
    {
        if (story != null)
        {
            story.HideStory();
        }
    }
}