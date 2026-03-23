using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenTarget : TargetClass
{
    //Add functionality here

    //Variables for general gameplay effects
    [Header("Target Effects")]
    [SerializeField] private int ScoreValue = 5; //Changing to this private or static causes issues when trying to isolate the system
    [SerializeField] private int ScoreMultiValue;
    [SerializeField] private int TimeDeduction = 100;


    public override void OnHit()
    {
        ScoreManager.Instance.ScoreIncrease(ScoreValue);
    }
}
