using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //General variables and other things 
    public static GameManager Instance; //Static instance so other scripts can access this
    //private bool isPaused;  //Add a bool here for "IsPaused" - Will be used to track if the game is paused or not

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

    void Start()
    {
        // UIManager.Instance.isTimerRunning = true; 
        //UIManager.Instance.DisablePauseMenu(); //Update these to use an event driven approach instead
    }


    //General Methods 
    public void GameOver()
    {
        Time.timeScale = 0; //Acts as if the game is "paused"
        Debug.Log("Gg lol");

        //Call method to display the "Pause menu". This will be used for testing
        //UIManager.Instance.DisplayPauseMenu(); - This is working! It has been commented out for now, though

    }

    //Main Menu Functions
    public void LoadLevel() //Function that will load the level
    {
        SceneManager.LoadScene("GameScene");
        Debug.Log("Loading Scene");
    }

    //void OptionsMenu() //Will be responsible for opening and closing the options menu

    public void QuitGame() //Will quit the game
    {
        Application.Quit();
        Debug.Log("Quitting the application");
    }

    //Add a method here for gamePausing and calling the display pause UI from the UI manager
}
