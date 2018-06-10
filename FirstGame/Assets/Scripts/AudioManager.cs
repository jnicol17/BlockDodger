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
        muteVolume(gd);
        setVolume(gd);

	}

    // play the theme song on scene load, already created in Awake
    void Start()
    {
        PlayThemeSong("Theme");
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
            // if its the theme song, loop it
            if (s.source.clip.name == "themesong")
            {
                s.source.loop = true;
            }
        }
    }

    // set the volumes of the sound objects, needs to be a separate method so it can be changed in the main menu script on the fly
    public void setVolume(GameDetails gd)
    {
        // loop through the sound list
        foreach (Sound s in sounds)
        {
            s.source.volume = gd.volumeNum; //
        }
    }

    // responds to the mute button, takes the current GameDetails state, which is modified when mute is pressed
    public void muteVolume(GameDetails gd)
    {
        foreach (Sound s in sounds)
        {
            s.source.mute = !gd.volumeOn; //
        }
    }

    // play the given song name if its found, otherwise don't play anything and raise no error
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.PlayOneShot(s.source.clip);
    }

    // themesong will be looped, can't use PlayOneShot
    public void PlayThemeSong(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }
}
