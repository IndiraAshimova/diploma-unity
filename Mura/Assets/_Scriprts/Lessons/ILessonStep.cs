using System;

public interface ILessonStep
{
    void Execute(Action onComplete);
}

public interface ITrackableStep
{
    bool IsCompleted(LessonProgress progress);
    void MarkCompleted(LessonProgress progress);
}
public interface ICancelableStep
{
    void CancelStep(); // скрывает UI и останавливает шаг
}