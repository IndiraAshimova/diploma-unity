using System;
using UnityEngine;

public class QuizStep : MonoBehaviour, ILessonStep, ICancelableStep
{
    public QuizManager quizManager;
    public LevelScoreManager levelScore;
    public FinishStep finishStep;

    private int pointsEarned = 0;

    public void Execute(Action onComplete)
    {
        if (quizManager == null)
        {
            Debug.LogError("[QuizStep] QuizManager не назначен!");
            onComplete?.Invoke();
            return;
        }

        pointsEarned = 0;
        quizManager.onQuizFinished = null;

        quizManager.onQuizFinished = () =>
        {
            onComplete?.Invoke();
        };

        quizManager.ShowQuiz();
        quizManager.StartQuiz();
    }

    public void AddScore(int score)
    {
        pointsEarned += score;
        Debug.Log($"[QuizStep] Игрок набрал {score} очков (в этом шаге всего {pointsEarned})");
    }

    public void CancelStep()
    {
        quizManager?.HideQuiz();
        pointsEarned = 0;
        Debug.Log("[QuizStep] Шаг отменён, очки сброшены");
    }
}