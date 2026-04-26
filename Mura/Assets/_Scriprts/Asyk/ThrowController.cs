using UnityEngine;
using System;

public class ThrowController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform startPoint;
    public float throwForce = 8f;

    public TurnOwner owner;

    public Action OnThrowFinished;

    private bool isMoving;
    private bool finished;

    public void Throw(Vector2 direction)
    {
        if (isMoving) return;

        isMoving = true;
        finished = false;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;

        rb.AddForce(direction.normalized * throwForce, ForceMode2D.Impulse);
    }

    void Update()
    {
        if (!isMoving || finished) return;

        if (rb.linearVelocity.magnitude < 0.1f)
        {
            isMoving = false;

            if (finished) return;
            finished = true;

            OnThrowFinished?.Invoke();

            ReturnToStart();
        }
    }

    void ReturnToStart()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        transform.position = startPoint.position;
    }
}