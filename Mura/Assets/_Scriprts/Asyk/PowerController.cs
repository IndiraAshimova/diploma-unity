using UnityEngine;
using UnityEngine.UI;

public class PowerController : MonoBehaviour
{
    [Header("Power Settings")]
    public float maxPower = 20f;
    public float powerSpeed = 15f;

    [Header("UI")]
    public Image powerBar;

    private float currentPower;
    private bool isCharging = false;
    private bool isIncreasing = true;

    void Update()
    {
        if (isCharging)
            ChargePower();
    }

    void ChargePower()
    {
        currentPower += (isIncreasing ? 1 : -1) * powerSpeed * Time.deltaTime;

        if (currentPower >= maxPower)
        {
            currentPower = maxPower;
            isIncreasing = false;
        }
        else if (currentPower <= 0)
        {
            currentPower = 0;
            isIncreasing = true;
        }

        UpdateUI();
    }

    // Сделаем public, чтобы можно было вызывать из AsykThrowController
    public void UpdateUI()
    {
        if (powerBar != null)
            powerBar.fillAmount = currentPower / maxPower;
    }

    public void BeginAutoPower()
    {
        currentPower = 0;
        isIncreasing = true;
        isCharging = true;
        UpdateUI();
    }

    public float StopCharging()
    {
        isCharging = false;
        return currentPower;
    }

    public void ResetPower()
    {
        currentPower = 0;
        isCharging = false;
        isIncreasing = true;
        UpdateUI();
    }
}