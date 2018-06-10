using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// green square behaviour script

public class PowerUp : MonoBehaviour {

    // rb2d for gravity
    [HideInInspector]public Rigidbody2D rb2d;

    // default score for addScore powerup
    private int score = 100;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        // if the player dies, destroy all green squares currently in the game
        if (GameController.instance.gameOver)
        {
            Destroy(this.gameObject);
        }
    }

    // if the powerup touches the ground, destory it
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // remove other object from game
            Destroy(this.gameObject);
        }
        // if the powerup touches the player, add the score and destroy the green square
        else if (other.gameObject.CompareTag("Player"))
        {

            // play powerup sound
            FindObjectOfType<AudioManager>().Play("GetPowerUp");

            // depending on the powerups name, call their corresponding methods
            if (this.gameObject.name == "AddScore(Clone)")
            {
                addScore();
            }
            else if (this.gameObject.name == "DoubleScore(Clone)")
            {
                doublePoints();
            }
            else if (this.gameObject.name == "Minimize(Clone)")
            {
                minimize();
            }

            // destory the powerup after activating its effects
            Destroy(this.gameObject);
        }
    }

    // make the player half size
    private void minimize()
    {
        GameController.instance.minimizeTime = Time.time + 15f;
    }

    // double the score multiplier
    private void doublePoints()
    {
        GameController.instance.multiplierTime = Time.time + 15f;
    }

    // add a flat score amount
    private void addScore()
    {
        // add the good squares score to the current score, and destroy the good square
        GameController.instance.PlayerScored(score);
    }

}
