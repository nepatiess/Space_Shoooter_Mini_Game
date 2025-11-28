using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gameUIPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TextMeshProUGUI scoreText;
    // [SerializeField] private Image[] healthImages; bi sýkýntý var

    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        HideAllPanels();
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
    }

    public void ShowGameUI()
    {
        HideAllPanels();
        if (gameUIPanel != null)
            gameUIPanel.SetActive(true);
    }

    public void ShowPauseUI()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void HidePauseUI()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    public void ShowGameOverUI()
    {
        HideAllPanels();
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            if (finalScoreText != null)
                finalScoreText.text = "Score: " + GameManager.Instance.GetCurrentScore();

            if (highScoreText != null)
                highScoreText.text = "High Score: " + GameManager.Instance.GetHighScore();
        }
    }

    void HideAllPanels()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (gameUIPanel != null) gameUIPanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    /*
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (healthImages == null) return;

        for (int i = 0; i < healthImages.Length; i++)
        {
            if (i < maxHealth)
            {
                healthImages[i].gameObject.SetActive(true);
                healthImages[i].enabled = (i < currentHealth);
            }
            else
            {
                healthImages[i].gameObject.SetActive(false);
            }
        }
    }
    */

    // Buton fonksiyonlarý
    public void OnPlayButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
        }
    }

    public void OnResumeButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResumeGame();
        }
    }

    public void OnRestartButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
    }

    public void OnQuitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
