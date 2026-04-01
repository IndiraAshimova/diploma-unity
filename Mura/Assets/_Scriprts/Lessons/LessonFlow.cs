using System.Collections.Generic;
using UnityEngine;

public class LessonFlowManager : MonoBehaviour
{
    public static LessonFlowManager Instance;

    public LessonProgress Progress = new LessonProgress();

    private Queue<ILessonStep> steps = new Queue<ILessonStep>();
    private bool isRunning = false;

    // ? здесь хранится текущий урок
    public LessonSO CurrentLessonSO { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Запуск урока
    public void StartLesson(
        List<ILessonStep> lessonSteps,
        LessonSO lessonSO = null,
        bool skipCompleted = true)
    {
        if (isRunning)
        {
            Debug.Log("[LessonFlow] Уже выполняется урок!");
            return;
        }

        CurrentLessonSO = lessonSO;

        steps.Clear();

        foreach (var step in lessonSteps)
        {
            if (skipCompleted &&
                step is ITrackableStep trackable &&
                trackable.IsCompleted(Progress))
                continue;

            steps.Enqueue(step);
        }
        isRunning = true;
        RunNextStep();
    }

    private void RunNextStep()
    {
        if (steps.Count == 0)
        {
            Debug.Log("[LessonFlow] Lesson finished");
            isRunning = false;
            return;
        }

        ILessonStep step = steps.Dequeue();
        step.Execute(RunNextStep);
    }

    public void StartLessonForce(
        List<ILessonStep> lessonSteps,
        LessonSO lessonSO = null,
        bool skipCompleted = false)
    {
        Debug.Log("[LessonFlow] Принудительный запуск");
        steps.Clear();
        isRunning = false;

        StartLesson(lessonSteps, lessonSO, skipCompleted);
    }
}