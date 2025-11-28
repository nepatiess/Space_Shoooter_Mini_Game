using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool isGameActive = false;
    private bool isPaused = false;

    private int currentScore = 0;
    private int highScore = 0;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadHighScore();
    }

    void Update()
    {
        // ESC tuþu ile pause
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        isPaused = false;
        currentScore = 0;
        Time.timeScale = 1f;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowGameUI();
            UIManager.Instance.UpdateScore(currentScore);
        }

        Debug.Log("Oyun baþladý!");
    }

    public void GameOver()
    {
        isGameActive = false;

        // High score kontrolü
        if (currentScore > highScore)
        {
            highScore = currentScore;
            SaveHighScore();
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowGameOverUI();
        }

        /*
        // Ses çal
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("gameover");
        }
        */

        Debug.Log("Oyun bitti! Skor: " + currentScore);
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowPauseUI();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.HidePauseUI();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    public void AddScore(int points)
    {
        currentScore += points;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateScore(currentScore);
        }
    }

    public bool IsGameActive()
    {
        return isGameActive && !isPaused;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
