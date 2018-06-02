using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// this class is the controls the game behaviour for everything other than enemy spawning

public class GameController : MonoBehaviour {

    // instance is used to ensure there is only ever one game controller
    public static GameController instance;

    // gameOver is used by multiple game objects to determine behaviour in the update function
    [HideInInspector] public bool gameOver = false;

    // displays when the game is over, tells player to restart or view main menu
    public GameObject gameOverText;

    // player score
    private int score = 0;
    public Text scoreText;

    // highscore
    public Text highscoreText;

    // persistant data will be stored in gd
    GameDetails gd;

    // this function ensures that there is only ever one game controller
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
        // set mouse to invisible
        Cursor.visible = false;
        // load the saved browser data
        GameDetailContainer.LoadedGameDetails = DataAccess.Load();

        // if there is browser data, store it in gd
        // otherwise, create new browser data and store it in gd
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
        // restart game
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        // return user to main menu
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
