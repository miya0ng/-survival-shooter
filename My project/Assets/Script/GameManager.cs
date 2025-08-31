using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public bool IsGameOver = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void GameOver()
    {
        IsGameOver = true;
        // Additional game over logic can be added here
    }

    public void RestartGame()
    {
        IsGameOver = false;
       // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Additional restart logic can be added here
    }
}
