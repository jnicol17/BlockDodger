using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [HideInInspector] public Rigidbody2D rb2d;
    public float increaseGravity = 0.5f;
    public float increaseGravityInterval = 10f;
    public float maxGravity = 6f;

    public int enemyScore;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameController.instance.gameOver)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // if increaseGravityInterval seconds have passed and we havent reached max gravity yet, increase gravity by increaseGravity amount
            if (Time.timeSinceLevelLoad >= increaseGravityInterval && rb2d.gravityScale < maxGravity)
            {
                increaseGravityInterval += increaseGravityInterval;
                rb2d.gravityScale += increaseGravity;

            }
        }
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // remove other object from game
            GameController.instance.PlayerScored(enemyScore);
            Destroy(this.gameObject);
        }
    }

}
