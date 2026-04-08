using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LessonProgressData
{
    public bool completed;
    public bool firstWinDone;
    public int bestScore;
    public int attempts;
}

public enum LessonResult
{
    Win,
    Partial,
    Lose
}

public class LessonProgressManager : MonoBehaviour
{
    public static LessonProgressManager Instance;

    private Dictionary<int, LessonProgressData> ProgressDict = new();
    private IXPService xpService; // ссылка на сервис XP

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadProgress();
    }

    // Метод для передачи внешнего XPService (например, через ServiceFactory)
    public void Initialize(IXPService service)
    {
        xpService = service;
    }

    public LessonProgressData GetLessonProgress(int lessonIndex)
    {
        if (!ProgressDict.ContainsKey(lessonIndex))
            ProgressDict[lessonIndex] = new LessonProgressData();

        return ProgressDict[lessonIndex];
    }

    // Метод завершения урока, возвращает XP
    public int HandleLessonEnd(int lessonIndex, int score, LessonResult result)
    {
        var progress = GetLessonProgress(lessonIndex);

        int xp = CalculateXP(progress, result);


        if (result == LessonResult.Win && !progress.firstWinDone)
        {
            progress.firstWinDone = true;
            progress.completed = true;
        }

        if (score > progress.bestScore)
            progress.bestScore = score;

        progress.attempts++;
        SaveProgress();

        GrantXP(xp); // начисляем XP через XPService
        return xp;
    }

    private int CalculateXP(LessonProgressData progress, LessonResult result)
    {
        bool isFirstWin = !progress.firstWinDone;

        switch (result)
        {
            case LessonResult.Win:
                return isFirstWin ? 150 : 50;

            case LessonResult.Partial:
                return 25;

            case LessonResult.Lose:
                return 5;
        }

        return 0;
    }

    private void GrantXP(int amount)
    {
        if (xpService != null)
        {
            StartCoroutine(xpService.AddXP(amount, response =>
            {
                Debug.Log($"XP начислено: {response.xp} XP, Level {response.level}, Streak {response.streak}");
            }));
        }
        else
        {
            Debug.LogWarning("XPService не инициализирован!");
        }
    }

    public void SaveProgress()
    {
        string json = JsonUtility.ToJson(new SerializationWrapper(ProgressDict));
        PlayerPrefs.SetString("LessonProgress", json);
        PlayerPrefs.Save();
    }

    public void LoadProgress()
    {
        string json = PlayerPrefs.GetString("LessonProgress", "");
        if (!string.IsNullOrEmpty(json))
        {
            var wrapper = JsonUtility.FromJson<SerializationWrapper>(json);
            ProgressDict = wrapper.ToDictionary();
        }
    }

    [System.Serializable]
    private class SerializationWrapper
    {
        public List<int> keys = new();
        public List<LessonProgressData> values = new();

        public SerializationWrapper(Dictionary<int, LessonProgressData> dict)
        {
            keys = new List<int>(dict.Keys);
            values = new List<LessonProgressData>(dict.Values);
        }

        public Dictionary<int, LessonProgressData> ToDictionary()
        {
            var dict = new Dictionary<int, LessonProgressData>();
            for (int i = 0; i < keys.Count; i++)
                dict[keys[i]] = values[i];
            return dict;
        }
    }
}