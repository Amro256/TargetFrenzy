using System.Collections;
using UnityEngine;
using System; //Namespace to allow usages of actions
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Migrate UI functionality from the game manager here!!
    public static UIManager Instance { get; private set; }

    //General variables / others
    private int spriteIndex; //For tracking the ammo sprites

    #region References
    [Header("Animator Reference")]
    [SerializeField] private Animator anim;


    [Header("UI Text References")]
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text TimerText;
    [SerializeField] private TMP_Text FinalScoreText;
    [SerializeField] private TMP_Text HighScoreText;
    [SerializeField] private TMP_Text TargetCounterText;
    [SerializeField] private TMP_Text MultiValueText;

    [Header("UI Canvas References")]
    [SerializeField] private Canvas PauseMenuCanvas; //Reference to the Pause Menu Canvas
    [SerializeField] private Canvas GameOverCanvas; //Reference to the Game Over Canvas
    [SerializeField] private Canvas GameHudCanvas; //Reference to the game hud

    [Header("UI Game Objects References")]
    [SerializeField] private GameObject BonusStartText; //Reference to gameobject containing the text for the start of the bonus round
    [SerializeField] private GameObject BonusCountdownText; //Reference to gameobject containing the countdown text for the bonus round
    [SerializeField] private GameObject ReloadWarningText;


    [Header("UI Groups")]
    [SerializeField] private GameObject[] gameHUD; //Reference to the score, time, and multiplier group //11/6/26: Changed to an array for refactoring purposes
    [SerializeField] private GameObject BonusRoundGroup; //Reference to the bonus round UI group

    [Header("Ammo Sprite Objects")]
    [SerializeField] private GameObject[] ammoSprites; //Reference to the ammo group sitting in the bottom left of the screen

    [Header("Others")]
    private float currentDisplayScore = 0;
    private float currentHighScoreDisplay = 0;
    #endregion

    #region Actions
    public static event Action OnInputsEnable;
    public static event Action OnInputsDisable;
    #endregion

    #region Enable & Disable Methods
    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScoreUI;
        ScoreManager.OnHighScore += UpdateHighScoreUI;

        GameManager.OnGamePause += ShowPauseMenu;
        GameManager.OnGameStart += HidePauseMenu;

        GameManager.OnGameResume += HidePauseMenu;
        GameManager.OnTimeOver += ShowTimeOverScreen;

        PlayerInputHandler.OnPlayerMissUI += ConsumeAmmo;
        TimeManager.OnTimerUpdate += UpdateTimerUI;

        AmmoManager.OnReloadSprites += ReloadAmmoSprites;
        PlayerInputHandler.OnConfirmedHit += ConsumeAmmo;

    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScoreUI;
        ScoreManager.OnHighScore -= UpdateHighScoreUI;


        GameManager.OnGamePause -= ShowPauseMenu;
        GameManager.OnGameStart -= HidePauseMenu;


        GameManager.OnGameResume -= HidePauseMenu;
        GameManager.OnTimeOver -= ShowTimeOverScreen;

        PlayerInputHandler.OnPlayerMissUI -= ConsumeAmmo;
        TimeManager.OnTimerUpdate -= UpdateTimerUI;

        AmmoManager.OnReloadSprites -= ReloadAmmoSprites;

        PlayerInputHandler.OnConfirmedHit -= ConsumeAmmo;
    }
    #endregion

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
    private void Start()
    {
        InitialiseUI();
    }

    void InitialiseUI()
    {
        BonusRoundGroup.SetActive(false); //Disables the bonusRound Group when the game starts
        GameOverCanvas.gameObject.SetActive(false); //Disable the game over canvas on start
        PauseMenuCanvas.gameObject.SetActive(false);

        ReloadWarningText.SetActive(false);
        MultiValueText.gameObject.SetActive(false);
    }

    #region Ammo Related Methods
    public void ConsumeAmmo() //Call this method in the mouseInput script
    {
        if (spriteIndex < ammoSprites.Length)
        {
            ammoSprites[spriteIndex].SetActive(false);
            spriteIndex++;
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
        spriteIndex = 0;
    }
    #endregion

    #region HUD Related Methods
    public void UpdateScoreUI(int score)
    {
        ScoreText.text = score.ToString();
    }

    public void UpdateFinalScoreUI(int finalScore)
    {
        FinalScoreText.text = finalScore.ToString();
    }

    public void UpdateHighScoreUI(int highScore)
    {
        HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void UpdateTimerUI(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); //Modulo operator - Returns the remainder after division


        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateTargetCounterUI(int value)
    {
        TargetCounterText.text = value.ToString();
    }

    public void UpdateMultiValueText(int multiValue)
    {
        MultiValueText.gameObject.SetActive(true);
        MultiValueText.text = "x" + multiValue.ToString();
        MultiValueText.color = new Color32(71, 197, 255, 255);
    }

    public void ShowReloadWarning()
    {
        ReloadWarningText.gameObject.SetActive(true);
    }

    public void HideReloadWarning()
    {
        ReloadWarningText.gameObject.SetActive(false);
    }

    public void HideMultiValueText()
    {
        MultiValueText.gameObject.SetActive(false);
    }

    public void ShowBonusText()
    {
        BonusStartText.gameObject.SetActive(true);
    }

    public void HideBonusText()
    {
        BonusStartText.gameObject.SetActive(false);
    }

    public void ShowCountdownText()
    {
        BonusCountdownText.gameObject.SetActive(true);
    }

    public void HideCountdownText()
    {
        BonusCountdownText.gameObject.SetActive(false);
    }
    #endregion

    #region Game Menu Methods
    public void ShowTimeOverScreen()
    {
        GameOverCanvas.gameObject.SetActive(true);
    }

    public void HideTimeOverScreen()
    {
        GameOverCanvas.gameObject.SetActive(false);
    }

    public void ShowMainHud()
    {
        GameHudCanvas.gameObject.SetActive(true);
    }

    public void HideMainHud()
    {
        GameHudCanvas.gameObject.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        PauseMenuCanvas.gameObject.SetActive(true);
    }

    public void HidePauseMenu()
    {
        PauseMenuCanvas.gameObject.SetActive(false);
    }
    #endregion

    #region Coroutines
    public IEnumerator BonusRoundIntroScreen()
    {
        //1) Disable the hud
        foreach (GameObject hudElement in gameHUD)
        {
            hudElement.SetActive(false);
        }

        //Disables player input - Using action
        OnInputsDisable?.Invoke();

        HideBonusText();
        HideCountdownText();

        //2) Activate the bonus Round Group
        BonusRoundGroup.gameObject.SetActive(true);
        StartCoroutine(BonusRoundTextAnim());

        //3) trigger the text animation
        anim.SetBool("IsBonusActive", true);

        //4) How long to wait before re-activating the other UI groups
        yield return new WaitForSeconds(7f); // 9/6/26: Changed from 5 seconds to 7 seconds)

        ReloadAmmoSprites(); //This is being called to update the ammo amount (visually)

        BonusRoundGroup.gameObject.SetActive(false);

        //5)Re-enable the top left / ammo UI groups
        foreach (GameObject hudElement in gameHUD)
        {
            hudElement.SetActive(true);
        }

        //6) Re-enable player input - Using action
        OnInputsEnable?.Invoke();
    }


    IEnumerator BonusRoundTextAnim() //Coroutine to control the timing of the two bonus round texts' : "Bonus Round" should play first followed by the countdown timer
    {
        ShowBonusText(); //Enable the "Bonus Round" text

        yield return new WaitForSeconds(2.5f); //Wait 2.5 seconds before disabling the previous text and enabling the countdown text
        HideBonusText(); //Disables the "Bonus Round text

        ShowCountdownText(); //Enable the countdown text - "3... 2... 1... Go!"

        //There's no need to wait for xyz seconds to disable the countdown text, as the whole group will be disabled in the "BonusRoundIntroScreen" coroutine
    }

    public IEnumerator FinalScoreTally()
    {
        while (currentDisplayScore < ScoreManager.Instance.TotalScore)
        {
            currentDisplayScore = Mathf.MoveTowards(currentDisplayScore, ScoreManager.Instance.TotalScore, 500f * Time.deltaTime);
            FinalScoreText.text = Mathf.FloorToInt(currentDisplayScore).ToString();
            yield return null;
        }
    }

    #endregion

}