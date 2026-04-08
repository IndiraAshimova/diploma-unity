using System;
using System.Collections;
using UnityEngine;

public class AsykResetSystem : MonoBehaviour
{
    public Rigidbody2D rb;
    public ArrowController arrow;
    public PowerController power;
    public AsykThrowController throwController;

    [Header("Settings")]
    public float stopVelocity = 0.1f;
    public float stopTimeRequired = 3f;
    public float resetDelay = 1f;

    private Vector2 startPosition;
    private float startRotation;

    private float stopTimer = 0f;
    private bool hasStopped = false;
    private bool isTracking = false;

    // Коллбек на завершение хода
    public Action OnStop;

    void Start()
    {
        startPosition = rb.position;
        startRotation = rb.rotation;
    }

    void Update()
    {
        if (isTracking)
            CheckIfStopped();
    }

    public void StartTracking()
    {
        stopTimer = 0f;
        hasStopped = false;
        isTracking = true;
    }

    void CheckIfStopped()
    {
        if (rb.linearVelocity.magnitude < stopVelocity)
        {
            stopTimer += Time.deltaTime;
            if (stopTimer >= stopTimeRequired && !hasStopped)
            {
                hasStopped = true;
                StartCoroutine(ResetAfterDelay());
            }
        }
        else
        {
            stopTimer = 0f;
        }
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);
        ResetAsyk();

        OnStop?.Invoke(); // передаем ход
    }

    void ResetAsyk()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.position = startPosition;
        rb.rotation = startRotation;

        arrow.ResetArrow();
        power.ResetPower();
        throwController.ResetThrow();

        stopTimer = 0f;
        hasStopped = false;
        isTracking = false;
    }
}