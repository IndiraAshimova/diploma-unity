using UnityEngine;
using System.Collections;

public class BotThrowController : MonoBehaviour
{
    public Rigidbody2D rb;
    public ArrowController arrow;
    public PowerController power;
    public AsykResetSystem resetSystem;

    public float minAngle = 30f;
    public float maxAngle = 150f;
    public float minPower = 5f;
    public float maxPower = 15f;
    public float reactionTime = 1.0f;

    private bool hasThrown = false;

    public void StartBotTurn()
    {
        if (hasThrown) return;
        StartCoroutine(BotThrowRoutine());
    }

    IEnumerator BotThrowRoutine()
    {
        float botAngle = Random.Range(minAngle, maxAngle);
        arrow.SetAngle(botAngle);
        arrow.LockArrow();

        float botPower = Random.Range(minPower, maxPower);

        yield return new WaitForSeconds(reactionTime);

        Vector2 direction = arrow.GetDirection();
        resetSystem.OnStop = () => TurnManager.Instance.EndTurn();

        rb.AddForce(direction * botPower, ForceMode2D.Impulse);
        resetSystem.StartTracking();

        hasThrown = true;
    }

    public void ResetBot()
    {
        hasThrown = false;
        arrow.ResetArrow();
        power.ResetPower();
    }
}