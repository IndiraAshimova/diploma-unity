using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class HamburgerMenuItem
{
    public string title;

    [Header("Lesson")]
    public LessonSO lessonSO;

    [Header("Источник шагов (Юрта)")]
    public YurtButtons yurtSource;

    [Header("С какого шага начать")]
    public MonoBehaviour startStep;

    [Header("Выход в меню")]
    public bool isExit;
}

public class HamburgerMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private HamburgerMenuButton buttonPrefab;
    [SerializeField] private List<HamburgerMenuItem> menuItems = new List<HamburgerMenuItem>();

    private List<ICancelableStep> currentlyRunningSteps = new List<ICancelableStep>();

    private void Start()
    {
        menuPanel.SetActive(false);

        foreach (var item in menuItems)
        {
            var buttonObj = Instantiate(buttonPrefab, menuPanel.transform);

            if (item.isExit)
            {
                buttonObj.Setup(item.title, () => ExitToMainMenu("Main_Menu"));
            }
            else
            {
                buttonObj.Setup(item.title, () => RunMenuItem(item));
            }
        }
    }

    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

    private void RunMenuItem(HamburgerMenuItem item)
    {
        Debug.Log("[Hamburger] Запуск пункта: " + item.title);

        // 1. Отменяем старые шаги
        foreach (var step in currentlyRunningSteps)
            step.CancelStep();

        currentlyRunningSteps.Clear();

        // 2. Сбрасываем только временные очки текущей сессии
        FindFirstObjectByType<LevelScoreManager>()?.ResetSessionScore();

        // 3. Прогресс урока НЕ обнуляем
        // LessonFlowManager.Instance.Progress = new LessonProgress(); // ? оставляем старый

        // 4. Собираем шаги для запуска
        List<ILessonStep> stepsList = new List<ILessonStep>();

        if (item.yurtSource != null)
        {
            bool startAdding = item.startStep == null;

            foreach (var obj in item.yurtSource.stepObjects)
            {
                if (obj is ILessonStep step)
                {
                    if (!startAdding && obj == item.startStep)
                        startAdding = true;

                    if (startAdding)
                    {
                        stepsList.Add(step);

                        if (step is ICancelableStep cancelable)
                            currentlyRunningSteps.Add(cancelable);
                    }
                }
            }

            // добавляем финал
            if (item.yurtSource.finishStep != null)
                stepsList.Add(item.yurtSource.finishStep);
        }
        else
        {
            Debug.LogWarning("[Hamburger] У пункта нет yurtSource!");
        }

        if (stepsList.Count == 0)
        {
            Debug.LogError("[Hamburger] Нет шагов для запуска!");
            return;
        }

        // 5. Передаём LessonSO в FlowManager
        LessonFlowManager.Instance.StartLessonForce(
            stepsList,
            item.lessonSO,
            false
        );

        menuPanel.SetActive(false);
    }

    private void ExitToMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}