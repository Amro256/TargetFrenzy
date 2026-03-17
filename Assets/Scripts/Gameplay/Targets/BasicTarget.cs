using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BasicTarget : TargetClass
{

    //Variables for general gameplay effects
    [Header("Target Effects")]
    //[SerializeField] private int score; //Changing to this private or static causes issues when trying to isolate the system
    [SerializeField] private int scoreMultiplier;
    [SerializeField] private int timePenalty;


    private int TotalScore; //General Variable to store the total score
    private int currentScore; //To keep track of the current score


    //private static int CurrentMultiValue; //Variable to store the current Multiplier value

    //private bool MultiActive = false;

    //Create a method that will handle the score and then call it in the mouse input script#endregion

    public void AddScore() //This could be moved to the game manager down the line
    {
        ScoreManager.Instance.ScoreIncrease(2);
        // //Debug.Log("Before: " + totalScore); - Used for testing
        // TotalScore += score;
        // currentScore = TotalScore;

        // // //Update the UI text here - which gets called in the mouse input script
        // UIManager.Instance.ScoreText.text = currentScore.ToString();

        //Debug.Log("After: " + totalScore); - Used for testing

    }
    


    //Create a method for the target multiplier (testing for now - Works as of: 17/3/26)

    // public void MultiTest()
    // {
    //     CurrentMultiValue = scoreMultiplier;
    //     Debug.Log("Current Multi value: " + CurrentMultiValue); //Ok. This is working! Just need to apply the multiplier value to the score itself now and update the score text
    //     MultiActive = true;

    //     if (MultiActive)
    //     {
    //         Debug.Log("MultiActive!");

    //         //Apply the value to the score and update the text score
    //         currentScore = TotalScore * CurrentMultiValue;
    //         Debug.Log("Current Score: " + currentScore);

    //         //Update text score

    //         UIManager.Instance.ScoreText.text = currentScore.ToString();
    //     }
    //     else
    //     {
    //         Debug.Log("Multi currently not activated!");
    //      }

    //     //Update the text display
    //     UIManager.Instance.MultiText.text = CurrentMultiValue.ToString();
    // }
}


