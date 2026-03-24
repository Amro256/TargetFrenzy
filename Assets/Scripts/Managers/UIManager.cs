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
    private int ammoIndex = 0;


    [Header("UI References")]
    [SerializeField] public TMP_Text ScoreText;
    [SerializeField] public TMP_Text TimerText;
    [SerializeField] public TMP_Text MultiText;
    [SerializeField] private Canvas PauseMenuCanvas; //Reference to the Pause Menu Canvas

    [Header("Game Objects")]
    [SerializeField] public GameObject[] ammoSprites;


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


    public void ToggleAmmoSpriteVisibility() //Call this method in the mouseInput script
    {
        AmmoManager.Instance.UpdateAmmoValue(1); //Works

        if (ammoIndex < ammoSprites.Length)
        {
            ammoSprites[ammoIndex].SetActive(false);
            ammoIndex++;
        }
    }

    public void DisplayPauseMenu() //Method that can be called by the game manager script to display the pause menu
    {
        //Add code here
        PauseMenuCanvas.enabled = true; //Now the canvas should be enabled when the player gets a game over
    }

     public void DisablePauseMenu() 
    {
        //Add code here
        PauseMenuCanvas.enabled = false;
    }
    
}
