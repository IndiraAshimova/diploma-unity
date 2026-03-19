using UnityEngine;

public class DialogueStep : MonoBehaviour, ILessonStep
{
    public Story story;

    public void Execute(System.Action onComplete)
    {
        story.onStoryEnd = onComplete;
        story.TrigerStory();
    }
}