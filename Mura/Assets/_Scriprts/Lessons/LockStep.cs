using UnityEngine;

public class LockStep : MonoBehaviour, ILessonStep
{
    public YurtUnlock yurt;

    public void Execute(System.Action onComplete)
    {
        yurt.Lock();
        onComplete?.Invoke();
    }
}