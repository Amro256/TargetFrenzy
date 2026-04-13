using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    //This script will be used to be track and store the player's score. It will also make it easier to implement the multiplier functionality without the code becoming a mess.


    //General Variables for scoring and score multiplier
    private int TotalScore; //General Variable to store the total score.
    private int CurrentScore; //Variable to track and store the current score
    private static int CurrentMultiValue; //Will be used to track and store the current multiplier value
    private bool IsMultiActive; //This bool will be used to check whether the score multiplier is active or not!

    //Actions
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnMultiValueChanged;

    private void OnEnable()
    {
        BasicTarget.OnTargetHit += ScoreIncrease;
        MultiplierTarget.OnMultiplierActive += ScoreMultiplier;
    }

    private void OnDisable()
    {
        BasicTarget.OnTargetHit -= ScoreIncrease;
        MultiplierTarget.OnMultiplierActive -= ScoreMultiplier;
    }


    public void ScoreIncrease(int ScoreValue) //Method for handling adding score that takes in an integer as a parameter 
    {
        TotalScore += ScoreValue;
        CurrentScore = TotalScore;

        if (IsMultiActive == true)
        {
            CurrentScore = TotalScore * CurrentMultiValue;
            Debug.Log("Current Score: " + CurrentScore);
        }

        //Update UI text here
        OnScoreChanged?.Invoke(CurrentScore);
    }

    public void ScoreDeduction(int ScoreValue) //Method for handling deduction in score
    {
        TotalScore -= ScoreValue;
        CurrentScore = TotalScore;

        //Update UI text here
        OnScoreChanged?.Invoke(CurrentScore);
    }

    //Add method here to handle score Multiplier --Currently works! Call this method for targets will be use the multiplier value

    public void ScoreMultiplier(int MultiValue)
    {
        CurrentMultiValue = MultiValue;
        Debug.Log("Current Multi value: " + CurrentMultiValue); //Ok. This is working! Just need to apply the multiplier value to the score itself now and update the score text
        IsMultiActive = true; //Set the boolean to true

        if (IsMultiActive)
        {
            Debug.Log("MultiActive!");

            //Apply the value to the score and update the text score

            // CurrentScore = TotalScore * CurrentMultiValue;
            // Debug.Log("Current Score: " + CurrentScore);

            //Update text score -Invoke Action
            //UIManager.Instance.ScoreText.text = CurrentScore.ToString();
            //OnScoreChanged?.Invoke(CurrentScore);
            OnScoreChanged?.Invoke(CurrentScore);
        }
        else
        {
            Debug.Log("Multi currently not activated!");
            IsMultiActive = false;
        }

        //Update the text display - Invoke Action
        //UIManager.Instance.MultiText.text = CurrentMultiValue.ToString();
        OnMultiValueChanged?.Invoke(CurrentMultiValue);
    }

    // public void TimeIncrease()
    // {
    //     //Check if the timer is currently running first
    // }

    // public void TimeDeduction()
    // {
    //     //Check if the timer is currently running first
    // }
}
