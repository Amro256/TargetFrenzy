using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoldenTarget : TargetClass
{
    //Add functionality here

    //Variables for general gameplay effects
    [Header("Target Effects")]
    [SerializeField] private int ScoreValue = 5; //Changing to this private or static causes issues when trying to isolate the system
    [SerializeField] private int ScoreMultiValue;
    [SerializeField] private int TimeDeduction;

     //Actions
    public static event Action<int> OnTargetHit;

    public override void OnHit()
    {
        if (GameManager.Instance.BonusRoundBool) //Check to see if its true first
        {
            GameManager.Instance.PlayerHitRowIncrement();

        }

        OnTargetHit?.Invoke(ScoreValue);

        //Update ammo value
        AmmoManager.Instance.UpdateAmmoValue(1);

        ScorePopUpManager.Instance.DisplayScorePopUp(transform.position);
    }
}
