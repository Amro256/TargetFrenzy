using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BasicTarget : TargetClass
{

    //Values for the score, Multiplier, and how much time to deduct for this target
    [Header("Target Effects")]
    [SerializeField] private int ScoreValue = 2; //Changing to this private or static causes issues when trying to isolate the system
    [SerializeField] private int ScoreMultiValue;
    [SerializeField] private int TimeDeduction;


    //Create a method that will handle the score and then call it in the mouse input script
    public override void OnHit()
    {
        ScoreManager.Instance.ScoreIncrease(ScoreValue);

        //ScoreManager.Instance.ScoreMultiplier(ScoreMultiValue); //--Current works with the new system - This is only being called in this script for testing
    }

}


