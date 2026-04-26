using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float minAngle = 20f;
    public float maxAngle = 160f;
    public float speed = 100f;

    float angle = 90f;
    bool up = true;
    bool active;

    void Update()
    {
        if (!active) return;

        angle += (up ? 1 : -1) * speed * Time.deltaTime;

        if (angle > maxAngle) up = false;
        if (angle < minAngle) up = true;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Enable() => active = true;
    public void Disable() => active = false;

    public float GetAngle() => angle;
}