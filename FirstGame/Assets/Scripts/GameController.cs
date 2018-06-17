using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshProUGUI scoreText;

    // highscore
    public TextMeshProUGUI highscoreText;

    // persistant data will be stored in gd
    GameDetails gd;

    // text that is displayed when there is a new highscore
    private int oldHighScore = -1;
    public GameObject newHighScoreText;
    public Text oldScoreText;
    public Text newScoreText;

    // default 1, set to 2 when powerup got
    [HideInInspector]
    public int multiplier = 1;

    // these timers are used when the player gets a powerup
    [HideInInspector]
    public float multiplierTime = -1f;
    [HideInInspector]
    public float minimizeTime = -1f;

    // player instance that we can deactivate if minimize powerup is used
    //public Player player;

    // used to disable player
    private bool disablePlayer = true;

    public Text powerUpText;

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
        // 0 for no sync, 1 for panel refresh rate, 2 for 1/2 panel rate
        //QualitySettings.vSyncCount = 1;
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

        // reset powerup timers everytime game is reloaded
        minimizeTime = -1f;
        multiplierTime = -1f;

        // set highscore text in bottom corner to current saved high score
        highscoreText.text = "Highscore: " + gd.highscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // if player died
        if (gameOver)
        {
            // 0 is "Left Click"
            // restart game
            //if (gameOver && Input.GetMouseButtonDown(0))
            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.instance.PlayThemeSong("Theme");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            // return user to main menu
            //if (gameOver && Input.GetKeyDown("e"))
            if (Input.GetKeyDown("e"))
            {
                AudioManager.instance.PlayThemeSong("Theme");
                QuitGame();
            }
        }

        else
        {
            powerUpText.text = "";
            // multiplier time is only > Time.time if a powerup is used, 15 second timer
            if (multiplierTime > Time.time && multiplierTime > minimizeTime)
            {
                // set score multiplier to 2x
                multiplier = 2;
                powerUpText.text = "DOUBLE POINTS";
            }
            // reset score multiplier after 15 seconds
            else if (multiplierTime < Time.time && multiplierTime != -1f)
            {
                multiplier = 1;
                //powerUpText.text = "";
            }

            // minimize time is only > Time.time if a powerup is used, 15 second timer
            if (minimizeTime > Time.time && minimizeTime > multiplierTime)
            {
                // if the player is not disabled, disable the player
                if (disablePlayer)
                {
                    Player.instance.gameObject.SetActive(false);
                    disablePlayer = false;
                }
                powerUpText.text = "SMALL PLAYER";
            }
            // reenable the player
            else if (minimizeTime < Time.time && minimizeTime != -1f)
            {
                Player.instance.gameObject.SetActive(true);
                disablePlayer = true;
                Player.instance.transform.position = new Vector2(Player.instance.getXPosition(), Player.instance.playerY);
                //powerUpText.text = "";
            }
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

        // update the score text box at the bottom of the screen
        score += scoreMultiplier * multiplier;
        scoreText.text = "Score: " + score.ToString();

        if (score > gd.highscore)
        {
            // we will save the old highscore once in oldHighScore and display extra text on game over
            if (oldHighScore == -1)
            {
                oldHighScore = gd.highscore;
            }

            // update the highscore text box in the bottom right corner with the current score
            gd.highscore = score;
            highscoreText.text = "Highscore: " + score.ToString();
        }

    }

    // called when the player gets hit by enemy (and dies)
    public void PlayerDied()
    {

        // remove powerup text
        powerUpText.text = "";

        // this boolean is used in multiple classes update functions
        gameOver = true;
        highscoreText.text = "Highscore: " + gd.highscore.ToString();

        // save the game details (currently just the new/same highscore)
        DataAccess.Save(gd);

        // text that tells user to restart or return to main menu 
        gameOverText.SetActive(true);

        // if oldHighScore is not -1 then it was set, only gets set if we have a new highScore
        if (oldHighScore != -1)
        {
            // set the text to true and set the old and new highscore values
            newHighScoreText.SetActive(true);
            oldScoreText.text = "Old Highscore: " + oldHighScore;
            newScoreText.text = "New Highscore: " + gd.highscore.ToString();
        }

        // set mouse to visible
        Cursor.visible = true;

        // in case player is minimized
        Player.instance.gameObject.SetActive(true);
        AudioManager.instance.StopThemeSong("Theme");

    }

    // return to main menu
    private void QuitGame()
    {
        SceneManager.LoadScene("Menu");
    }

}
