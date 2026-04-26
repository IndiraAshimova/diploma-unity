using UnityEngine;

public class BotController : MonoBehaviour
{
    public ThrowController throwController;

    public void MakeTurn()
    {
        Debug.Log("[BOT] MakeTurn START");

        throwController.owner = TurnOwner.Bot;

        Vector2 direction = Vector2.left;

        direction.x += Random.Range(-0.2f, 0.2f);
        direction.y += Random.Range(-0.2f, 0.2f);

        Debug.Log("[BOT] Throw ? " + direction);

        throwController.Throw(direction);
    }
}