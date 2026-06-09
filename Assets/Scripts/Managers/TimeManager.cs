using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;
using System.Timers;

public class TimeManager : MonoBehaviour
{
    //General variables
    public float timeRemaining;
    private float BonusTimeRemaining = 31;
    public bool isTimerRunning;

    //Actions
    public static event Action<float> OnTimerUpdate;
    public static event Action OnOutOfTime;

    void OnEnable()
    {
        TimeIncreaseTarget.OnTimeIncrease += TimeIncrease;
        TimeDeductionTarget.OnTimeDeduction += TimeDeduction;
        Score_TimeDeductionTarget.OnTimeDeduction += TimeDeduction;

        BonusRoundManager.OnBonusRoundStartTime += StartBonusRound;
    }

    void OnDisable()
    {
        TimeIncreaseTarget.OnTimeIncrease -= TimeIncrease;
        TimeDeductionTarget.OnTimeDeduction -= TimeDeduction;
        Score_TimeDeductionTarget.OnTimeDeduction -= TimeDeduction;

        BonusRoundManager.OnBonusRoundStartTime -= StartBonusRound;
    }

    // Start is called before the first frame update
    void Start()
    {
        isTimerRunning = true; //Sets the timer bool to true, so the timer starts running when the game starts
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out bozo!");
                timeRemaining = 0; //Sets the time remaining to 0 to prevent it from going into the negatives
                DisplayTime(timeRemaining);
                isTimerRunning = false; //Sets the bool to false as the timmer is no longer running
                OnOutOfTime?.Invoke();
            }

        }
    }

    //Method here to display the time (Now being moved to the Time Manager Script to isolate the system)
    public void DisplayTime(float timeToDisplay)
    {
        //Minutes 
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); //Modulo operator - Returns the remainder after division

        //display the time value

        OnTimerUpdate?.Invoke(timeToDisplay);
    }


    public void TimeIncrease(int timeUpValue)
    {
        //We need to check whether the timer is running. If it is, then perform the code below
        if (isTimerRunning)
        {
            timeRemaining += timeUpValue; //Update the 'time remaining' variable to increase time
            DisplayTime(timeRemaining); //Calling the displayTime method to update the UI
        }
    }

    public void TimeDeduction(int timeDownValue) //We need to check whether the timer is running. If it is, then perform the code below 
    {
        if (isTimerRunning)
        {
            timeRemaining -= timeDownValue; //Update the 'time remaining' variable to deduct time
            DisplayTime(timeRemaining); //Calling the displayTime method to update the UI
        }
    }

    void StartBonusRound() //Method that will be called when the bonus round is active
    {
        //Call the Coroutine here
        StartCoroutine(BonusTimerBuffer());
    }


    IEnumerator BonusTimerBuffer() //Coroutine to temporarily freeze the timer during the bonus round intro screen
    {
        timeRemaining = BonusTimeRemaining; //Sets the time remaining to bonus time remaining (set to 30 seconds for now)
        isTimerRunning = false; //Setting the bool to false means the timer wont run when the bonus round is triggered, 

        yield return new WaitForSeconds(7f); //Wait 5 seconds before running the timer (9/6/26: Changed from 5 seconds to 7 seconds)
        isTimerRunning = true; //Sets the bool back to true to start the timer
    }
}
