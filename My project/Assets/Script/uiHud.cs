using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uiHud : MonoBehaviour
{
    GameManager gameManager;

    public TextMeshProUGUI scoreText;

    public Slider playerHpBar;

    public float fillamount;

    public GameObject popUp;
    
    
    private bool isPop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(gameManager.IsGameOver)
        // {

        // }
        if (Input.GetKeyDown(KeyCode.Escape) && !isPop)
        {
            Time.timeScale = 0;
            popUp.SetActive(true);
            isPop = true;
        }  

        scoreText.text = "Score: " + gameManager.score.ToString();
    }

    public void OnClickExit()
    {
        popUp.SetActive(false);
        isPop = false;
        Application.Quit();
    }

    public void OnClickResume()
    {
        Time.timeScale = 1;
        popUp.SetActive(false);
        isPop = false;
    }
}
