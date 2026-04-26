using UnityEngine;

public class PlayerThrowInput : MonoBehaviour
{
    public ThrowController throwController;
    public TurnManager turnManager;

    private bool canThrow = true;

    void Update()
    {
        if (turnManager == null) return;

        if (turnManager.currentTurn != TurnOwner.Player) return;
        if (turnManager.IsLocked()) return;

        if (Input.GetMouseButtonDown(0))
        {
            ThrowForward();
        }
    }

    void ThrowForward()
    {
        canThrow = false;

        Debug.Log("[PLAYER] Throw");

        throwController.owner = TurnOwner.Player;
        throwController.Throw(Vector2.right);
    }

    void OnEnable()
    {
        throwController.OnThrowFinished += ResetInput;
    }

    void OnDisable()
    {
        throwController.OnThrowFinished -= ResetInput;
    }

    void ResetInput()
    {
        canThrow = true;
    }
}