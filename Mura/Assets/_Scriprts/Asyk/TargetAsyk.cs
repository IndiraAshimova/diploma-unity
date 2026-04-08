using System.Collections;
using UnityEngine;

public class TargetAsyk : MonoBehaviour
{
    public int pointValue = 1;
    public float requiredDistance = 2.0f;

    private Vector2 startPosition;
    private float startRotation;
    private Rigidbody2D rb;

    private bool isTaken = false;
    private bool isReturning = false;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.eulerAngles.z;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isTaken) return;

        CheckDistance();
        CheckReturn();
    }

    void CheckDistance()
    {
        if (Vector2.Distance(startPosition, transform.position) >= requiredDistance)
        {
            TakeAsyk();
        }
    }

    void CheckReturn()
    {
        if (isTaken || isReturning) return;

        // Порог скорости выше 0.1f, чтобы не срабатывало на дрожание
        if (rb.linearVelocity.magnitude < 0.2f)
        {
            if (Vector2.Distance(startPosition, transform.position) < requiredDistance)
            {
                ReturnToStart();
            }
        }
    }

    void ReturnToStart()
    {
        if (isReturning) return;

        isReturning = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        transform.position = startPosition;
        transform.rotation = Quaternion.Euler(0, 0, startRotation);

        StartCoroutine(ResetReturnFlag());
    }

    IEnumerator ResetReturnFlag()
    {
        yield return new WaitForSeconds(0.2f);
        isReturning = false;
    }

    void TakeAsyk()
    {
        if (isTaken) return;

        isTaken = true;
        ScoreManager.Instance.AddScore(pointValue);
        gameObject.SetActive(false);

        // Передаем ход после выбивания
        TurnManager.Instance.EndTurn();
    }
}