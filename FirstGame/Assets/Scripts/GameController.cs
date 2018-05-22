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

    public void PlayerScored(int scoreMultiplier)
    {
        if (gameOver)
        {
            return;
        }
        score += scoreMultiplier;
        scoreText.text = "Score: " + score.ToString();
    }

    public void PlayerDied()
    {
        gameOver = true;
        gameOverText.SetActive(true);
        Cursor.visible = true;
    }

    private void QuitGame()
    {
        // save any game data here
        //#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        //    UnityEditor.EditorApplication.isPlaying = false;
        //#else
        // Application.Quit();
        //#endif

        SceneManager.LoadScene("Menu");
    }

}
