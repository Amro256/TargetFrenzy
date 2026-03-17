using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    void Awake()
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

    //This script will be used to be track and store the player's score. It will also make it easier to implement the multiplier functionality without the code becoming a mess.
    private int TotalScore; //General Variable to store the total score.
    private int CurrentScore; //Variable to track and store the current score

    //private static int multiValue;


    public void ScoreIncrease(int score) //Method for handling adding score
    {
        TotalScore += score;
        CurrentScore = TotalScore;

        //Update the UI text here
        UIManager.Instance.ScoreText.text = CurrentScore.ToString();
    }

    public void ScoreDeduction() //Method for handling deduction in score
    {

    }


    //Add a future method to handle score Multiplier
    public void ScoreMultiplier()
    {
        
    }
}
