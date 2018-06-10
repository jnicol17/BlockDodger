using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// main menu scene

public class MainMenu : MonoBehaviour {

    // persistant data will be stored in gd
    GameDetails gd;

    // create the sound button text (sound: on or sound: off)
    public TextMeshProUGUI muteButtonText;

    public void Start()
    {
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

    }

    // called when the player clicks the "Play" button
    public void PlayGame()
    {
        clickButtonSound();
        // load the game scene
        SceneManager.LoadScene("Main");
    }

    // called when the player clicks the "Quit" button
    public void QuitGame()
    {

        // depending on the platform, exit the game
        // in the case of WebGL, can't close the tab, so instead redirect player to info about game
        DataAccess.Save(gd);
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE)
            Application.Quit();
        #elif (UNITY_WEBGL)
            Application.OpenURL("https://jnicol17.github.io/BlockDodger/");
        #endif
    }

    // mute button
    public void MuteButton()
    {
        clickButtonSound();
        // mute if not muted, unmute if muted
        if (gd.volumeOn)
        {
            gd.volumeOn = false;
        }
        else
        {
            gd.volumeOn = true;
        }
        DataAccess.Save(gd);
        // reset mute button text
        setMuteButton();
        AudioManager.instance.muteVolume(gd); //
    }

    // change volume with volume slider
    public void SetVolume(float volume)
    {
        gd.volumeNum = volume;
        DataAccess.Save(gd);
        AudioManager.instance.setVolume(gd);
    }

    // when the user clicks on the options menu button
    public void setOptionsMenu()
    {

        // set the volume slider
        setVolumeSliderValue();
        // set the mute buttons state
        setMuteButton();

    }

    // set the mute buttons value when options menu is loaded
    public void setMuteButton()
    {

        if (!gd.volumeOn)
        {
            muteButtonText.text = "Sound: Off";
        }
        else
        {
            muteButtonText.text = "Sound: On";
        }
    }

    // set value of volume slider when options menu is loaded
    public void setVolumeSliderValue()
    {
        FindObjectOfType<Slider>().value = gd.volumeNum;
    }

    public void clickButtonSound()
    {
        // play button click sound
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

}
