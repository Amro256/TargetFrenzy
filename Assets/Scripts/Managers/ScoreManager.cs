using System.Collections;
using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //This script will be used to be track and store the player's score. It will also make it easier to implement the multiplier functionality without the code becoming a mess.

    //Singleton

    public static ScoreManager Instance;

    //General Variables
    private int TotalScore; //General Variable to store the score.
    private int CurrentMultiValue; //Will be used to track and store the current multiplier value
    private bool IsMultiActive; //This bool will be used to check whether the score multiplier is active or not! (By default it'll be set to false)
    private bool isRoutineRunning;
    private Coroutine currentCoroutine;

    private int bonusRoundThreshold = 300; //If the player's score hits this threshold, it'll trigger the bonus round

    //Actions
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnMultiValueChanged;

    public static event Action OnBonusRoundActivated;
    
    

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

        if (IsMultiActive != false) //If the multiplier is set the true --14/5/26: Changed to is equal to true due to a bug related to the code change of the scoreMultiplier method
        {
            HitScore *= CurrentMultiValue;
        }

        TotalScore += HitScore;


        if (TotalScore >= bonusRoundThreshold)
        {
            Debug.Log("You've met the threshold!");

            OnBonusRoundActivated?.Invoke();

            //Code here - What do we want to do here? - 28/5/2026
            // 0.5) Set is BonusRoundActive bool to true - DONE (Game Manager Property)
            // 1) Disable Spawners temporarily - DONE  (Game Manager)
            // 2) Set the timer to 30 seconds (or the time amount decided for the bonus round) - DONE

            // 3) Set up an Coroutine to Flash the words "Bonus round" on screen, followed by 3,2,1, GO (UI Manager)
            // 4) Re-enable everything (Game + UI Managers)


            //Call the method from the bonus round manager here!

            //29/5/26 - Moved the code below into its own Bonus Round Manager script

            // GameManager.Instance.BonusRoundBool = true;
            // Debug.Log("Bonus Round has been activated!");
            // //Action here
            // OnBonusRoundActive?.Invoke();

            // GameManager.Instance.spawnerObjects.SetActive(false);
            // Debug.Log("Spawners disabled for now");

        }


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
        if (!IsMultiActive) //the "!" is a logical not operator. This operator reverses the state of the boolean from false to true
        {
            IsMultiActive = true; //Set Multi bool to true
            CurrentMultiValue = MultiValue;

            Debug.Log("Multi Active!");
            Debug.Log("Current Multi value: " + CurrentMultiValue); //Ok. This is working! Just need to apply the multiplier value to the score itself now and update the score text

            //Code to handling the UI bar goes here -- Activate Bar
            currentCoroutine = StartCoroutine(MultiplierBarManager.Instance.BarRoutine());

            //Call a new Coroutine that will reset the multi bool once the multiplier duration is up
            StartCoroutine(MultiplierDuration());
        }
        else
        {
            Debug.Log("Multi Already Active");
        }
    }

    private IEnumerator MultiplierDuration() //A 2nd Coroutine 
    {
        yield return new WaitForSeconds(MultiplierBarManager.Instance.maxMultiplierDuration); //This will wait for the multiplier duration to be done BEFORE setting the Multi bool back to false
        //And so it uses the max multiplier duration from the multiplier script as 'how long it should wait' before the bool is set to false

        IsMultiActive = false; //Sets the multi bool back to false
     }

}
