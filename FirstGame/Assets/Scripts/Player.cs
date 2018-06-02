using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player behaviour script

public class Player : MonoBehaviour {

    // score of the green squares
    //private int goodGuyScore = 100;

    // player height never changes
    private float playerY = -5.46f;

    // x value can not be larger that |clampX|
    private float clampX = 14.1f;

    // called once per frame
    void Update()
    {
        // if the player has not died
        if (!GameController.instance.gameOver)
        {
            // mouse movement is restricted between the two side walls
            float x = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -clampX, clampX);

            // move the player to the new x position
            this.transform.position = new Vector2(x, playerY);
        }

        // if the player has died, reset them to the origin (centre of screen)
        else
        {
            this.transform.position = new Vector2(0, playerY);
        }
    }

    //// when the player collides with enemy square or good square
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    // enemy square, player will die
    //    if (other.gameObject.CompareTag("Enemy"))
    //    {
    //        // remove enemy from game
    //        Destroy(other.gameObject);
    //        // freeze player at origin
    //        GameController.instance.PlayerDied();
    //    }
    //    // good square, currently just a score boost, will become upgrades
    //    else if (other.gameObject.CompareTag("GoodGuy"))
    //    {
    //        // add the good squares score to the current score, and destroy the good square
    //        GameController.instance.PlayerScored(goodGuyScore);
    //        Destroy(other.gameObject);
    //    }
    //}
}
