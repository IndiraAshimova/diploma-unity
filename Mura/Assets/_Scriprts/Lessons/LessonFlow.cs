using System.Collections.Generic;
using UnityEngine;

public class LessonFlowManager : MonoBehaviour
{
    private Queue<ILessonStep> steps = new Queue<ILessonStep>();

    public void StartLesson(List<ILessonStep> lessonSteps)
    {
        steps.Clear();

        foreach (var step in lessonSteps)
            steps.Enqueue(step);

        RunNextStep();
    }

    void RunNextStep()
    {
        if (steps.Count == 0)
        {
            Debug.Log("Lesson finished");
            return;
        }

        ILessonStep step = steps.Dequeue();

        if (step == null)
        {
            Debug.LogError("STEP IS NULL ?");
            RunNextStep();
            return;
        }

        step.Execute(() =>
        {
            RunNextStep();
        });
    }
}