using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct MainMenuUIElements
{
    [SerializeField] private RectTransform lessonsParent;
    public RectTransform LessonsParent => lessonsParent;
}
public class UIMainMenuManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private MainMenuUIElements uiElements;

    [Header("Lesson Prefab")]
    [SerializeField]
    private LessonButtonData lessonPrefab;

    [Header("All Lessons")]
    [SerializeField]
    private LessonSO[] allLessons;

    private List<LessonButtonData>
        currentLessons =
            new List<LessonButtonData>();

    private void Awake()
    {
        allLessons =
            Resources.LoadAll<LessonSO>(
                "Lessons");
    }

    public void ShowCategory(
        Category category)
    {
        ClearLessons();

        foreach (var lesson in allLessons)
        {
            if (lesson.category != category)
                continue;

            CreateLessonButton(lesson);
        }
    }

    private void CreateLessonButton(
        LessonSO lesson)
    {
        LessonButtonData newLesson =
            Instantiate(
                lessonPrefab,
                uiElements.LessonsParent);

        newLesson.Setup(lesson);

        // Подписка на событие
        newLesson.OnLessonSelected +=
            HandleLessonSelected;

        currentLessons.Add(newLesson);
    }

    private void HandleLessonSelected(
        LessonSO lesson)
    {
        // сохраняем урок
        LessonContext.CurrentLesson =
            lesson;

        // загружаем сцену
        SceneManager.LoadScene(
            lesson.sceneName);
    }

    private void ClearLessons()
    {
        foreach (var lesson
                 in currentLessons)
        {
            if (lesson != null)
            {
                lesson.OnLessonSelected -=
                    HandleLessonSelected;

                Destroy(lesson.gameObject);
            }
        }

        currentLessons.Clear();
    }
}