using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    //This script will be used to be track and store the player's score. It will also make it easier to implement the multiplier functionality without the code becoming a mess.
    public static ScoreManager Instance;

    //General Variables to store the store
    private int TotalScore; //General Variable to store the total score.
    private int CurrentScore; //Variable to track and store the current score


    //private static int CurrentMultiValue; //Will be used to track and store the current multiplier value
    //private bool IsMultiActive; //This bool will be used to check whether the score multiplier is active or not! --Will be implemented soon--

    void Awake() //Singleton Pattern  
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ScoreIncrease(int ScoreValue) //Method for handling adding score that takes in an integer as a parameter 
    {
        TotalScore += ScoreValue;
        CurrentScore = TotalScore;

        //Update the UI text here
        UIManager.Instance.ScoreText.text = CurrentScore.ToString();
    }

    public void ScoreDeduction() //Method for handling deduction in score
    {
        //Code here
    }


    //Add a future method to handle score Multiplier
    public void ScoreMultiplier()
    {
        //Code here!
    }
}
