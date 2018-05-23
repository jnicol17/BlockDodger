using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float spawnTime = 5f;        // The amount of time between each spawn.
    public float spawnDelay = 3f;       // The amount of time before spawning starts.
    public float timeMin = 3f;
    public float timeMax = 7f;
    [HideInInspector] public bool beingInvoked = false;
    [HideInInspector] public bool recentlyModified = false;
    public GameObject enemies;
    public GameObject goodGuy;

    // Use this for initialization
    void Start () {
        // Start calling the Spawn function repeatedly after a delay .
        //InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }
	
	// Update is called once per frame
	void Update () {
        //if (GameController.instance.gameOver)
        //{
        //    this.gameObject.SetActive(false);
        //}
        if (GameController.instance.gameOver)
        {
            Destroy(this.gameObject);
        }
    }

    public void Spawn()
    {
        // Instantiate a random enemy.
        //int enemyIndex = Random.Range(0, enemies.Length);
        if (!GameController.instance.gameOver)
        {
            Instantiate(enemies, transform.position, transform.rotation);
        }
    }

    public void SpawnGoodGuy()
    {
        if (!GameController.instance.gameOver)
        {
            Instantiate(goodGuy, transform.position, transform.rotation);
        }
    }
}
