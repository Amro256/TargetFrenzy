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
    private bool isPaused;  //Add a bool here for "IsPaused" - Will be used to track if the game is paused or not

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
        UIManager.Instance.isTimerRunning = true; //Sets the timer bool to true, so the timer starts running when the game starts
    }


    //Re-add the update method to handle time
    void Update() //
    {
        if (UIManager.Instance.isTimerRunning)
        {
            if (UIManager.Instance.timeRemaining > 0)
            {
                UIManager.Instance.timeRemaining -= Time.deltaTime;
                UIManager.Instance.displayTime(UIManager.Instance.timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out bozo!");
                UIManager.Instance.timeRemaining = 0; //Sets the tiem remaining to 0 to prevent it from going into the negatives
                UIManager.Instance.isTimerRunning = false; //Sets the bool to false as the timmer is no longer running
            }
        }
    }

    //General Methods 
    public void GameOver()
    {
        Time.timeScale = 0; //Acts as if the game is "paused"
        Debug.Log("Gg lol");

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
