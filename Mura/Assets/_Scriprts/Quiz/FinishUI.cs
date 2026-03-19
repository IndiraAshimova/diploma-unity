using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishUI : MonoBehaviour
{
    public System.Action onFinishClosed;

    [Header("UI Elements")]
    [SerializeField] private GameObject finishWindow;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button backButton;

    [Header("Сервисы")]
    [SerializeField] private XPService xpService; // назначаем через инспектор

    private int lastScore = 0;

    private void Awake()
    {
        finishWindow.SetActive(false);
        backButton.onClick.AddListener(OnBackButtonPressed);
    }

    public void Show(int score)
    {
        lastScore = score;
        scoreText.text = "Score: " + score;
        finishWindow.SetActive(true);
    }

    private void OnBackButtonPressed()
    {
        StartCoroutine(SendXPAndReturn());
    }

    private IEnumerator SendXPAndReturn()
    {
        if (xpService != null)
        {
            // Теперь вызываем AddXP напрямую через ссылку
            yield return StartCoroutine(xpService.AddXP(lastScore));
        }
        else
        {
            Debug.LogWarning("XPService не назначен на FinishUI!");
        }

        onFinishClosed?.Invoke();

    }
}