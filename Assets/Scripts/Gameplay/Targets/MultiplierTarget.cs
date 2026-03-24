using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierTarget : TargetClass
{
    //Add functionality here

    //Variables for general gameplay effects
    [Header("Target Effects")]
    [SerializeField] private int ScoreValue; //Changing to this private or static causes issues when trying to isolate the system
    [SerializeField] private int ScoreMultiValue = 4;
    [SerializeField] private int TimeDeduction;


    public override void OnHit()
    {
        //Add Mutlivalue here
        //ScoreManager.Instance.ScoreMultiplier(ScoreMultiValue); //--Current works with the new system - This is only being called in this script for testing
    }
}



