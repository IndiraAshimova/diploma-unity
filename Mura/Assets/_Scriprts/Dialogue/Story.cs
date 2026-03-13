using UnityEngine;

public class Story : MonoBehaviour {
    public Dialogue dialogue;

    public void TrigerStory()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }
    
}
