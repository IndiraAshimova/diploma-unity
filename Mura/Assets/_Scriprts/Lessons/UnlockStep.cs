using UnityEngine;

public class UnlockStep : MonoBehaviour, ILessonStep
{
    public YurtUnlock yurt;

    public void Execute(System.Action onComplete)
    {
        yurt.Unlock();
        onComplete?.Invoke();
    }
}