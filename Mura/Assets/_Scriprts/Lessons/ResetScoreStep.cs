using System;
using UnityEngine;

public class ResetScoreStep : MonoBehaviour, ILessonStep
{
    public LevelScoreManager levelScore;

    public void Execute(Action onComplete)
    {
        if (levelScore != null)
        {
            Debug.Log("[Score] ResetScore");

            levelScore.ResetScore();
        }

        onComplete?.Invoke();
    }
}