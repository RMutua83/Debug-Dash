using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    [Header("Game Over UI")]
    public GameObject gameOverCanvas;
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        // Make sure the GameOver UI is hidden at start
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    public void ShowGameOver()
    {
        // Show the canvas
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }

        // Update the score text
        if (finalScoreText != null)
        {
            int finalScore = 0;

            if (ScoreManager.Instance != null)
            {
                finalScore = ScoreManager.Instance.currentScore;
            }

            finalScoreText.text = "Your Score: " + finalScore;
        }

        // Start coroutine to delay freezing
        StartCoroutine(FreezeAfterDelay(15f));
    }

    IEnumerator FreezeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}