using UnityEngine;

public class XPClickHandler : MonoBehaviour
{
    public DashboardManager dashboard;

    public void OnXPButtonClick()
    {
        Debug.Log("Кнопка нажата!"); // 1. Проверка клика
        if (dashboard != null)
        {
            dashboard.AddXP(1);
        }
        else
        {
            Debug.LogError("DashboardManager не присвоен!");
        }

    }
}
