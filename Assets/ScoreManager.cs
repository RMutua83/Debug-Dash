using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    [Header("Scoring")]
    public int currentScore = 0;
    private int highScore = 0;

    [Header("High Score Alert")]
    public GameObject highScoreAlert;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (scoreText == null)
            scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();

        if (highScoreText == null)
            highScoreText = GameObject.Find("HighScoreText")?.GetComponent<TextMeshProUGUI>();

        if (highScoreAlert == null)
            highScoreAlert = GameObject.Find("HighScoreAlert");

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log("Loaded high score from PlayerPrefs: " + highScore);
        UpdateUI();

        if (highScoreAlert != null)
            highScoreAlert.SetActive(false);
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log("Added " + amount + " points. Current score: " + currentScore);

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            Debug.Log("New high score saved: " + highScore);
            ShowHighScoreAlert();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;

        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
        Debug.Log("Score reset to 0.");
        UpdateUI();
    }

    public void ResetHighScore()
    {
        highScore = 0;
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
        Debug.Log("High score deleted from PlayerPrefs.");
        UpdateUI();
    }

    void ShowHighScoreAlert()
    {
        if (highScoreAlert != null)
        {
            highScoreAlert.SetActive(true);
            StartCoroutine(HideHighScoreAlert());
        }
    }

    IEnumerator HideHighScoreAlert()
    {
        yield return new WaitForSeconds(3f);
        if (highScoreAlert != null)
            highScoreAlert.SetActive(false);
    }
}
