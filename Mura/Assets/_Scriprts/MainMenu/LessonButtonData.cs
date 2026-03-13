using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LessonButtonData : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI lessonNameText;
    [SerializeField] private Image completedIcon;

    [Header("Icons")]
    [SerializeField] private Sprite completedSprite;
    [SerializeField] private Sprite notCompletedSprite;

    private LessonSO lessonData;

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
        lessonNameText.text = data.LessonName;

        UpdateProgressUI();
    }

    public void LoadLesson()
    {
        SceneManager.LoadScene(lessonData.sceneName);
    }

    private void UpdateProgressUI()
    {
        bool completed = PlayerPrefs.GetInt("Lesson_" + lessonData.lessonIndex, 0) == 1;

        completedIcon.sprite = completed ? completedSprite : notCompletedSprite;
    }
}