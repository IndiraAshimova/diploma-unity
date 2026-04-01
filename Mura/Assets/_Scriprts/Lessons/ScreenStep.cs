using UnityEngine;

public class ScreenStep : MonoBehaviour, ILessonStep
{
    public GameObject screenToShow;
    public GameObject screenToHide;

    public void Execute(System.Action onComplete)
    {
        if (screenToHide != null)
            screenToHide.SetActive(false);
        else
            Debug.LogWarning("ScreenStep: screenToHide не назначен!");

        if (screenToShow != null)
            screenToShow.SetActive(true);
        else
            Debug.LogWarning("ScreenStep: screenToShow не назначен!");

        onComplete?.Invoke();
    }
}