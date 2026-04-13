using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //NameSpace to allow usages of actions
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    //Migrate UI functionality from the game manager here!!

    //General variables / others
    private int ammoIndex;


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
        ScoreManager.OnMultiValueChanged += UpdateMultiUI;

        GameManager.OnGamePause += DisplayPauseMenu; //-Might Change this
        GameManager.OnGameStart += DisablePauseMenu;
        GameManager.OnGameResume += DisablePauseMenu;
        MouseInput.OnPlayerMissUI += DisableAmmoSpriteVisibility;
        TimeManager.OnTimerChange += UpdateTimerUI;

        AmmoManager.OnPlayerReloadUI += EnableAmmoSpriteVisibility;

    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScoreUI;
        ScoreManager.OnMultiValueChanged -= UpdateMultiUI;

        GameManager.OnGamePause -= DisplayPauseMenu; //-- Might change this
        GameManager.OnGameStart -= DisablePauseMenu;
        GameManager.OnGameResume -= DisablePauseMenu;
        MouseInput.OnPlayerMissUI -= DisableAmmoSpriteVisibility;
        TimeManager.OnTimerChange -= UpdateTimerUI;

        AmmoManager.OnPlayerReloadUI -= EnableAmmoSpriteVisibility;
    }


    public void DisableAmmoSpriteVisibility() //Call this method in the mouseInput script
    {
        AmmoManager.Instance.UpdateAmmoValue(1); //Works but replace this later

        if (ammoIndex < ammoSprites.Length)
        {
            ammoSprites[ammoIndex].SetActive(false);
            ammoIndex++;
            Debug.Log("Current Index: " + ammoIndex);
        }
    }

    public void EnableAmmoSpriteVisibility() //Does the opposite of the code above - used for when the player has to reload (Currently not being called)
    {
        // Debug.Log("Sprite re-enabled!"); -- The Function is being called correctly
        
        foreach (GameObject sprites in ammoSprites) //Lol this worked initially, I just had to reset the ammo index back to 0 for the above function to work
        {
            sprites.SetActive(true);
            //Reset the ammo index - so the UI can keep updating accordingly
            ammoIndex = 0; 
        }
    }

    public void UpdateScoreUI(int score)
    {
        ScoreText.text = score.ToString();
    }

    public void UpdateMultiUI(int multiValue)
    {
        MultiText.text = multiValue.ToString();
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
