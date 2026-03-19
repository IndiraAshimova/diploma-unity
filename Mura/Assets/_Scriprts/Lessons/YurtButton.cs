using System.Collections.Generic;
using UnityEngine;

public class YurtButton : MonoBehaviour
{
    public LessonFlowManager flowManager;

    public ScreenStep openPart1;
    public DialogueStep dialogue1;
    public MiniGameStep miniGame;
    public ScreenStep backToMap;    
    public ScreenStep closePart1; 
    public LockStep lockThisYurt;
    public UnlockStep unlockNextYurt;

    public void OnClickYurt()
    {

        Debug.Log(openPart1);
        Debug.Log(dialogue1);
        Debug.Log(closePart1);
        Debug.Log(miniGame);
        Debug.Log(backToMap);

        List<ILessonStep> steps = new List<ILessonStep>()
        {
            openPart1,
            dialogue1,
            closePart1, 
            miniGame,
            lockThisYurt,     
            unlockNextYurt,
            backToMap

        };

        flowManager.StartLesson(steps);
    }
}