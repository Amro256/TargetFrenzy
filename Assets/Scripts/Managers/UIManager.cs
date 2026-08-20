using System.Collections;
using UnityEngine;
using System; //Namespace to allow usages of actions
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    //Migrate UI functionality from the game manager here!!
    public static UIManager Instance;

    //General variables / others
    private int ammoIndex;


    [Header("Animator Reference")]
    [SerializeField] private Animator anim;


    [Header("UI Text References")]
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text TimerText;
    [SerializeField] private TMP_Text FinalScoreText;
    [SerializeField] private TMP_Text TargetCounterText;

    [Header("UI Canvas References")]
    public Canvas PauseMenuCanvas; //Reference to the Pause Menu Canvas
    public Canvas GameOverCanvas; //Reference to the Game Over Canvas
    public Canvas GameHudCanvas; //Reference to the Game Over Canvas

    [Header("UI Game Objects References")]
    [SerializeField] private GameObject BonusStartText; //Reference to gameobject containing the text for the start of the bonus round
    [SerializeField] private GameObject BonusCountdownTest; //Reference to gameobject containing the countdown text for the bonus round
    [SerializeField] public GameObject ReloadWarningText;


    [Header("UI Groups")]
    [SerializeField] private GameObject[] gameHUD; //Reference to the score, time, and multiplier group //11/6/26: Changed to an array for refactoring purposes
    [SerializeField] private GameObject BonusRoundGroup; //Reference to the bonus round UI group


    [Header("Ammo Sprite Objects")]
    [SerializeField] public GameObject[] ammoSprites; //Reference to the ammo group sitting in the bottom left of the screen

    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScoreUI;

        GameManager.OnGamePause += DisplayMenu; //-Might Change this
        GameManager.OnGameStart += DisableMenu;
        GameManager.OnGameResume += DisableMenu;
        GameManager.OnTimeOver += DisplayMenu;

        PlayerInputHandler.OnPlayerMissUI += ConsumeAmmo;
        TimeManager.OnTimerUpdate += UpdateTimerUI;

        AmmoManager.OnPlayerReloadUI += ReloadAmmoSprites;
        PlayerInputHandler.OnConfirmedHit += ConsumeAmmo;

    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScoreUI;

        GameManager.OnGamePause -= DisplayMenu; //-- Might change this
        GameManager.OnGameStart -= DisableMenu;
        GameManager.OnGameResume -= DisableMenu;
        GameManager.OnTimeOver -= DisplayMenu;
        
        PlayerInputHandler.OnPlayerMissUI -= ConsumeAmmo;
        TimeManager.OnTimerUpdate -= UpdateTimerUI;

        AmmoManager.OnPlayerReloadUI -= ReloadAmmoSprites;

        PlayerInputHandler.OnConfirmedHit -= ConsumeAmmo;
    }

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
        BonusRoundGroup.SetActive(false); //Disables the bonusRound Group when the game starts
        GameOverCanvas.gameObject.SetActive(false); //Disable the game over canvas on start
        PauseMenuCanvas.gameObject.SetActive(false);

        ReloadWarningText.SetActive(false);

        StartCoroutine(StartUpSequence.Instance.BeginStartUpSequence());
    }

    public void ConsumeAmmo() //Call this method in the mouseInput script
    {

        if (ammoIndex < ammoSprites.Length)
        {
            ammoSprites[ammoIndex].SetActive(false);
            ammoIndex++;
        }

        Debug.Log("Current Index: " + ammoIndex);
    }

    public void ReloadAmmoSprites() //Does the opposite of the code above - used for when the player has to reload (Currently not being called --Is working as of 5/5/26)
    {
        // Debug.Log("Sprite re-enabled!"); -- The Function is being called correctly

        foreach (GameObject sprites in ammoSprites) //Lol this worked initially, I just had to reset the ammo index back to 0 for the above function to work
        {
            sprites.SetActive(true);

        }
        //Reset the ammo index - so the UI can keep updating accordingly
        ammoIndex = 0;
    }

    public void UpdateScoreUI(int score)
    {
        ScoreText.text = score.ToString();
    }

    public void UpdateFinalScoreUI(int finalScore)
    {
        FinalScoreText.text = finalScore.ToString();
    }


    public void UpdateTimerUI(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); //Modulo operator - Returns the remainder after division


        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void UpdateCounterUI(int value)
    {
        TargetCounterText.text = value.ToString();
    }


    public void DisplayMenu(Canvas UIMenu) //Method that can be called by the game manager script to display the pause menu
    {
        if (UIMenu != null)
        {
            UIMenu.enabled = true; //Now the canvas should be enabled when the player gets a game over    
        }
    }

    public void DisableMenu(Canvas UIMenu)
    {
        if (UIMenu != null)
        {
            UIMenu.enabled = false; //Now the canvas should be enabled when the player gets a game over    
        }
    }

    public IEnumerator BonusRoundIntroScreen()
    {
        //1) Disable the top left UI group and the ammo UI group & disable player input

        foreach (GameObject hudElements in gameHUD)
        {
            hudElements.SetActive(false);
        }

        //Disables player input
        PlayerInputHandler.instance.DisableAllPlayerActions();
        PlayerInputHandler.instance.DisablePauseAction();

        BonusStartText.SetActive(false);
        BonusCountdownTest.SetActive(false);

        //2) Activate the bonus Round Group
        BonusRoundGroup.SetActive(true);
        StartCoroutine(BonusRoundTextAnim());

        //3) trigger the text animation
        anim.SetBool("IsBonusActive", true);

        //4) How long to wait before re-activating the other UI groups
        yield return new WaitForSeconds(7f); // 9/6/26: Changed from 5 seconds to 7 seconds)

        ReloadAmmoSprites(); //This is being called to update the ammo amount (visually)

        //5)Re-enable the top left / ammo UI groups
        BonusRoundGroup.SetActive(false);

        foreach (GameObject hudElements in gameHUD)
        {
            hudElements.SetActive(true);
        }

        //6) Re-enable player input
        PlayerInputHandler.instance.EnableAllPlayerActions();
        PlayerInputHandler.instance.EnablePauseAction();
    }


    IEnumerator BonusRoundTextAnim() //Coroutine to control the timing of the two bonus round texts' : "Bonus Round" should play first followed by the countdown timer
    {
        BonusStartText.SetActive(true); //Enable the "Bonus Round" text

        yield return new WaitForSeconds(2.5f); //Wait 2.5 seconds before disabling the previous text and enabling the countdown text
        BonusStartText.SetActive(false); //Disables the "Bonus Round text

        BonusCountdownTest.SetActive(true); //Enable the countdown text

        //There's no need to wait for xyz seconds to disable the countdown text, as the whole group will be disabled in the "BonusRoundIntroScreen" coroutine
    }

}
