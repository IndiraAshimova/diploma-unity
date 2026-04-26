using UnityEngine;
using UnityEngine.SceneManagement;

public class HamburgerMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private GameObject menuPanel;

    [Header("Buttons")]
    [SerializeField]
    private HamburgerMenuButton resumeButton;

    //[SerializeField]
    //private HamburgerMenuButton restartButton;

    [SerializeField]
    private HamburgerMenuButton exitButton;

    [Header("Map")]
    [SerializeField]
    private GameObject mapPanel;


    private YurtButtons currentLessonYurt;


    private void Start()
    {
        menuPanel.SetActive(false);

        resumeButton.Setup(
            "Resume",
            ResumeLesson
        );


        exitButton.Setup(
            "Exit",
            ExitToMainMenu
        );
    }


    // ВАЖНО: вызывать при старте урока
    public void SetCurrentLesson(
        YurtButtons yurt)
    {
        currentLessonYurt = yurt;
    }


    public void ToggleMenu()
    {
        menuPanel.SetActive(
            !menuPanel.activeSelf
        );
    }


    private void ResumeLesson()
    {
        menuPanel.SetActive(false);
    }


    private void RestartLesson()
    {
        Debug.Log(
            "[Hamburger] Restart lesson"
        );

        if (currentLessonYurt != null)
        {
            currentLessonYurt
                .RestartLesson();
        }
        else
        {
            Debug.LogWarning(
                "[Hamburger] No lesson set!"
            );
        }

        menuPanel.SetActive(false);
    }


    private void OpenMap()
    {
        Debug.Log(
            "[Hamburger] Open map"
        );

        if (mapPanel != null)
        {
            mapPanel.SetActive(true);
        }

        menuPanel.SetActive(false);
    }


    private void ExitToMainMenu()
    {
        Debug.Log(
            "[Hamburger] Exit to main menu"
        );

        SceneManager.LoadScene(
            "Main_Menu"
        );
    }
}