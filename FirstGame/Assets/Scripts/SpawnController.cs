using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public static SpawnController instance;

    public GameObject spawnerPrefab;
    public float startMin = 3f;
    public float startMax = 7f;
    public float spawnMin = 2f;
    public float spawnMax = 5f;

    private float nextIncrease = 0f;
    private float increaseInterval = 3f;

    private float invokeResetIncrease = 15f;
    private float increaseInvokeReset = 15f;

    private float modifiedCount = 0;

    //public Spawner[] spawners;
    public GameObject[] spawners;
    public int numSpawners = 15;

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
    //private void OnLevelWasLoaded()
    {
    //spawners = new Spawner[numSpawners];
    spawners = new GameObject[numSpawners];
        int j = -14;
        for (int i = 0; i < numSpawners; i++)
        {
            spawners[i] = Instantiate(spawnerPrefab, new Vector2(j, 10), Quaternion.identity);
            spawners[i].GetComponent<Spawner>().spawnTime = Random.Range(startMin, startMax);
            spawners[i].GetComponent<Spawner>().spawnDelay = Random.Range(spawnMin, spawnMax);
            j += 2;
        }
	}
	
	// Update is called once per frame
	void Update () {
        // if the game is not over yet
        if (!GameController.instance.gameOver)
        {
            int j = Random.Range(0, numSpawners);
            if (!spawners[j].GetComponent<Spawner>().beingInvoked)
            {
                spawners[j].GetComponent<Spawner>().beingInvoked = true;
                spawners[j].GetComponent<Spawner>().InvokeRepeating("Spawn", spawners[j].GetComponent<Spawner>().spawnDelay, spawners[j].GetComponent<Spawner>().spawnTime);
            }
            else
            {
                if (Time.timeSinceLevelLoad >= nextIncrease)
                {
                    Debug.Log(Time.timeSinceLevelLoad);
                    Debug.Log(j);
                    if (spawners[j].GetComponent<Spawner>().timeMax > spawners[j].GetComponent<Spawner>().timeMin && spawners[j].GetComponent<Spawner>().recentlyModified == false)
                    {
                        modifiedCount += 1;
                        spawners[j].GetComponent<Spawner>().recentlyModified = true;
                        nextIncrease += increaseInterval;
                        spawners[j].GetComponent<Spawner>().timeMax -= 0.25f;
                        spawners[j].GetComponent<Spawner>().spawnTime = Random.Range(spawners[j].GetComponent<Spawner>().timeMin, spawners[j].GetComponent<Spawner>().timeMax);
                        //spawners[j].GetComponent<Spawner>().spawnDelay = Random.Range(spawnMin, spawnMax);
                    }
                    else
                    {
                        spawners[j].GetComponent<Spawner>().spawnTime = spawners[j].GetComponent<Spawner>().timeMin;
                        //spawners[j].GetComponent<Spawner>().spawnDelay = Random.Range(spawnMin, spawnMax);
                    }
                }
            }

            if (modifiedCount >= numSpawners)
            {
                for (int a = 0; a < numSpawners; a++)
                {
                    spawners[a].GetComponent<Spawner>().recentlyModified = false;
                }
            }

            if (Time.timeSinceLevelLoad >= invokeResetIncrease)
            {
                invokeResetIncrease += increaseInvokeReset;
                for (int k = 0; k < numSpawners; k++)
                {
                    spawners[k].GetComponent<Spawner>().CancelInvoke("Spawn");
                    spawners[k].GetComponent<Spawner>().beingInvoked = false;
                    spawners[k].GetComponent<Spawner>().spawnDelay = Random.Range(0f, 2f);
                }
            }

            int spawnGoodGuy = Random.Range(0, 1000);
            if (spawnGoodGuy == 555)
            {
                spawners[j].GetComponent<Spawner>().Invoke("SpawnGoodGuy", 1f);
            }
        }
	}
}
