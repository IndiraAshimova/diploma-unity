using System.Collections.Generic;
using UnityEngine;

public enum Category
{
    Beginner,
    Teen,
    Advanced
}

[CreateAssetMenu(fileName = "NewLesson", menuName = "Lessons/Lesson")]
public class LessonSO : ScriptableObject
{
    [SerializeField] private string lessonName;
    public string LessonName => lessonName;

    public Category category;
    public int lessonIndex;
    public string sceneName;

    [Header("Win thresholds")]
    public int winScore = 50;
    public int partialScore = 25;

    [SerializeField] private List<Question> lessonQuestions;
    public List<Question> LessonQuestions => lessonQuestions;

    public LessonResult GetResult(int score)
    {
        if (score >= winScore)
            return LessonResult.Win;

        if (score >= partialScore)
            return LessonResult.Partial;

        return LessonResult.Lose;
    }

    private void OnValidate()
    {
        if (partialScore > winScore)
        {
            Debug.LogWarning(
                $"PartialScore больше WinScore в {name}. »справл€ю автоматически."
            );

            partialScore = winScore / 2;
        }
    }
}