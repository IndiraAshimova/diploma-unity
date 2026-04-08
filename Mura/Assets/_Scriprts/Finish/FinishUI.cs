using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishUI : MonoBehaviour
{
    public System.Action onFinishClosed;
    public System.Action onRestartPressed;

    [Header("UI Screens")]
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject partialWinUI;
    [SerializeField] private GameObject loseUI;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button backButton;
    [SerializeField] private Button restartButton; 

    private void Awake()
    {
        backButton.onClick.AddListener(OnBackButtonPressed);

        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartPressed);

        HideAllScreens();
    }

    public void Show(int score, LessonResult result)
    {
        scoreText.text = $"Score: {score}";
        HideAllScreens();

        switch (result)
        {
            case LessonResult.Win:
                winUI?.SetActive(true);
                break;

            case LessonResult.Partial:
                partialWinUI?.SetActive(true);
                break;

            case LessonResult.Lose:
                loseUI?.SetActive(true);
                break;
        }
    }

    private void HideAllScreens()
    {
        winUI?.SetActive(false);
        partialWinUI?.SetActive(false);
        loseUI?.SetActive(false);
    }

    private void OnBackButtonPressed()
    {
        onFinishClosed?.Invoke();
    }

    private void OnRestartPressed()
    {
        onRestartPressed?.Invoke();
    }
}