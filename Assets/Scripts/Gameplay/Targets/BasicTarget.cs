using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System;

public class BasicTarget : TargetClass
{
    //Values for the score, Multiplier, and how much time to deduct for this target
    [Header("Target Effects")]
    [SerializeField] private int ScoreValue = 2; //Changing to this private or static causes issues when trying to isolate the system
    [SerializeField] private int ScoreMultiValue;
    [SerializeField] private int TimeDeduction;

    //Actions
    public static event Action<int> OnTargetHit;


    //Create a method that will handle the score and then call it in the mouse input script
    public override void OnHit()
    {
        GameManager.Instance.PlayerHitRowIncrement();

        OnTargetHit?.Invoke(ScoreValue);

        //Update the ammo value
        AmmoManager.Instance.UpdateAmmoValue(1);

        ScorePopUpManager.Instance.DisplayScorePopUp(transform.position, ScoreValue, "+", Color.green);
    }

}


