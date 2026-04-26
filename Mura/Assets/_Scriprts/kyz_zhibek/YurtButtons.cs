using System.Collections.Generic;
using UnityEngine;

public class YurtButtons : MonoBehaviour
{
    [Header("Steps")]
    public List<MonoBehaviour> stepObjects =
        new List<MonoBehaviour>();

    public FinishStep finishStep;

    [Header("Lesson")]
    [SerializeField]
    private LessonSO lessonSO;
    public LessonSO LessonSO => lessonSO;

    [Header("Score Settings")]
    [SerializeField]
    private bool resetScoreOnStart = true;

    public void OnClickYurt()
    {
        if (resetScoreOnStart)
        {
            FindFirstObjectByType<
                LevelScoreManager>()
                ?.ResetScore();
        }

        List<ILessonStep> steps =
            new List<ILessonStep>();

        foreach (var obj in stepObjects)
        {
            if (obj is ILessonStep step)
                steps.Add(step);
        }

        if (finishStep != null)
            steps.Add(finishStep);

        LessonFlowManager.Instance
            .StartLesson(
                steps,
                lessonSO  
            );
    }

    public void RestartLesson()
    {
        if (resetScoreOnStart)
        {
            FindFirstObjectByType<LevelScoreManager>()
                ?.ResetScore();
        }

        List<ILessonStep> steps =
            new List<ILessonStep>();

        foreach (var obj in stepObjects)
        {
            if (obj is ILessonStep step)
                steps.Add(step);
        }

        if (finishStep != null)
            steps.Add(finishStep);

        LessonFlowManager.Instance
            .StartLessonForce(
                steps,
                lessonSO
            );
    }
}