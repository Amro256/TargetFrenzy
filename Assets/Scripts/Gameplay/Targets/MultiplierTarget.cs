using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MultiplierTarget : TargetClass
{
    //Add functionality here

    //Variables for general gameplay effects
    [Header("Target Effects")]
    [SerializeField] private int ScoreValue; //Changing to this private or static causes issues when trying to isolate the system
    [SerializeField] private int ScoreMultiValue;
    [SerializeField] private int TimeDeduction;

    //Actions
    public static event Action<int> OnTargetHit;
    public static event Action<int> OnMultiplierActive;


    public override void OnHit()
    {
        GameManager.Instance.PlayerHitRowIncrement();

        //Add Mutlivalue here -Invoke action!
        OnMultiplierActive?.Invoke(ScoreMultiValue);
        OnTargetHit?.Invoke(ScoreValue);
        AmmoManager.Instance.UpdateAmmoValue(1);
        

        ScorePopUpManager.Instance.DisplayScorePopUp(transform.position, ScoreValue, "+", Color.green);
    }
}



