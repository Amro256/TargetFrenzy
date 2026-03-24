using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeIncreaseTarget : TargetClass
{
    //Add functionality here

    //Variables for general gameplay effects
    [Header("Target Effects")]
    [SerializeField] private int ScoreValue; 
    [SerializeField] private int ScoreMultiValue;
    [SerializeField] private int TimeDeduction = 10; //This target's purpose is to add more time 

    public override void OnHit()
    {
        //Add code here for Increasing Time

        TimeManager.Instance.TimeIncrease(TimeDeduction); //Working!!!
    }
}
