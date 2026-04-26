using UnityEngine;
using System.Collections;

public enum TurnOwner
{
    Player,
    Bot
}

public class TurnManager : MonoBehaviour
{
    public ThrowController playerThrow;
    public ThrowController botThrow;
    public BotController bot;

    public TurnOwner currentTurn = TurnOwner.Player;

    private bool turnLocked;

    void Start()
    {
        currentTurn = TurnOwner.Player;
        Debug.Log("[TURN] START ? Player");
        StartTurn();
    }

    public bool IsPlayerTurn()
    {
        return currentTurn == TurnOwner.Player && !turnLocked;
    }

    public bool IsLocked()
    {
        return turnLocked;
    }

    public void NextTurn(bool hit)
    {
        Debug.Log($"[TURN] NextTurn | hit = {hit} | current = {currentTurn}");

        if (hit)
        {
            Debug.Log("[TURN] HIT ? SAME PLAYER");
            StartTurn();
            return;
        }

        currentTurn = (currentTurn == TurnOwner.Player)
            ? TurnOwner.Bot
            : TurnOwner.Player;

        Debug.Log("[TURN] SWITCH ? " + currentTurn);

        StartTurn();
    }

    void StartTurn()
    {
        StopAllCoroutines();
        turnLocked = true;

        Debug.Log("[TURN] Start ? " + currentTurn);

        if (currentTurn == TurnOwner.Bot)
        {
            StartCoroutine(BotTurn());
        }
        else
        {
            StartCoroutine(PlayerTurn());
        }
    }

    IEnumerator PlayerTurn()
    {
        yield return null;
        turnLocked = false;

        Debug.Log("[TURN] PLAYER READY");
    }

    IEnumerator BotTurn()
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("[BOT] MAKE TURN");
        bot.MakeTurn();

        turnLocked = false;
    }

    public ThrowController GetActiveThrower()
    {
        return currentTurn == TurnOwner.Player ? playerThrow : botThrow;
    }
}