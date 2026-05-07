using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //Singleton Pattern
    public static GameManager Instance; //Static instance so other scripts can access this

    //General Variables
    private bool IsPaused = false;  //Add a bool here for "IsPaused" - Will be used to track if the game is paused or not

    //14/4/26: The variables below were moved from the player input script to the game manager 
    private int MaxMisses = 5; //Max amount of possible clicks the player has before resulting in a game over
    private int MissCount = 0; //Variable that will track the player's misses 

    //Actions 
    public static event Action OnOutOfAmmo; //--Action: For displaying the pause UI when the player is out of ammo
    public static event Action OnGameStart; //--Action: For disabling the pause UI on start
    public static event Action OnGamePause; //--Action: Enables the pause UI when the game is paused
    public static event Action OnGameResume; //--Action: Disables the pause UI when the game resumes

    void OnEnable()
    {
        PlayerInputHandler.OnPlayerMissedShot += PlayerMissShot;
        TimeManager.OnOutOfTime += TimeOver;
    }

    void OnDisable()
    {
        PlayerInputHandler.OnPlayerMissedShot -= PlayerMissShot;
        TimeManager.OnOutOfTime -= TimeOver;

    }

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
        //Invoke action here  
        OnGameStart?.Invoke(); //What this action does: Disables the "Pause" menu UI when the game starts
    }


    //General Methods 
    public void TimeOver()
    {
        Time.timeScale = 0; //Acts as if the game is "paused"
        Debug.Log("Time Over");

        //Inputs need to be disabled

        //Call method to display the "Pause menu". This will be used for testing

        //Invoke action here  
        //OnOutOfAmmo?.Invoke(); //Working!!

    }

    //General Functions
    public void LoadLevel() //Function that will load the level
    {
        SceneManager.LoadScene("GameScene");
        Debug.Log("Loading Scene");
    }

    //void OptionsMenu() //Will be responsible for opening and closing the options menu

    public void QuitGame() //Will quit the game
    {
        Application.Quit();
        Debug.Log("Quitting!");
    }

    public void PauseGame()
    {
        //Code here for game pausing
        if (!IsPaused)
        {
            IsPaused = true;
            Time.timeScale = 0;
            Debug.Log("Game Currently Paused!");
            //Invoke action here to display pause UI
            OnGamePause?.Invoke();
        }
        else
        {
            IsPaused = false;
            Time.timeScale = 1;
            Debug.Log("Game Resumed!");
            //Invoke action here to hide the pause UI
            OnGameResume?.Invoke();
        }
    }

    public bool IsGamePaused()
    {
        return IsPaused;
    }

    private void PlayerMissShot() //Method responsible for the players' misses! 14/4/26: Moved fom the Player Input script to the Game manager
    {
        MissCount++;
        Debug.Log("Missed Counts: " + MissCount);

        if (MissCount >= MaxMisses)
        {
            //The player input map needs to be disable here, as the game still registers inputs and the "OnFire" method is still called

            //Inputs.Player.Disable(); // This disables the player action map! 
            

            Debug.Log("Game Over");

            //GameManager.Instance.GameOver();

            return;
        }
    }
}
