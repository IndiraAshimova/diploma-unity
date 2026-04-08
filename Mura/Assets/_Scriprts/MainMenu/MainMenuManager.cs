using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text xpText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text streakText;

    [Header("Panels")]
    [SerializeField] private GameObject lessonsPanel;
    [SerializeField] private GameObject categoryPanel;

    [Header("UI Manager")]
    [SerializeField] private UIMainMenuManager uiManager;

    [Header("Services")]
    private ServiceFactory services;

    private void Awake()
    {
        Debug.Log("MainMenuManager Awake");

        services =
            new ServiceFactory();
    }

    private void OnEnable()
    {
        if (PlayerModel.Instance != null)
            PlayerModel.Instance.OnPlayerDataUpdated += UpdateUI;
    }

    private void OnDisable()
    {
        if (PlayerModel.Instance != null)
            PlayerModel.Instance.OnPlayerDataUpdated -= UpdateUI;
    }

    private void Start()
    {
        if (uiManager == null)
            uiManager = GetComponent<UIMainMenuManager>();
        CloseCategory();

        StartCoroutine(
            services.Profile.GetProfile(OnProfileLoaded));
        StartCoroutine(
            services.Streak.GetStreak());
    }

    private void OnProfileLoaded(UserProfileResponse profile)
    {
        PlayerModel.Instance.UpdatePlayerData(profile.xp, profile.level, profile.streak);
    }

    private void UpdateUI(int xp, int level, int streak)
    {
        xpText.text = "XP: " + xp;
        levelText.text = "Level: " + level;
        streakText.text = "Streak: " + streak;
    }

    private void OpenCategory(Category category)
    {
        categoryPanel.SetActive(true);
        lessonsPanel.SetActive(true);
        uiManager.ShowCategory(category);
    }

    public void OpenBeginner() => OpenCategory(Category.Beginner);
    public void OpenTeen() => OpenCategory(Category.Teen);
    public void OpenAdvanced() => OpenCategory(Category.Advanced);

    public void BackToMainMenu() => CloseCategory();
    private void CloseCategory() => categoryPanel.SetActive(false);

    public void LoadLessonScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenProfile()
    {
        SceneManager.LoadScene("Dashboard");
    }
}