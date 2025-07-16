using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogicScript : MonoBehaviour
{
    public static LogicScript instance;
    public GameObject gameOverCanvas;
    public Text currentScoreText;
    public Text highScoreText;
    private int score;
    public int Score
    {
        get { return score; }
        private set
        {
            score = value;
            currentScoreText.text = score.ToString();
            UpdateHighScore();
        }
    }

    public bool isGameOver = false;
    public bool gameHasStarted = false;
    public GameObject backToMenuButton;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Score = 0;
        try
        {
            highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error loading HighScore from PlayerPrefs: " + e.Message);
            highScoreText.text = "Error";
        }
        gameOverCanvas.SetActive(false);
        if (backToMenuButton != null) backToMenuButton.SetActive(false);

        Time.timeScale = 1;
        isGameOver = false;
        gameHasStarted = false;
    }

    public void UpdateScore()
    {
        Score++;
    }

    private void UpdateHighScore()
    {
        int currentHighScore = 0;
        try
        {
            currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error getting HighScore for comparison: " + e.Message);
        }

        if (Score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", Score);
            highScoreText.text = Score.ToString();
        }
    }
    public void playAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void newGame()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void gameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        gameHasStarted = false;
        UpdateHighScore();
        gameOverCanvas.SetActive(true);

        if (backToMenuButton != null) backToMenuButton.SetActive(true);
        StartCoroutine(FreezeTimeAfterDelay(0.7f));
    }
    IEnumerator FreezeTimeAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 0;
    }
    public void StartTheGame()
    {
        gameHasStarted = true;
    }
    public void BackToCharacterSelect()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Characters");
    }
}