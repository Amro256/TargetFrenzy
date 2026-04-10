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

    private bool IsPaused = false;  //Add a bool here for "IsPaused" - Will be used to track if the game is paused or not

    //Actions 
    public static event Action OnOutOfAmmo; //--Action: For displaying the pause UI when the player is out of ammo
    public static event Action OnGameStart; //--Action: For disabling the pause UI on start--
    public static event Action OnGamePause;
    public static event Action OnGameResume;



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
    public void GameOver()
    {
        Time.timeScale = 0; //Acts as if the game is "paused"
        Debug.Log("Gg lol");

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
        Debug.Log("Quitting the application");
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
}
