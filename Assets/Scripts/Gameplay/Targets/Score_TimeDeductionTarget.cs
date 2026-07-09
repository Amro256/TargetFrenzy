using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Score_TimeDeductionTarget : TargetClass
{
    //Add functionality here

    //Variables for general gameplay effects
    [Header("Target Effects")]
    [SerializeField] private int ScoreValue = 10; //With this target, the player loses some of their score and current time
    [SerializeField] private int ScoreMultiValue;
    [SerializeField] private int TimeValue;

    //Actions 
    public static event Action<int> OnScoreDeduction;
    public static event Action<int> OnTimeDeduction;


    public override void OnHit()
    {
        GameManager.Instance.PlayerHitRowDecrement();

        //Add code here for score and time deduction
        AmmoManager.Instance.UpdateAmmoValue(1);
        
        OnScoreDeduction?.Invoke(ScoreValue);
        OnTimeDeduction?.Invoke(TimeValue);
        //TimeManager.Instance.TimeDeduction(TimeDeduction);
    }
}
