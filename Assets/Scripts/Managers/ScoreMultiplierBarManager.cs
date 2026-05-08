using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMultiplierBarManager : MonoBehaviour
{

    //Variables / References
    [SerializeField] private Slider multiplierBar;

    //Duration (Reference the Multiplier target script)
    [SerializeField] private float multiplierDuration; //This will be used to control the duration of the Multiplier -- I've turned this into a property so the MutliplierBar Manager can access it


    // Start is called before the first frame update
    void Start()
    {
        multiplierBar.maxValue = multiplierDuration; //Setting the slider value to the multiplier duration. This will max out the slider's fill when the game starts
        multiplierBar.value = multiplierDuration;
    }

    void Update()
    {
        StartBarDepletion();
    }



    void StartBarDepletion()
    {
        //Check if the multi bool is active first


        //Check if the Bar duration is > 0, and if it is decrement it by time.deltaTime
        if (multiplierDuration > 0)
        {
            multiplierDuration -= Time.deltaTime;

            //Update the UI accordingly
            multiplierBar.value = multiplierDuration;

        }

    }
    
}
