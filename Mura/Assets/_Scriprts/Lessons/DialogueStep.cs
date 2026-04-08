using System;
using UnityEngine;

public class DialogueStep : MonoBehaviour, ILessonStep, ITrackableStep, ICancelableStep
{
    public DialogueTrigger dialogueTrigger;

    public void Execute(Action onComplete)
    {
        dialogueTrigger.onDialogueEnd += () =>
        {
            MarkCompleted(
                LessonFlowManager.Instance.Progress);

            onComplete?.Invoke();

            dialogueTrigger.onDialogueEnd = null;
        };

        dialogueTrigger.gameObject.SetActive(true);

        dialogueTrigger.TriggerDialogue();
    }

    public void CancelStep()
    {
        dialogueTrigger?.gameObject.SetActive(false); // скрываем UI
    }

    public bool IsCompleted(LessonProgress progress) => progress.dialogueCompleted;
    public void MarkCompleted(LessonProgress progress) => progress.dialogueCompleted = true;
}