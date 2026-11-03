using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownManager : MonoBehaviour
{

    //This script will handle the 3,2,1 countdown timer that will take place prior to the bonus round! 
    //Decided to isolate this from the UI manager to prevent it from becoming too bloated

    //Singleton
    public static CountdownManager Instance { get; private set; }

    //General variables
    [SerializeField] private int countdownTime;
    [SerializeField] private TextMeshProUGUI countdownText;

    [Header("Resume Only Objects")]
    [SerializeField] private GameObject resumeCountdownUI;
    [SerializeField] private TextMeshProUGUI resumeCountdownText;

    [Header("Scriptable Object Reference")]
    [SerializeField] private AudioData countdownAudio;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        resumeCountdownUI.SetActive(false);
    }


    //Use a coroutine to decrement the countdown timer    
    public IEnumerator CountdownTimer()
    {
        yield return new WaitForSeconds(2.5f); //This is here to prevent THIS coroutine starting at the same time as the "bonus round" text. Meaning this will start after the completion of the first coroutine


        AudioManager.Instance.PlayUISFX(countdownAudio.Clips[1], 1f);

        //Check to see if the countdown timer is greater than 0 (use a while loop)

        while (countdownTime > 0)
        {
            //While the timer is greater than 0, assign the timer value to the text value
            countdownText.text = countdownTime.ToString();

            //How long to wait (in seconds) before calling it again?
            yield return new WaitForSeconds(1f);

            //Decrement
            countdownTime--;

        }

        countdownText.text = "GO!";

        //How long to wait again (in seconds) before disabling the text gameObject?
        yield return new WaitForSeconds(2f);

        //Disable the gameObject that the countdown text is attached to
        countdownText.gameObject.SetActive(false);


        //Reset countdown timer
        countdownTime = 3;

    }


    //Same method as above except this is for the resume countdown timer   
    public IEnumerator ResumeCountdownTimer()
    {
        if (!GameManager.Instance.IsResumeCountdownPlaying) yield break; //Check if the bool value is "true". If so, display the countdown

        Debug.Log("Playing Resume Countdown");

        DisableElements();

        UIManager.Instance.HideReloadWarning();

        //Show the countdown text
        resumeCountdownUI.SetActive(true);

        AudioManager.Instance.PlayUISFX(countdownAudio.Clips[1], 1f);

        //Check to see if the countdown timer is greater than 0 (use a while loop)
        while (countdownTime > 0)
        {
            //While the timer is greater than 0, assign the timer value to the text value
            resumeCountdownText.text = countdownTime.ToString();

            //How long to wait (in seconds) before calling it again?
            yield return new WaitForSecondsRealtime(1f);

            //Decrement
            countdownTime--;
        }

        resumeCountdownText.text = "GO!";

        //How long to wait again (in seconds) before disabling the text gameObject?
        yield return new WaitForSecondsRealtime(2f);

        //Disable the gameObject that the countdown text is attached to
        resumeCountdownUI.SetActive(false);

        //Resumes Gameplay
        GameManager.Instance.ResumeGameplay();

        EnableElements();

        //Reset Bool
        GameManager.Instance.IsResumeCountdownPlaying = false;

        //Reset countdown timer
        countdownTime = 3;

    }


    public void DisableElements()
    {
        //Hide the Pause Menu
        UIManager.Instance.HidePauseMenu();

        //disable touch input
        PlayerInputHandler.instance.DisableTouchActions();

        //Disable the Pause and Reload Buttons
        UIManager.Instance.DisablePauseAndReloadButton();
    }

    public void EnableElements()
    {
        //re-enable inputs after checking if the player is out of ammo
        if (AmmoManager.Instance.IsAmmoEmpty())
        {
            PlayerInputHandler.instance.DisableTouchActions();
        }
        else
        {
            PlayerInputHandler.instance.EnableTouchActions();
        }
        //Re-enable the buttons
        UIManager.Instance.EnablePauseAndReloadButton();
    }
}




