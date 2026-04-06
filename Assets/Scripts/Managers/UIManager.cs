using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //NameSpace to allow usages of actions
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Migrate UI functionality from the game manager here!!

    //General variables / others
    private int ammoIndex = 0;


    [Header("UI References")]
    [SerializeField] public TMP_Text ScoreText;
    [SerializeField] public TMP_Text TimerText;
    [SerializeField] public TMP_Text MultiText;
    [SerializeField] private Canvas PauseMenuCanvas; //Reference to the Pause Menu Canvas

    [Header("Game Objects")]
    [SerializeField] public GameObject[] ammoSprites;


    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScoreUI;
        GameManager.OnOutOfAmmo += DisplayPauseMenu;
        GameManager.OnGameStart += DisablePauseMenu;
        MouseInput.OnPlayerMissUI += ToggleAmmoSpriteVisibility;
        TimeManager.OnTimerChange += UpdateTimerUI;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScoreUI;
        GameManager.OnOutOfAmmo -= DisplayPauseMenu;
        GameManager.OnGameStart -= DisablePauseMenu;
        MouseInput.OnPlayerMissUI -= ToggleAmmoSpriteVisibility;
        TimeManager.OnTimerChange -= UpdateTimerUI;
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

    public void UpdateScoreUI(int score)
    {
        ScoreText.text = score.ToString();
    }

     public void UpdateTimerUI(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); //Modulo operator - Returns the remainder after division


        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
