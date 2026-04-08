using UnityEngine;

public class AsykThrowController : MonoBehaviour
{
    public Rigidbody2D rb;
    public ArrowController arrow;
    public PowerController power;
    public AsykResetSystem resetSystem;

    private bool angleLocked = false;
    private bool hasThrown = false;

    void Update()
    {
        if (!angleLocked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                arrow.LockArrow();
                angleLocked = true;
                power.BeginAutoPower();
            }
        }
        else if (!hasThrown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                float finalPower = power.StopCharging();

                // Коллбек на завершение хода
                resetSystem.OnStop = () => TurnManager.Instance.EndTurn();

                Throw(finalPower);
                hasThrown = true;
            }
        }
    }

    void Throw(float powerValue)
    {
        Vector2 direction = arrow.GetDirection();
        rb.AddForce(direction * powerValue, ForceMode2D.Impulse);
        resetSystem.StartTracking();
    }

    public void ResetThrow()
    {
        angleLocked = false;
        hasThrown = false;
        arrow.ResetArrow();
        power.ResetPower();
    }
}