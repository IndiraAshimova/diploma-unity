using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public int totalItems = 4;
    private int currentCorrect = 0;

    public MiniGameStep step;

    [Header("Очки за мини-игру")]
    [SerializeField] private int reward = 20;

    [SerializeField] private LevelScoreManager levelScore;

    private void Start()
    {
        // регистрируем максимум
        if (levelScore != null)
        {
            levelScore.AddMaxScore(reward);
        }
    }

    public void RegisterCorrect()
    {
        currentCorrect++;

        Debug.Log($"[MiniGame] {currentCorrect}/{totalItems}");

        if (currentCorrect >= totalItems)
        {
            Debug.Log("[MiniGame] Завершена");

            if (levelScore != null)
            {
                levelScore.AddScore(reward);
            }

            if (step != null)
            {
                step.FinishMiniGame();
            }
        }
    }
}