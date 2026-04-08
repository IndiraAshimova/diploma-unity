[System.Serializable]
public class LessonProgress
{
    public bool storyCompleted;      // для истории
    public bool dialogueCompleted;   // для диалога
    public bool miniGameCompleted;   // для мини-игры
    public bool quizCompleted;       // для квиза

    public int totalScore;           // общий score
}