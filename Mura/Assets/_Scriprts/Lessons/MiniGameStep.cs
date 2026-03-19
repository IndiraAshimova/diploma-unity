using UnityEngine;

public class MiniGameStep : MonoBehaviour, ILessonStep
{
    public GameObject miniGameObject;

    private System.Action onComplete;

    public void Execute(System.Action onComplete)
    {
        this.onComplete = onComplete;

        miniGameObject.SetActive(true);
    }
    public void FinishMiniGame()
    {
        miniGameObject.SetActive(false);
        onComplete?.Invoke();
    }
}