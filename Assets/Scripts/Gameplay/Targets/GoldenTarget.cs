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
        OnTargetHit?.Invoke(ScoreValue);

        //Update ammo value
        AmmoManager.Instance.UpdateAmmoValue(1);
    }
}
