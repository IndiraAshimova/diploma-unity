using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public System.Action onDialogueEnd;

    [SerializeField]
    private DialogueManager dialogueManager;

    public void TriggerDialogue()
    {
        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager не назначен!");
            return;
        }

        dialogueManager.onDialogueEnd =
            () => onDialogueEnd?.Invoke();

        dialogueManager.StartDialogue(dialogue);
    }

    public void ShowDialogue()
    {
        if (dialogueManager != null)
            dialogueManager.gameObject.SetActive(true);
    }

    public void HideDialogue()
    {
        if (dialogueManager != null)
            dialogueManager.gameObject.SetActive(false);
    }
}