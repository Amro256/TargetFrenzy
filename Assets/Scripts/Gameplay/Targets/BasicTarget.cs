using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BasicTarget : TargetClass
{
    private static int totalScore; //General Variable to store the total score.
    private static int multiValue;



    //Create a method that will handle the score and then call it in the mouse input script
    public void AddScore() //This could be moved to the game manager down the line
    {
        //Debug.Log("Before: " + totalScore); - Used for testing
        totalScore += score;
        //Update the UI text here - which gets called in the mouse input script
        UIManager.Instance.ScoreText.text = totalScore.ToString();

        //Debug.Log("After: " + totalScore); - Used for testing
    }

    //Create a method for the target multiplier (testing for now)
    public void MultiTest()
    {
        multiValue = scoreMultiplier;

        //Update the text display
        UIManager.Instance.MultiText.text = multiValue.ToString();
    }
}


