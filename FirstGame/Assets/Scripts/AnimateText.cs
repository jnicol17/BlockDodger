﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateText : MonoBehaviour {

    public Text textBox;
    //Store all your text in this string array
    string title = "_B_L_O_C_K_D_O_D_G_E_R_";
    bool isUnderScore = true;
    private int test = 23;
    int currentlyDisplayingText = 0;

    void Awake()
    {
        StartCoroutine(AnimateTitle());
    }
    //This is a function for a button you press to skip to the next text
    public void SkipToNextText()
    {
        StopAllCoroutines();
        currentlyDisplayingText++;
        //If we've reached the end of the array, do anything you want. I just restart the example text
        if (currentlyDisplayingText > title.Length)
        {
            currentlyDisplayingText = 0;
        }
        StartCoroutine(AnimateTitle());
    }
    //Note that the speed you want the typewriter effect to be going at is the yield waitforseconds (in my case it's 1 letter for every      0.03 seconds, replace this with a public float if you want to experiment with speed in from the editor)
    IEnumerator AnimateTitle()
    {
        foreach (char letter in title.ToCharArray())
        {
            if (isUnderScore)
            {
                textBox.text += letter;
            }
            else
            {
                textBox.text = "_" ;
            }
            yield return new WaitForSeconds(.2f);
        }
    }

    //private float m_TimeStamp;
    //private bool cursor = false;
    //private string cursorChar = "";

    //void update()
    //{
    //    if (Time.time - m_TimeStamp >= 0.2)
    //    {
    //        m_TimeStamp = Time.time;
    //        if (cursor == false)
    //        {
    //            cursor = true;
    //            cursorChar += "_";
    //        }
    //        else
    //        {
    //            cursor = false;
    //            cursorChar = cursorChar.Substring(0, cursorChar.Length - 1);
    //        }
    //    }
    //}
}