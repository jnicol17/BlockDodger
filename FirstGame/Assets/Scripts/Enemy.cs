using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enemy behaviour script

public class Enemy : MonoBehaviour {

    [HideInInspector] public Rigidbody2D rb2d;

    // the amount that the enemy fall speed increases
    private float increaseGravity = 0.5f;
    // the interval for increasing enemy fall speed
    private float increaseGravityInterval = 10f;
    // maximum gravity for an enemy
    private float maxGravity = 6f;

    // enemy score
    public int enemyScore;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {

        // if the player dies, destory all enemies
        if (GameController.instance.gameOver)
        {
            Destroy(this.gameObject);
        }

        // keep track of the time and increase if it passes the increaseGravityInterval
        else
        {
            // if increaseGravityInterval seconds have passed and we havent reached max gravity yet, increase gravity by increaseGravity amount
            if (Time.timeSinceLevelLoad >= increaseGravityInterval && rb2d.gravityScale < maxGravity)
            {
                // increasing the interval
                increaseGravityInterval += increaseGravityInterval;
                // increasing the enemy gravity
                rb2d.gravityScale += increaseGravity;

            }
        }
		
	}

    // if the enemy collides with the player or the ground
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the enemy hits the ground, increase player score and destroy the enemy
        if (other.gameObject.CompareTag("Ground"))
        {
            // play sound
            FindObjectOfType<AudioManager>().Play("EnemyDie");

            // remove other object from game
            GameController.instance.PlayerScored(enemyScore);
            Destroy(this.gameObject);
        }

        // if the enemy hits the player, player dies
        else if (other.gameObject.CompareTag("Player"))
        {
            // remove enemy from game
            Destroy(this.gameObject);

            // play sound
            FindObjectOfType<AudioManager>().Play("PlayerDie");

            // freeze player at origin
            GameController.instance.PlayerDied();
        }
    }

}
