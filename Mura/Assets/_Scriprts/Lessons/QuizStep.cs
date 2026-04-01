using System;
using UnityEngine;

public class QuizStep : MonoBehaviour, ILessonStep, ICancelableStep
{
    public QuizManager quizManager;
    public LevelScoreManager levelScore; // общий менеджер очков
    public FinishStep finishStep;        // шаг финала

    private int pointsEarned = 0;        // очки, набранные в этом шаге

    public void Execute(Action onComplete)
    {
        if (quizManager == null)
        {
            Debug.LogError("[QuizStep] QuizManager не назначен!");
            onComplete?.Invoke();
            return;
        }

        // Сбрасываем очки этого шага
        pointsEarned = 0;

        // ВАЖНО: очищаем прошлую подписку
        quizManager.onQuizFinished = null;

        // Подписка на завершение квиза
        quizManager.onQuizFinished = () =>
        {
            // сохраняем прогресс урока
            SaveQuizProgress();

            // показываем финал
            if (finishStep != null)
            {
                finishStep.Execute(() =>
                {
                    Debug.Log("[QuizStep] Финал закрыт, шаг квиза завершён");
                    onComplete?.Invoke();
                });
            }
            else
            {
                onComplete?.Invoke();
            }
        };

        quizManager.ShowQuiz();
        quizManager.StartQuiz();
    }

    /// <summary>
    /// Вызываем из QuizManager или другого метода квиза,
    /// чтобы начислить очки, набранные игроком в этом шаге
    /// </summary>
    public void AddScore(int score)
    {
        pointsEarned += score;
        Debug.Log($"[QuizStep] Игрок набрал {score} очков (в этом шаге всего {pointsEarned})");
    }

    private void SaveQuizProgress()
    {
        if (LessonContext.CurrentLesson == null)
            return;

        var lesson = LessonContext.CurrentLesson;

        var progress =
            LessonProgressManager.Instance
                .GetLessonProgress(lesson.lessonIndex);

        int totalScore = levelScore != null ? levelScore.TotalScore : 0;

        if (totalScore > progress.bestScore)
        {
            progress.bestScore = totalScore;
        }

        LessonProgressManager.Instance.SaveProgress();
    }

    public void CancelStep()
    {
        quizManager?.HideQuiz();
        pointsEarned = 0;
        Debug.Log("[QuizStep] Шаг отменён, очки сброшены");
    }
}