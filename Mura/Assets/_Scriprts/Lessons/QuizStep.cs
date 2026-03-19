using UnityEngine;

public class QuizStep : MonoBehaviour, ILessonStep
{
    public QuizManager quizManager;

    public void Execute(System.Action onComplete)
    {
        if (quizManager == null)
        {
            Debug.LogError("QuizManager не назначен!");
            onComplete?.Invoke();
            return;
        }

        // Подписываемся на событие окончания квиза
        quizManager.onQuizFinished = () =>
        {
            // Закрываем окно квиза
            quizManager.CloseQuizWindow();

            // Сообщаем LessonFlowManager, что шаг завершён
            onComplete?.Invoke();
        };

        // Запускаем квиз
        quizManager.StartQuiz();
    }
}