using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI gameOverScoreText;
    [SerializeField] TextMeshProUGUI gameOverBestText;

    private float totalTime;

    public bool gameOver = false;
    private int score;

    void Awake()
    {
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            totalTime += Time.deltaTime;
            timeText.text = "Time: " + System.TimeSpan.FromSeconds(totalTime).ToString(@"mm\:ss\:fff");
        }
        if (gameOver && Input.GetKeyDown(KeyCode.Return))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOver = true;
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
        gameOverScoreText.text = "Score: " + score;
        gameOverBestText.text = "Best Score: " + PlayerPrefs.GetInt("BestScore", 0);
        gameOverPanel.SetActive(true);
    }
}
