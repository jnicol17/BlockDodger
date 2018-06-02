using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// green square behaviour script

public class GoodGuy : MonoBehaviour {

    [HideInInspector]public Rigidbody2D rb2d;
    public int score = 100;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        // if the player dies, destroy all green squares currently in the game
        if(GameController.instance.gameOver)
        {
            Destroy(this.gameObject);
        }
    }

    // if the green square touches the ground, destory it
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // remove other object from game
            Destroy(this.gameObject);
        }
        // if the green square touches the player, add the score and destroy the green square
        else if (other.gameObject.CompareTag("Player"))
        {
            // add the good squares score to the current score, and destroy the good square
            GameController.instance.PlayerScored(score);
            Destroy(this.gameObject);
        }
    }
}
