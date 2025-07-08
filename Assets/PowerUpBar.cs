using UnityEngine;
using UnityEngine.UI;

public class PowerUpBar : MonoBehaviour
{
    public Image fillImage;
    public float currentCharge = 0f;
    public float maxCharge = 10f;
    public bool isFull => currentCharge >= maxCharge;

    public void AddCharge(float amount)
    {
        currentCharge += amount;
        currentCharge = Mathf.Clamp(currentCharge, 0f, maxCharge);
        UpdateUI();
    }

    public void ResetCharge()
    {
        currentCharge = 0f;
        UpdateUI();
    }

    void UpdateUI()
    {
        fillImage.fillAmount = currentCharge / maxCharge;
    }
}