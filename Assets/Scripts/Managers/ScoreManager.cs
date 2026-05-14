using System.Collections;
using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //This script will be used to be track and store the player's score. It will also make it easier to implement the multiplier functionality without the code becoming a mess.

    //General Variables
    private int TotalScore; //General Variable to store the score.
    private int CurrentMultiValue; //Will be used to track and store the current multiplier value
    private bool IsMultiActive; //This bool will be used to check whether the score multiplier is active or not!
    private bool isRoutineRunning;
    private Coroutine currentCoroutine;

    //Actions
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnMultiValueChanged;

    private void OnEnable()
    {
        BasicTarget.OnTargetHit += ScoreIncrease;
        MultiplierTarget.OnMultiplierActive += ScoreMultiplier;
        Score_TimeDeductionTarget.OnScoreDeduction += ScoreDeduction;
    }

    private void OnDisable()
    {
        BasicTarget.OnTargetHit -= ScoreIncrease;
        MultiplierTarget.OnMultiplierActive -= ScoreMultiplier;
        Score_TimeDeductionTarget.OnScoreDeduction -= ScoreDeduction;
    }


    public void ScoreIncrease(int ScoreValue) //Method for handling adding score that takes in an integer as a parameter 
    {
        int HitScore = ScoreValue;

        //Check to see if the multiplier is active, then apply it to the score

        if (IsMultiActive) //If the multiplier is set the true
        {
            HitScore *= CurrentMultiValue;
        }

        TotalScore += HitScore;

        //Update the score UI here
        OnScoreChanged?.Invoke(TotalScore);
    }

    public void ScoreDeduction(int ScoreValue) //Method for handling deduction in score
    {
        TotalScore -= ScoreValue;

        //Update the score UI here
        OnScoreChanged?.Invoke(TotalScore);
    }

    //Add method here to handle the score Multiplier --Currently works! Call this method for targets will be use the multiplier value --13/4/26: Code was refactored--
    public void ScoreMultiplier(int MultiValue)
    {
        if (!IsMultiActive)
        {
            IsMultiActive = true; //Set Multi bool to true
            CurrentMultiValue = MultiValue;

            Debug.Log("Multi Active!");
            Debug.Log("Current Multi value: " + CurrentMultiValue); //Ok. This is working! Just need to apply the multiplier value to the score itself now and update the score text

            //Code to handling the UI bar goes here -- Activate Bar
            currentCoroutine = StartCoroutine(MultiplierBarManager.Instance.BarRoutine());

        }
        else
        {
            Debug.Log("Multi Already Active");
            IsMultiActive = false; //This check has to be here, otherwise the Coroutine wont restart after the duration ends
        }

        

        // else
        // {
        //     IsMultiActive = false;
        //     Debug.Log("Multi currently not activated!");
        // }

        //Update the text display - Invoke Action
        //OnMultiValueChanged?.Invoke(CurrentMultiValue);

    }

}
