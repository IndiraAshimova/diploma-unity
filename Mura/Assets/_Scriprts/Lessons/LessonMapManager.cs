using System.Collections.Generic;
using UnityEngine;

public class LessonMapManager : MonoBehaviour
{
    [Header("Map Points")]
    [SerializeField]
    private List<MapPoint> mapPoints =
        new List<MapPoint>();


    [Header("UI")]
    [SerializeField]
    private MapButton buttonPrefab;

    [SerializeField]
    private Transform buttonContainer;


    private void Start()
    {
        CreateButtons();
    }


    private void CreateButtons()
    {
        foreach (var point in mapPoints)
        {
            if (point == null)
                continue;

            var buttonObj =
                Instantiate(
                    buttonPrefab,
                    buttonContainer
                );

            buttonObj.Setup(
                point,
                this
            );
        }
    }


    public void GoToPoint(MapPoint point)
    {
        if (point == null ||
            point.yurtSource == null)
        {
            Debug.LogError("[Map] Ошибка точки");
            return;
        }

        Debug.Log("[Map] Переход к: " + point.title);

        List<ILessonStep> steps =
            new List<ILessonStep>();

        bool startAdding =
            point.startStep == null;

        foreach (var obj in point.yurtSource.stepObjects)
        {
            if (!startAdding &&
                obj == point.startStep)
            {
                startAdding = true;
            }

            if (startAdding &&
                obj is ILessonStep step)
            {
                steps.Add(step);
            }
        }

        if (steps.Count == 0)
        {
            Debug.LogError(
                "[Map] Стартовый шаг не найден!"
            );
            return;
        }

        if (point.yurtSource.finishStep != null)
        {
            steps.Add(
                point.yurtSource.finishStep
            );
        }

        LessonSO lesson =
            point.yurtSource.LessonSO;

        LessonFlowManager.Instance
            .StartLessonForce(
                steps,
                lesson
            );
    }
}