using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapButton : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Button button;

    private MapPoint point;
    private LessonMapManager manager;

    public void Setup(
        MapPoint mapPoint,
        LessonMapManager mapManager)
    {
        point = mapPoint;
        manager = mapManager;

        if (titleText != null)
            titleText.text = point.title;

        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(() =>
        {
            manager.GoToPoint(point);
        });
    }
}