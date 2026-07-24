using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    //This script will be used to be track and store the player's score. It will also make it easier to implement the multiplier functionality without the code becoming a mess.

    //Singleton

    public static ScoreManager Instance;

    //General Variables
    private int totalScore; //General Variable to store the score.
    private int hitScore;

    public int HitScore
    {
        get{ return hitScore; }
    }
    
    public int TotalScore
    {
        get { return totalScore; }
    }

    private int CurrentMultiValue; //Will be used to track and store the current multiplier value
    private bool isMultiActive;//This bool will be used to check whether the score multiplier is active or not! (By default it'll be set to false)

    public bool IsMultiActive
    {
        get { return isMultiActive; }
        set { isMultiActive = value; }
    }

    private bool HasBonusBeenTriggered;
    private int bonusRoundThreshold = 1555; //If the player's score hits this threshold, it'll trigger the bonus round

    //Actions
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnMultiValueChanged;

    public static event Action OnBonusRoundActivated;


    void Awake() //Singleton pattern
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    private void OnEnable()
    {
        BasicTarget.OnTargetHit += ScoreIncrease;
        MultiplierTarget.OnTargetHit += ScoreIncrease;
        GoldenTarget.OnTargetHit += ScoreIncrease;
        MultiplierTarget.OnMultiplierActive += ScoreMultiplier;
        Score_TimeDeductionTarget.OnScoreDeduction += ScoreDeduction;
    }

    private void OnDisable()
    {
        BasicTarget.OnTargetHit -= ScoreIncrease;
        MultiplierTarget.OnTargetHit -= ScoreIncrease;
        GoldenTarget.OnTargetHit -= ScoreIncrease;
        MultiplierTarget.OnMultiplierActive -= ScoreMultiplier;
        Score_TimeDeductionTarget.OnScoreDeduction -= ScoreDeduction;
    }


    public void ScoreIncrease(int ScoreValue) //Method for handling adding score that takes in an integer as a parameter 
    {
        hitScore = ScoreValue;
        

        //Check to see if the multiplier is active, then apply it to the score

        if (isMultiActive != false) //If the multiplier is set the true --14/5/26: Changed to is equal to true due to a bug related to the code change of the scoreMultiplier method
        {
            hitScore *= CurrentMultiValue;
        }
        
        totalScore += HitScore;


        if (TotalScore >= bonusRoundThreshold && !HasBonusBeenTriggered) //Bool check in place to prevent the bonus round animations from repeating
        {  
            OnScoreChanged?.Invoke(TotalScore);
            Debug.Log("Current Score: " + TotalScore);
            
            Debug.Log("You've met the threshold!");
            HasBonusBeenTriggered = true;
            
            OnBonusRoundActivated?.Invoke();
            return;
        }

         Debug.Log("Current Score: " + TotalScore);

        //Update the score UI here
        OnScoreChanged?.Invoke(TotalScore);        
    }

    public void ScoreDeduction(int ScoreValue) //Method for handling deduction in score
    {
        totalScore -= ScoreValue;

        if (totalScore <= 0)
        {
            totalScore = 0;    
            OnScoreChanged?.Invoke(TotalScore);
        }

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
            StartCoroutine(MultiplierBarManager.Instance.BarRoutine());

            //Call a new Coroutine that will reset the multi bool once the multiplier duration is up
            StartCoroutine(MultiplierDuration());

            ScorePopUpManager.Instance.DisplayMultiplierPopUp(MultiValue, "x", Color.blue);
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
