using UnityEngine;

public class Story : MonoBehaviour
{
    public Dialogue dialogue;
    public System.Action onStoryEnd;

    public void TrigerStory()
    {
        DialogueManager dm = FindAnyObjectByType<DialogueManager>();
        dm.onDialogueEnd = () => onStoryEnd?.Invoke();
        dm.StartDialogue(dialogue);
    }
}