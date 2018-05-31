using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //private BoxCollider2D boxCollider;
    //private Rigidbody2D rb2d;
    public int speed = 10;

    // static speed modifier that is modified by test buttons in the main menu when doing user testing on player speed
    public static float speedM = 1f;

    public int goodGuyScore;

    // Use this for initialization
    void Start ()
    {
        //boxCollider = GetComponent<BoxCollider2D>();
        //rb2d = GetComponent<Rigidbody2D>();
	}

    //void FixedUpdate()
    //{
    //    if (!GameController.instance.gameOver)
    //    {
    //        //int horizontal = (int)Input.GetAxisRaw("Horizontal");
    //        //Vector3 movement = new Vector2(horizontal, 0);

    //        //// more smooth
    //        //float move = speed * Time.deltaTime * speedM;
    //        //if ((transform.position.x  <= 14.1f && horizontal >= 0) || (transform.position.x >= -14.1f && horizontal <= 0))
    //        //{
    //        //    transform.position = Vector3.MoveTowards(transform.position, transform.position + movement, move);
    //        //}
    //        float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
    //        if (this.transform.position.x <= 14.1f && x >= 0 || this.transform.position.x >= -14.1f && x <= 0) {
    //            this.transform.position = new Vector2(x, this.transform.position.y);
    //        }
    //    }
    //}

    void Update()
    {
        if (!GameController.instance.gameOver)
        {
            //int horizontal = (int)Input.GetAxisRaw("Horizontal");
            //Vector3 movement = new Vector2(horizontal, 0);

            //// more smooth
            //float move = speed * Time.deltaTime * speedM;
            //if ((transform.position.x  <= 14.1f && horizontal >= 0) || (transform.position.x >= -14.1f && horizontal <= 0))
            //{
            //    transform.position = Vector3.MoveTowards(transform.position, transform.position + movement, move);
            //}
            float x = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -14.1f, 14.1f);
            //Debug.Log(x);
            //if (x <= 14f && x >= -14f)
            //{
            this.transform.position = new Vector2(x, -5.46f);
            //}
            //else
            //{
                //this.transform.position = new Vector2(this.transform.position.x, -5.46f);
            //}
        }
        else
        {
            this.transform.position = new Vector2(0, -5.46f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // remove other object from game
            Destroy(other.gameObject);
            // freeze player
            GameController.instance.PlayerDied();
        }
        else if (other.gameObject.CompareTag("GoodGuy"))
        {
            GameController.instance.PlayerScored(goodGuyScore);
            Destroy(other.gameObject);
        }
    }
}
