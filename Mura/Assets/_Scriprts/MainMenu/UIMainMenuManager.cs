using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MainMenuUIElements
{
    [SerializeField] private RectTransform lessonsParent;
    public RectTransform LessonsParent => lessonsParent;
}

public class UIMainMenuManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private MainMenuUIElements uiElements;

    [Header("Lesson Prefab")]
    [SerializeField] private LessonButtonData lessonPrefab;

    [Header("All Lessons")]
    [SerializeField] private LessonSO[] allLessons;

    private List<LessonButtonData> currentLessons = new List<LessonButtonData>();

    private void Awake()
    {
        allLessons = Resources.LoadAll<LessonSO>("Lessons");
        // Папка: Assets/Resources/Lessons
    }
    public void ShowCategory(Category category)
    {
        ClearLessons();

        foreach (var lesson in allLessons)
        {
            if (lesson.category == category)
            {
                Debug.Log("Creating lesson: " + lesson.LessonName);
                LessonButtonData newLesson =
                    Instantiate(lessonPrefab, uiElements.LessonsParent);

                newLesson.Setup(lesson);

                currentLessons.Add(newLesson);
            }
        }
    }

    private void ClearLessons()
    {
        foreach (var lesson in currentLessons)
        {
            Destroy(lesson.gameObject);
        }

        currentLessons.Clear();
    }
}