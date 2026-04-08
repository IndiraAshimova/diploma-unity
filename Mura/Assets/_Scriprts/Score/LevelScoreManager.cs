using System;
using UnityEngine;

public class LevelScoreManager : MonoBehaviour
{
    // Основной счёт
    public int TotalScore { get; private set; }

    // Максимальный возможный счёт (для процента)
    public int MaxScore { get; private set; }

    // Очки текущей сессии (например, при возврате к StoryStep)
    public int SessionScore { get; private set; }

    // Событие для UI
    public event Action OnScoreChanged;

    #region Основные методы

    public void AddScore(int amount)
    {
        TotalScore += amount;
        SessionScore += amount;
        OnScoreChanged?.Invoke();
    }

    public void AddMaxScore(int amount)
    {
        MaxScore += amount;
        OnScoreChanged?.Invoke();
    }

    public void ResetScore()
    {
        TotalScore = 0;
        MaxScore = 0;
        SessionScore = 0;
        OnScoreChanged?.Invoke();
    }

    public void ResetSessionScore()
    {
        SessionScore = 0;
        OnScoreChanged?.Invoke();
    }

    public float GetPercent()
    {
        if (MaxScore == 0) return 0f;
        return (float)TotalScore / MaxScore;
    }

    // Прямой сет значений (для восстановления прогресса, если нужно)
    public void SetScore(int total, int max)
    {
        TotalScore = total;
        MaxScore = max;
        OnScoreChanged?.Invoke();
    }

    #endregion
}