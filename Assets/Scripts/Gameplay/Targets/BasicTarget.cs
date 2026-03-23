using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BasicTarget : TargetClass
{

    //Values for the score, Multiplier, and how much time to deduct for this target
    [Header("Target Effects")]
    [SerializeField] private int ScoreValue = 2; //Changing to this private or static causes issues when trying to isolate the system
    [SerializeField] private int ScoreMultiValue;
    [SerializeField] private int TimeDeduction;


    //private static int CurrentMultiValue; //Variable to store the current Multiplier value

    //private bool MultiActive = false;

    //Create a method that will handle the score and then call it in the mouse input script
    
    // public void AddScore() //This methods gets called in the MouseInput Script
    // {
    //     ScoreManager.Instance.ScoreIncrease(ScoreValue);
    // }

    public override void OnHit()
    {
        ScoreManager.Instance.ScoreIncrease(ScoreValue);
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


