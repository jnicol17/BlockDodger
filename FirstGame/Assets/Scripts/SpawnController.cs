using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script controls spawning of enemies and good guys

public class SpawnController : MonoBehaviour {

    // instance is used to make sure there is only one spawn controller
    public static SpawnController instance;

    // the spawner prefab is going to be instantiated at the beginning
    public GameObject spawnerPrefab;

    // min and max times for the initial spawn time of enemies
    public float timeMin = 2f;
    public float timeMax = 5f;

    // min and max times for the initial spawn delay of enemies
    public float delayMin = 2f;
    public float delayMax = 4f;

    // minimum spawn time for an enemy
    private float minSpawnTime = 0.5f;

    // nextIncrease is the time until we decrease the spawn time of the enemy with the highest spawn time
    private float nextIncrease = 0f;
    // we will decrease spawn time every increaseInterval seconds
    private float increaseInterval = 3f;

    // max is used to find the spawner with the highest spawn time
    private int max = 0;

    // array of numSpawners spawners
    public GameObject[] spawners;
    public int numSpawners = 15;

    // this function is used to make sure there is only one spawn controller
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
    void Start()
    {
        // initialize the list of spawners
        spawners = new GameObject[numSpawners];

        // x is the x position of the spawners
        int x = -14;
        // i is the index of the array
        for (int i = 0; i < numSpawners; i++)
        {
            // instantiate a spawner prefab
            spawners[i] = Instantiate(spawnerPrefab, new Vector2(x, 10), Quaternion.identity);
            // set the spawn time and spawn delay to be in the range of the min and max values
            spawners[i].GetComponent<Spawner>().spawnTime = Random.Range(timeMin, timeMax);
            spawners[i].GetComponent<Spawner>().spawnDelay = Random.Range(delayMin, delayMax);
            // next spawner is 2 units to the right
            x += 2;
            // begin spawning the enemies (using invokeRepeating)
            invokeSpawner(i);
        }
	}
	
	// Update is called once per frame
	void Update () {

        // if the player has not died yet
        if (!GameController.instance.gameOver)
        {

            // if it's time to decrease the max spawn time
            if (Time.timeSinceLevelLoad >= nextIncrease)
            {
                // max will be the index of the spawner with the highest spawn time
                max = 0;
                for (int i = 0; i < numSpawners; i++)
                {
                    // spawn time must be bigger than the spawn time of the current max, and can't already have min spawn time
                    //if (spawners[i].GetComponent<Spawner>().spawnTime > spawners[max].GetComponent<Spawner>().spawnTime && spawners[i].GetComponent<Spawner>().timeMin > minSpawnTime)
                    if (spawners[i].GetComponent<Spawner>().spawnTime > spawners[max].GetComponent<Spawner>().spawnTime)
                    {
                        max = i;
                    }
                }

                // next increase will be in increaseInterval seconds
                nextIncrease += increaseInterval;

                // if the spawner hasn't reached the min spawn time yet, the new spawn time will be in the range (min, current)
                if (spawners[max].GetComponent<Spawner>().timeMin > minSpawnTime)
                {
                    spawners[max].GetComponent<Spawner>().spawnTime = Random.Range(spawners[max].GetComponent<Spawner>().timeMin, spawners[max].GetComponent<Spawner>().spawnTime);
                    spawners[max].GetComponent<Spawner>().timeMin -= 0.5f;

                    // stop spawning enemies at the current spawn time, start spawning at the new spawn time
                    spawners[max].GetComponent<Spawner>().CancelInvoke("Spawn");
                    spawners[max].GetComponent<Spawner>().spawnDelay = Random.Range(0f, 1f);
                    invokeSpawner(max);
                }
                // if the spawner has reach min spawn time, set the spawn time to the min spawn time and start invoking at the min spawn time
                else
                {
                    spawners[max].GetComponent<Spawner>().spawnTime = minSpawnTime;
                    spawners[max].GetComponent<Spawner>().CancelInvoke("Spawn");
                    spawners[max].GetComponent<Spawner>().spawnDelay = Random.Range(0f, 0.5f);
                    invokeSpawner(max);
                }

            }

            // on every frame there is a 1/1000 chance that a spawner will spawn a powerup
            int spawnPowerUp = Random.Range(500, 600);
            //int spawnPowerUp = Random.Range(0, 1000);
            if (spawnPowerUp == 555)
            {
                int j = Random.Range(0, 15);
                spawners[j].GetComponent<Spawner>().Invoke("SpawnPowerUp", 1f);
            }
        }
	}

    // start spawning from spawner j
    void invokeSpawner(int j)
    {
        spawners[j].GetComponent<Spawner>().InvokeRepeating("Spawn", spawners[j].GetComponent<Spawner>().spawnDelay, spawners[j].GetComponent<Spawner>().spawnTime);
    }
}
