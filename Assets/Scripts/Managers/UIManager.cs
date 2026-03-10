using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Migrate UI functionality from the game manager here!!!
    public static UIManager Instance; //Static instance so other scripts can access this



    //General variables / others
    public float timeRemaining = 10;
    public bool isTimerRunning;


    [Header("TMP Text References")]
    [SerializeField] public TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;

    [Header("Game Objects")]
    [SerializeField] private GameObject[] ammoSprites;


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


    //Method here to display the time
    public void displayTime(float timeToDisplay)
    {
        //Minutes 
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); //Modulo operator - Returns the remainder after division

        //display the time value
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
