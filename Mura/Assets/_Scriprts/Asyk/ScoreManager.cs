using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int playerScore;
    public int botScore;

    public void AddScore(bool isPlayer)
    {
        if (isPlayer)
            playerScore++;
        else
            botScore++;

        Debug.Log($"Player: {playerScore} | Bot: {botScore}");
    }
}