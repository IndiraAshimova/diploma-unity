using UnityEngine;

public class ScreenStep : MonoBehaviour, ILessonStep
{
    public GameObject screenToShow;
    public GameObject screenToHide;

    public void Execute(System.Action onComplete)
    {
        if (screenToHide != null)
            screenToHide.SetActive(false);

        if (screenToShow != null)
            screenToShow.SetActive(true);

        onComplete?.Invoke();
    }
}