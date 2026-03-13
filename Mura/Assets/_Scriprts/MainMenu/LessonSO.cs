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
    public Sprite thumbnail;
    public int lessonIndex;          // порядок внутри категории
    public string sceneName;       // сцена урока

    [SerializeField] private List<Question> lessonQuestions;
    public List<Question> LessonQuestions => lessonQuestions;
}