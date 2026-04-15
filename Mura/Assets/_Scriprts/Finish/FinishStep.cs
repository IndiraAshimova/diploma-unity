using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishStep : MonoBehaviour, ILessonStep
{
    public FinishUI finishUI;
    public GameObject finishRoot;
    public LevelScoreManager levelScore;

    public string nextScene = "Main_Map";

    public LessonSO lesson;
    [SerializeField]
    private YurtButtons restartYurt;

    public void Execute(Action onComplete)
    {
        if (finishUI == null || finishRoot == null)
        {
            Debug.LogError("FinishUI или FinishRoot не назначен!");
            onComplete?.Invoke();
            return;
        }

        int score =
            levelScore != null
                ? levelScore.TotalScore
                : 0;

        var lessonToUse =
            lesson ??
            LessonFlowManager.Instance.CurrentLessonSO;

        if (lessonToUse == null)
        {
            Debug.LogError("CurrentLessonSO == null!");
            onComplete?.Invoke();
            return;
        }

        LessonResult result =
            lessonToUse.GetResult(score);

        finishRoot.SetActive(true);

        finishUI.Show(score, result);

        LessonProgressManager.Instance
            .HandleLessonEnd(
                lessonToUse.lessonIndex,
                score,
                result
            );

        // Кнопка "Назад"
        finishUI.onFinishClosed = () =>
        {
            onComplete?.Invoke();
            SceneManager.LoadScene(nextScene);
        };

        finishUI.onRestartPressed = () =>
        {
            RestartLesson();
        };
    }
    void RestartLesson()
    {
        Debug.Log("[FinishStep] Restart lesson");

        finishRoot.SetActive(false);

        if (restartYurt != null)
        {
            restartYurt.RestartLesson();
        }
        else
        {
            Debug.LogError("restartYurt не назначен!");
        }
    }

}