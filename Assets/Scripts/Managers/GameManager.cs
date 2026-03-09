using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//For Initial UI testing
using UnityEngine.UI;
using TMPro;
using System.Runtime.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    //Initial UI Testing - Refactor when its best
    [SerializeField] public TMP_Text scoreText;
    [SerializeField] public GameObject[] ammoSprites;
    //Add a bool here for "IsPaused"

    void Awake()
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
