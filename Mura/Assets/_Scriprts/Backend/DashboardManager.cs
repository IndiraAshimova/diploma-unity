using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class DashboardManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text welcomeText;
    public TMP_Text xpText;
    public TMP_Text levelText;
    public TMP_Text streakText;

    private ServiceFactory services;

    private void OnEnable()
    {
        Debug.Log("DashboardManager: OnEnable");

        if (PlayerModel.Instance != null)
        {
            PlayerModel.Instance.OnPlayerDataUpdated += UpdateUI;
        }
        else
        {
            Debug.LogError("PlayerModel.Instance == null");
        }
    }

    private void OnDisable()
    {
        if (PlayerModel.Instance != null)
            PlayerModel.Instance.OnPlayerDataUpdated -= UpdateUI;
    }

    private void Awake()
    {
        Debug.Log("DashboardManager: Awake");

        services = new ServiceFactory();

        Debug.Log("ServiceFactory создан");
    }

    private void Start()
    {
        Debug.Log("DashboardManager: Start");

        Debug.Log("Запрос профиля...");

        StartCoroutine(
            services.Profile.GetProfile(OnProfileLoaded));

        Debug.Log("Запрос streak...");

        StartCoroutine(
            services.Streak.GetStreak(OnStreakLoaded));
    }

    private void OnProfileLoaded(UserProfileResponse profile)
    {
        Debug.Log(
            $"Profile Loaded: {profile.username} XP={profile.xp}");

        welcomeText.text =
            "User: " + profile.username;

        PlayerModel.Instance.UpdatePlayerData(
            profile.xp,
            profile.level,
            profile.streak);
    }

    private void OnStreakLoaded(int streak)
    {
        Debug.Log(
            $"Streak Loaded: {streak}");
    }

    private void UpdateUI(
        int xp,
        int level,
        int streak)
    {
        Debug.Log(
            $"UpdateUI XP={xp} Level={level} Streak={streak}");

        xpText.text =
            " " + xp;

        levelText.text =
            "Level: " + level;

        streakText.text =
            "Streak: " + streak;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}