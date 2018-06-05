using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// main menu scene

public class MainMenu : MonoBehaviour {

    // persistant data will be stored in gd
    GameDetails gd;

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

    // test button
    public void TestData()
    {
        gd.volumeNum = 0.1f;
        if (gd.volumeOn)
        {
            gd.volumeOn = false;
        }
        else
        {
            gd.volumeOn = true;
        }
        DataAccess.Save(gd);
        AudioManager.instance.setVolume(gd); //
    }

    public void MuteButton()
    {
        if (gd.volumeOn)
        {
            gd.volumeOn = false;
        }
        else
        {
            gd.volumeOn = true;
        }
        DataAccess.Save(gd);
        AudioManager.instance.muteVolume(gd); //
    }

    public void SetVolume(float volume)
    {
        gd.volumeNum = volume;
        DataAccess.Save(gd);
        AudioManager.instance.setVolume(gd);
    }

    public void setVolumeSliderValue()
    {
        FindObjectOfType<Slider>().value = gd.volumeNum;
    }

}
