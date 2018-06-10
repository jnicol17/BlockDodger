using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player behaviour script

public class SmallPlayer : MonoBehaviour
{

    // player height never changes
    public float playerY;

    // x value can not be larger that |clampX|
    private float clampX = 14.1f;

    public static SmallPlayer instance;

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

    public float getXPosition()
    {
        // mouse movement is restricted between the two side walls
        float x = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -clampX, clampX);
        return x;
    }
}
