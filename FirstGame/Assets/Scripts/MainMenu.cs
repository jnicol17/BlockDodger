using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE)
            Application.Quit();
        #elif (UNITY_WEBGL)
            Application.OpenURL("https://jnicol17.github.io/BlockDodger/");
        #endif
    }


    // Test Buttons for static variables to modify player speed from main menu when doing user testing

    //public void Test2()
    //{
    //    Player.speedM = 2f;
    //}

    //public void Test125()
    //{
    //    Player.speedM = 1.25f;
    //}

    //public void Test15()
    //{
    //    Player.speedM = 1.5f;
    //}

    //public void Test175()
    //{
    //    Player.speedM = 1.75f;
    //}
}
