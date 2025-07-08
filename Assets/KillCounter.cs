using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public int killCount = 0;
    public TMP_Text killCountText;

    void Start()
    {
        UpdateKillText();
    }

    public void AddKill()
    {
        killCount++;
        UpdateKillText();
    }

    void UpdateKillText()
    {
        if (killCountText != null)
        {
            killCountText.text = "Kills: " + killCount;
        }
    }
}