using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_TimeDeductionTarget : TargetClass
{
    //Add functionality here

    //Variables for general gameplay effects
    [Header("Target Effects")]
    [SerializeField] private int ScoreValue = 10; //With this target, the player loses some of their score and current time
    [SerializeField] private int ScoreMultiValue;
    [SerializeField] private int TimeDeduction;


    public override void OnHit()
    {
        //Add code here for score and time deduction

        //ScoreManager.Instance.ScoreDeduction(ScoreValue); //Working!!
        //TimeManager.Instance.TimeDeduction(TimeDeduction);
    }
}
