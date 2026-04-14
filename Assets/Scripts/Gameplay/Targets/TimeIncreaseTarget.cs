using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeIncreaseTarget : TargetClass
{
    //Add functionality here

    //Variables for general gameplay effects
    [Header("Target Effects")]
    [SerializeField] private int ScoreValue; 
    [SerializeField] private int ScoreMultiValue;
    [SerializeField] private int TimeValue = 10; //This target's purpose is to add more time 

    //Actions
    public static event Action<int> OnTimeIncrease;

    public override void OnHit()
    {
        //Add code here for Increasing Time

        OnTimeIncrease?.Invoke(TimeValue);
    }
}
