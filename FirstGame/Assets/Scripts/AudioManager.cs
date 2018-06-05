using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    // instance is used to ensure there is only ever one game controller
    public static AudioManager instance;

    // persistant data will be stored in gd
    GameDetails gd;

    // array of possible sounds
    public Sound[] sounds;

    // Use this for initialization
    void Awake () {

        // ensure there is only one audio manager
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);


        // load the saved browser data
        GameDetailContainer.LoadedGameDetails = DataAccess.Load();

        // if there is browser data, store it in gd
        // otherwise, create new browser data and store it in gd
        if (GameDetailContainer.LoadedGameDetails != null)
        {
            gd = GameDetailContainer.LoadedGameDetails;
        }
        else
        {
            gd = new GameDetails();
        }

        createSounds(gd);
        setVolume(gd);

	}

    void Start()
    {
        Play("Theme");
    }

    // create the sound objects and link the clips
    public void createSounds(GameDetails gd)
    {

        // loop through the sound list
        foreach (Sound s in sounds)
        {
            // create sound object and add clip
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }
    }

    // set the volumes of the sound objects, needs to be a separate method so it can be changed in the main menu script on the fly
    public void setVolume(GameDetails gd)
    {
        // loop through the sound list
        foreach (Sound s in sounds)
        {
            //Debug.Log("Audio Manager");
            //Debug.Log(gd.volumeOn);
            //Debug.Log(gd.volumeNum);
            if (gd.volumeOn)
            {
                s.source.volume = gd.volumeNum;
            }
            else
            {
                s.source.volume = 0f;
            }
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }
}
