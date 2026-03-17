using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDeductionTarget : TargetClass
{
    //Add functionality here

    //Variables for general gameplay effects
    [Header("Target Effects")]
    [SerializeField] private int score; //Changing to this private or static causes issues when trying to isolate the system
    [SerializeField] private int scoreMultiplier;
    [SerializeField] private int timePenalty;  
}
