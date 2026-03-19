using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public int totalItems = 4; // сколько нужно правильно собрать
    private int currentCorrect = 0;

    public MiniGameStep step;

    public void RegisterCorrect()
    {
        currentCorrect++;
        Debug.Log($"Правильно: {currentCorrect}/{totalItems}");

        if (currentCorrect >= totalItems)
        {
            Debug.Log("Мини-игра завершена");
            step.FinishMiniGame();
        }
    }
}