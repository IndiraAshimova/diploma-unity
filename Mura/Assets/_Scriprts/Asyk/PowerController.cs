using UnityEngine;

public class PowerController : MonoBehaviour
{
    public float maxPower = 20f;
    public float speed = 15f;

    float power;
    bool charging;
    bool up = true;

    public void BeginAutoPower()
    {
        power = 0;
        charging = true;
        up = true;
    }

    void Update()
    {
        if (!charging) return;

        power += (up ? 1 : -1) * speed * Time.deltaTime;

        if (power >= maxPower) up = false;
        if (power <= 0) up = true;
    }

    public float StopCharging()
    {
        charging = false;
        return power;
    }

    public void ResetPower()
    {
        power = 0;
        charging = false;
    }
}