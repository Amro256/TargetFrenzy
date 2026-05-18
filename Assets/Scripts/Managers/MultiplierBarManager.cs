using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierBarManager : MonoBehaviour
{

    //Singleton Pattern
    public static MultiplierBarManager Instance;

    

    // //Variables / References
    [SerializeField] private Slider multiplierSlider; //11/5/26 - Moved from its own script to the UI manager

    // //Duration (Reference the Multiplier target script)
    [SerializeField] public float maxMultiplierDuration; //This will be used to control the duration of the Multiplier
    
     void Awake() //Singleton pattern
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // // Start is called before the first frame update
    void Start()
    {
        multiplierSlider.maxValue = maxMultiplierDuration; //This sets the slider's max value to multiplier duration value, which is set to 10
        multiplierSlider.value = maxMultiplierDuration; //Sets the bars value to the multiplier duration. Without assigning a value to the slider's value, it will default to 1.
    }


    public IEnumerator BarRoutine()
    {
        multiplierSlider.value = maxMultiplierDuration;

        while (multiplierSlider.value > 0)
        {
            multiplierSlider.value -= Time.deltaTime;
            //Debug.Log("Slider Value: " + multiplierSlider.value);
            yield return null;
        }

        Debug.Log("Multiplier ended!");
        multiplierSlider.value = maxMultiplierDuration; //This resets the slider's value after the multiplier has ended
       
    }

}
