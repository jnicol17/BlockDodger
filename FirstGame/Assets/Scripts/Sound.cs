using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound{

    // the name of the sound
    public string name;
    // the sound clip
    public AudioClip clip;
    // the volume
    public float volume;
    // the source of the sound (player death, enemy hits ground, etc)
    [HideInInspector]
    public AudioSource source;
}
