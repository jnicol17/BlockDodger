﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script controls the spawner prefab

public class Spawner : MonoBehaviour {

    public float spawnTime;        // The amount of time between each spawn.
    public float spawnDelay;       // The amount of time before spawning starts.

    // min spawn time for the spawner, will decrease in spawnController to a min value
    public float timeMin = 2f;

    // spawners can spawn powerups and enemies
    public GameObject enemies;
    public GameObject[] powerUps;

    public int numPowerUps;

	// Update is called once per frame
	void Update () {
        // if the player dies, destroy all spawners
        if (GameController.instance.gameOver)
        {
            Destroy(this.gameObject);
        }
    }

    // spawn an enemy if the player has not died, invoked repeatedly in spawn controller
    public void Spawn()
    {
        if (!GameController.instance.gameOver)
        {
            Instantiate(enemies, transform.position, transform.rotation);
        }
    }

    // spawn a powerup if the player has not died, invoked randomly in spawn controller
    public void SpawnPowerUp()
    {
        int index = Random.Range(0, numPowerUps);
        if (!GameController.instance.gameOver)
        {
            Instantiate(powerUps[index], transform.position, transform.rotation);
        }
    }
}
