using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public bool IsGameOver = false;
    private float gameOverTimer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        if (IsGameOver)
        {
            gameOverTimer += Time.unscaledDeltaTime;
            if (gameOverTimer >= 3f)
            {
                Time.timeScale = 0f;
                StartCoroutine("RestartGame", 3000f);
            }
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
        gameOverTimer = 0f; // 타이머 초기화
    }

    public void RestartGame()
    {
        IsGameOver = false;
        gameOverTimer = 0f;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int amount)
    {
        score += amount;
    }
}