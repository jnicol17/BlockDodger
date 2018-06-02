using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;
    [HideInInspector] public bool gameOver = false;
    public GameObject gameOverText;

    private int score = 0;
    public Text scoreText;

    public Text highscoreText;
    GameDetails gd;

    public bool newHighScore = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization  
    void Start () {
        Cursor.visible = false;
        GameDetailContainer.LoadedGameDetails = DataAccess.Load();
        if (GameDetailContainer.LoadedGameDetails != null)
        {
            gd = GameDetailContainer.LoadedGameDetails;
        }
        else
        {
            gd = new GameDetails();
        }
        highscoreText.text = "Highscore: " + gd.highscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // 0 is "Left Click"
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (gameOver && Input.GetKeyDown("e"))
        {
            QuitGame();
        }
    }

    // called everytime the player scores
    // either from enemy hitting ground or player hitting bonus cube
    public void PlayerScored(int scoreMultiplier)
    {
        if (gameOver)
        {
            return;
        }

        score += scoreMultiplier;
        scoreText.text = "Score: " + score.ToString();

        if (score > gd.highscore)
        {
            gd.highscore = score;
            highscoreText.text = "Highscore: " + score.ToString();
        }

    }

    // called when the player gets hit by enemy (and dies)
    public void PlayerDied()
    {
        // this boolean is used in multiple classes update functions
        gameOver = true;
        highscoreText.text = "Highscore: " + gd.highscore.ToString();

        // save the game details (currently just the new/same highscore)
        DataAccess.Save(gd);

        // text that tells user to restart or return to main menu 
        gameOverText.SetActive(true);
        // set mouse to visible
        Cursor.visible = true;
    }

    // return to main menu
    private void QuitGame()
    {
        SceneManager.LoadScene("Menu");
    }

}
