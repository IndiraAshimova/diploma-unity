using UnityEngine;
using System;

public class PlayerModel : MonoBehaviour
{
    public static PlayerModel Instance;

    public string playerName;
    public int xp;
    public int level;
    public int streak;

    // Событие для UI: xp, level, streak
    public event Action<int, int, int> OnPlayerDataUpdated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Обновление XP и уровня
    public void UpdateXP(int newXp, int newLevel)
    {
        xp = newXp;
        level = newLevel;
        OnPlayerDataUpdated?.Invoke(xp, level, streak);
        Debug.Log($"XP обновлён: XP={xp} Level={level}");
    }

    // Отдельное обновление streak
    public void UpdateStreak(int newStreak)
    {
        streak = newStreak;
        OnPlayerDataUpdated?.Invoke(xp, level, streak);
        Debug.Log($"Streak обновлён: Streak={streak}");
    }

    // Единый метод для обновления всех данных (опционально)
    public void UpdatePlayerData(int newXp, int newLevel, int newStreak)
    {
        xp = newXp;
        level = newLevel;
        streak = newStreak;
        OnPlayerDataUpdated?.Invoke(xp, level, streak);
        Debug.Log($"Player updated: XP={xp} Level={level} Streak={streak}");
    }

    public int GetXP() => xp;
    public int GetLevel() => level;
    public int GetStreak() => streak;
}