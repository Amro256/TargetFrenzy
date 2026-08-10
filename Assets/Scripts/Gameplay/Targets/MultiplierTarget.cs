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
        base.OnHit();
        
        GameManager.Instance.PlayerHitRowIncrement();
        AmmoManager.Instance.UpdateAmmoValue(1);
        //Add Mutlivalue here -Invoke action!
        OnTargetHit?.Invoke(ScoreValue);

        if (GameManager.Instance.BonusRoundBool != false) // To prevent the multiplier being triggered during the bonus round intro
        {
            return;
        }
        else
        {
             OnMultiplierActive?.Invoke(ScoreMultiValue);
        }
        ScorePopUpManager.Instance.DisplayScorePopUp(transform.position, ScoreValue, "+", Color.green);
    }
}



