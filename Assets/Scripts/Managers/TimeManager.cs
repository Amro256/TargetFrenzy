using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{

    public static TimeManager Instance;

    //General variables
    public float timeRemaining = 10;
    public bool isTimerRunning;

    //Actions
    public static event Action<float> OnTimerUpdate;

    void OnEnable()
    {
        TimeIncreaseTarget.OnTimeIncrease += TimeIncrease;
        TimeDeductionTarget.OnTimeDeduction += TimeDeduction;
    }

    void OnDisable()
    {
        TimeIncreaseTarget.OnTimeIncrease -= TimeIncrease;
        TimeDeductionTarget.OnTimeDeduction -= TimeDeduction;

    }


    // void Awake() //Singleton Pattern  
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //     }
    //     else
    //     {
    //         DontDestroyOnLoad(gameObject);
    //     }
    // }


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
                isTimerRunning = false; //Sets the bool to false as the timmer is no longer running
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
}
