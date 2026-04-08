using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public AsykThrowController playerThrow;
    public BotThrowController botThrow;
    public TossAsyk tossAsyk;

    private bool isPlayerTurn = true;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        tossAsyk.Toss();
        DecideFirstTurn();
        StartTurn();
    }

    void DecideFirstTurn()
    {
        if (tossAsyk.tossResult == AsykPosition.Alshy ||
            tossAsyk.tossResult == AsykPosition.Taike)
            isPlayerTurn = true;
        else
            isPlayerTurn = false;

        Debug.Log(isPlayerTurn ? "Игрок ходит первым" : "Бот ходит первым");
    }

    void StartTurn()
    {
        playerThrow.gameObject.SetActive(isPlayerTurn);
        botThrow.gameObject.SetActive(!isPlayerTurn);

        if (isPlayerTurn)
        {
            playerThrow.gameObject.SetActive(true);
            playerThrow.ResetThrow(); // сброс UI и состояния

            // Включаем и сбрасываем стрелку игрока
            playerThrow.arrow.ResetArrow();
            playerThrow.arrow.gameObject.SetActive(true);
        }
        else
        {
            botThrow.gameObject.SetActive(true);
            botThrow.StartBotTurn();

            // Скрываем стрелку бота
            botThrow.arrow.gameObject.SetActive(false);
        }
    }

    // Передача хода
    public void EndTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        StartTurn();
    }

    // Проверка, чей сейчас ход
    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}