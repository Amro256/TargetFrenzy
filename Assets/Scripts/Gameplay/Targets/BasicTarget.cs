using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BasicTarget : TargetClass
{
    private static int totalScore; //General Variable to store the total score.

    //Create a method that will handle the score and then call it in the mouse input script
    public void AddScore() //This could be moved to the game manager down the line
    {
        Debug.Log("Before: " + totalScore);
        totalScore += score;
        Debug.Log("After: " + totalScore);
    } 
}


