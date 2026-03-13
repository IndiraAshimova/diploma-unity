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

    [Header("Services")]
    public ProfileService profileService;
    public StreakService streakService;
    public XPService xpService;

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
        if (profileService != null)
            StartCoroutine(profileService.GetProfile(OnProfileLoaded));

        if (streakService != null)
            StartCoroutine(streakService.GetStreak());
    }

    private void OnProfileLoaded(UserProfileResponse profile)
    {
        welcomeText.text = "User: " + profile.username;
        PlayerModel.Instance.UpdatePlayerData(profile.xp, profile.level, profile.streak);
    }

    private void UpdateUI(int xp, int level, int streak)
    {
        xpText.text = "XP: " + xp;
        levelText.text = "Level: " + level;
        streakText.text = "Streak: " + streak;
    }

    public void AddXP(int amount)
    {
        if (xpService != null)
            StartCoroutine(xpService.AddXP(amount));
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}