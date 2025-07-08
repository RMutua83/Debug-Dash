using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Load the Main Menu scene
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0); // Main Menu
    }

    // Load the Neon City scene
    public void LoadNeonCity()
    {
        SceneManager.LoadScene(1); // Neon City
    }

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
