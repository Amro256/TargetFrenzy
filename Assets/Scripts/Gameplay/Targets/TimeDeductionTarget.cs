using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDeductionTarget : TargetClass
{
    //Add functionality here

    //Variables for general gameplay effects
    [Header("Target Effects")]
    [SerializeField] private int ScoreValue; 
    [SerializeField] private int ScoreMultiValue;
    [SerializeField] private int TimeDeduction; //This target's purpose is to ONLY deduct time.

    public override void OnHit()
    {
        //Add code here for time Deduction
        TimeManager.Instance.TimeDeduction(TimeDeduction); //Working!!! 
    }
}
