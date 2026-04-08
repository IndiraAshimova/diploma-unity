using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private ServiceFactory services;

    void Awake()
    {
        Debug.Log("[GameInitializer] Инициализация сервисов");

        services = new ServiceFactory();

        if (LessonProgressManager.Instance != null)
        {
            LessonProgressManager.Instance.Initialize(
                services.XP
            );

            Debug.Log("[GameInitializer] XPService подключён");
        }
        else
        {
            Debug.LogError("[GameInitializer] LessonProgressManager не найден!");
        }
    }
}