using System.Collections;
using UnityEngine;
using System; //NameSpace to allow usages of actions
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class UIManager : MonoBehaviour
{
    //Migrate UI functionality from the game manager here!!

    //General variables / others
    private int ammoIndex;
    [SerializeField] private Animator anim;


    [Header("UI References")]
    [SerializeField] public TMP_Text ScoreText;
    [SerializeField] public TMP_Text TimerText;
    [SerializeField] private Canvas PauseMenuCanvas; //Reference to the Pause Menu Canvas

    [Header("UI Groups")]
    [SerializeField] private GameObject topLeftUIGroup; //Reference to the score, time, and multiplier group
    [SerializeField] private GameObject ammoSpriteGroup; //Reference to the ammo group sitting in the bottom left of the screen
    [SerializeField] private GameObject BonusRoundGroup; //Reference to the ammo group sitting in the bottom left of the screen


    [Header("Game Objects")]
    [SerializeField] public GameObject[] ammoSprites;

    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScoreUI;

        GameManager.OnGamePause += DisplayPauseMenu; //-Might Change this
        GameManager.OnGameStart += DisablePauseMenu;
        GameManager.OnGameResume += DisablePauseMenu;
        PlayerInputHandler.OnPlayerMissUI += ConsumeAmmo;
        TimeManager.OnTimerUpdate += UpdateTimerUI;

        AmmoManager.OnPlayerReloadUI += ReloadAmmoSprites;
        PlayerInputHandler.OnConfirmedHit += ConsumeAmmo;

    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScoreUI;

        GameManager.OnGamePause -= DisplayPauseMenu; //-- Might change this
        GameManager.OnGameStart -= DisablePauseMenu;
        GameManager.OnGameResume -= DisablePauseMenu;
        PlayerInputHandler.OnPlayerMissUI -= ConsumeAmmo;
        TimeManager.OnTimerUpdate -= UpdateTimerUI;

        AmmoManager.OnPlayerReloadUI -= ReloadAmmoSprites;

        PlayerInputHandler.OnConfirmedHit -= ConsumeAmmo;
    }

    void Start()
    {
        BonusRoundGroup.SetActive(false); //Disables the bonusRound Group when the game starts

        StartCoroutine(BonusRoundIntroScreen()); //5/6/26: This is only being called here for testing purposes. It will called somewhere else later
    }

    public void ConsumeAmmo() //Call this method in the mouseInput script
    {

        if (ammoIndex < ammoSprites.Length)
        {
            ammoSprites[ammoIndex].SetActive(false);
            ammoIndex++;
            Debug.Log("Current Index: " + ammoIndex);
        }
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


    public void UpdateTimerUI(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); //Modulo operator - Returns the remainder after division


        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void DisplayPauseMenu() //Method that can be called by the game manager script to display the pause menu
    {
        if (PauseMenuCanvas != null)
        {
            PauseMenuCanvas.enabled = true; //Now the canvas should be enabled when the player gets a game over    
        }
    }

    public void DisablePauseMenu()
    {
        if (PauseMenuCanvas != null)
        {
            PauseMenuCanvas.enabled = false; //Now the canvas should be enabled when the player gets a game over    
        }
    }

    public IEnumerator BonusRoundIntroScreen()
    {
        //1) Disable the top left UI group and the ammo UI group
        topLeftUIGroup.SetActive(false);
        ammoSpriteGroup.SetActive(false);

        //2) Activate the bonus Round Group
        BonusRoundGroup.SetActive(true);

        //3) trigger the text animation
        anim.SetBool("IsBonusActive", true);

        //4) How long to wait before re-activating the other UI groups
        yield return new WaitForSeconds(5f);

        //5)Re-enable the top left / ammo UI groups

        topLeftUIGroup.SetActive(true);
        ammoSpriteGroup.SetActive(true);
    }

    
}
