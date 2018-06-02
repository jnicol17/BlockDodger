using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// main menu scene

public class MainMenu : MonoBehaviour {
    
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
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE)
            Application.Quit();
        #elif (UNITY_WEBGL)
            Application.OpenURL("https://jnicol17.github.io/BlockDodger/");
        #endif
    }
}
