using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [Header("Angle Settings")]
    public float minAngle = 20f;
    public float maxAngle = 160f;
    public float angleSpeed = 100f;

    [Header("References")]
    public PowerController power;

    private float currentAngle = 90f;
    private bool movingUp = true;
    private bool isLocked = false;

    void Update()
    {
        if (!isLocked)
        {
            MoveArrow();
        }

        // Выбор угла
        if (Input.GetMouseButtonDown(0) && !isLocked)
        {
            LockArrow();
        }
    }

    void MoveArrow()
    {
        currentAngle += (movingUp ? 1 : -1) * angleSpeed * Time.deltaTime;

        if (currentAngle >= maxAngle)
            movingUp = false;
        if (currentAngle <= minAngle)
            movingUp = true;

        UpdateRotation();
    }

    void UpdateRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    public Vector2 GetDirection()
    {
        float rad = currentAngle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    public void LockArrow()
    {
        isLocked = true;
        if (power != null)
            power.BeginAutoPower();
    }

    public void ResetArrow()
    {
        isLocked = false;
        currentAngle = 90f;
        movingUp = true;
        UpdateRotation();
    }

    public bool IsLocked()
    {
        return isLocked;
    }

    public void SetAngle(float angle)
    {
        currentAngle = Mathf.Clamp(angle, minAngle, maxAngle);
        UpdateRotation();
    }
}