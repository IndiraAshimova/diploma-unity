using TMPro;
using UnityEngine;

public class LevelScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private LevelScoreManager levelScore;

    private void Start()
    {
        if (levelScore == null)
        {
            Debug.LogError("[ScoreUI] LevelScoreManager не назначен!");
            return;
        }

        // Обновляем UI при старте
        UpdateScore();

        // Подписка на изменения очков
        levelScore.OnScoreChanged += UpdateScore;
    }

    private void OnDestroy()
    {
        if (levelScore != null)
            levelScore.OnScoreChanged -= UpdateScore;
    }

    private void UpdateScore()
    {
        if (levelScore == null) return;

        scoreText.text = $"Score: {levelScore.TotalScore}";
    }
}