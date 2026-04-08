using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LessonButtonData : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI lessonNameText;
    [SerializeField] private Image completedIcon;

    [Header("Icons")]
    [SerializeField] private Sprite completedSprite;
    [SerializeField] private Sprite notCompletedSprite;

    private LessonSO lessonData;

    // Событие выбора урока
    public System.Action<LessonSO> OnLessonSelected;

    public RectTransform Rect
    {
        get
        {
            return GetComponent<RectTransform>();
        }
    }

    public void Setup(LessonSO data)
    {
        lessonData = data;

        lessonNameText.text =
            lessonData.LessonName;

        UpdateProgressUI();
    }

    public void OnClick()
    {
        // Только уведомляем
        OnLessonSelected?.Invoke(lessonData);
    }

    private void UpdateProgressUI()
    {
        var progress =
            LessonProgressManager.Instance
                .GetLessonProgress(
                    lessonData.lessonIndex);

        bool completed =
            progress.completed;

        completedIcon.sprite =
            completed
                ? completedSprite
                : notCompletedSprite;
    }

    private void OnEnable()
    {
        if (lessonData != null)
            UpdateProgressUI();
    }
}