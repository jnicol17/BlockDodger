using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDetailContainer : MonoBehaviour {

    public static GameDetails LoadedGameDetails = null;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
}
