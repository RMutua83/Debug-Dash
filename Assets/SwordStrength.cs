using UnityEngine;

public class SwordStrength : MonoBehaviour
{
    public float baseDamage = 1f;

    public void IncreaseStrength(float amount)
    {
        baseDamage += amount;
        Debug.Log("Sword damage increased: " + baseDamage);
    }

    public float GetDamage()
    {
        return baseDamage;
    }
}
