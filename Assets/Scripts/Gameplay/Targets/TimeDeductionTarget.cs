using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeDeductionTarget : TargetClass
{
    //Add functionality here

    //Variables for general gameplay effects
    [Header("Target Effects")]
    [SerializeField] private int ScoreValue; 
    [SerializeField] private int ScoreMultiValue;
    [SerializeField] private int TimeValue; //This target's purpose is to ONLY deduct time.

    //Actions
    public static event Action<int> OnTimeDeduction;

    public override void OnHit()
    {
        //Add code here for time Deduction

        OnTimeDeduction?.Invoke(TimeValue);

    }
}
