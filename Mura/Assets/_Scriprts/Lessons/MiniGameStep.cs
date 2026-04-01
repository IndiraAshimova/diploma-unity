using UnityEngine;

public class MiniGameStep : MonoBehaviour, ILessonStep
{
    public GameObject miniGameObject;
    public LevelScoreManager levelScore;
    private System.Action onComplete;

    public void Execute(System.Action onComplete)
    {
        this.onComplete = onComplete;
        miniGameObject.SetActive(true);
    }

    public void AddScore(int score)
    {
        if (levelScore != null)
            levelScore.AddScore(score);
    }

    public void FinishMiniGame()
    {
        miniGameObject.SetActive(false);
        Debug.Log("[MiniGameStep] FinishMiniGame called");
        onComplete?.Invoke();
    }
}