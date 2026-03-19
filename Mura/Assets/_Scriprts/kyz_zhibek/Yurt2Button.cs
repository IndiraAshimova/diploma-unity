using System.Collections.Generic;
using UnityEngine;

public class Yurt2Button : MonoBehaviour
{
    public LessonFlowManager flowManager;

    public ScreenStep openPart2;
    public DialogueStep dialogue2;
    public QuizStep quizGame;
    public ScreenStep backToMap;

    public void OnClickYurt2()
    {
        List<ILessonStep> steps = new List<ILessonStep>()
        {
            openPart2,
            dialogue2,
            quizGame,
            backToMap
        };

        flowManager.StartLesson(steps);
    }
}