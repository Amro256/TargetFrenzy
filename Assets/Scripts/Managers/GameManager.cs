using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //Singleton Pattern
    public static GameManager Instance {get; private set;} //Static instance so other scripts can access this

    //References
    public int targetHitInARow; //To track the targets hit
    [SerializeField] private int maxTargetsToHit = 10;
    [SerializeField] private Texture2D targetReticleTexture;
    [SerializeField] private SpawnerClass[] spawners;

    //General Variables - Bool   
    private bool isPaused; //Add a bool here for "IsPaused" - Will be used to track if the game is paused or not
    private bool IsBonusRActive = false;
    private bool isGameOver;
    private bool isIntroSeqPlaying { get; set; }

    public bool IsIntroSeqPlaying
    {
        get { return isIntroSeqPlaying; }
        set { isIntroSeqPlaying = value; }
     }

    public bool BonusRoundBool
    {
        get { return IsBonusRActive; }
        set { IsBonusRActive = value; }
    }

    public bool IsPaused
    {
        get { return isPaused; }
        set { isPaused = value; }
    }


    //14/4/26: The variables below were moved from the player input script to the game manager 
    private int MaxMisses = 5; //Max amount of possible clicks the player has before resulting in a game over
    private int MissCount = 0; //Variable that will track the player's misses 

    #region Actions
    //Actions 
    public static event Action OnOutOfAmmo; //--Action: For displaying the pause UI when the player is out of ammo
    public static event Action OnMaxTargetsRowHit;
    public static event Action OnGameStart; //--Action: For disabling the pause UI on start
    public static event Action OnGamePause; //--Action: Enables the pause UI when the game is paused
    public static event Action OnGameResume; //--Action: Disables the pause UI when the game resumes
    public static event Action OnTimeOver; //--Action: Enable the timer over canvas when the player runs out of time
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
        //Set the intro sequence bool to true BEFORE start
        isIntroSeqPlaying = true;
    }


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

    void Start()
    {
        IsBonusRActive = false;
        //Start to coroutine for the startup sequence
        StartCoroutine(StartUpSequence.Instance.BeginStartUpSequence());
        isGameOver = false;
    }

    public void UpdateMouseCursor() //Call this method when the player is hovering over a target
    {
        Cursor.SetCursor(targetReticleTexture, Vector2.zero, CursorMode.Auto);
    }

    //General Methods 
    public void TimeOver()
    {
        isGameOver = true;

        //Disable the player's fire and reload input
        PlayerInputHandler.instance.DisableAllPlayerActions();
        PlayerInputHandler.instance.DisablePauseAction();

        //Call method to display the "Pause menu". This will be used for testing - 15/6/26: This will now be changed to the game over screen

        // 1) Destroy any targets currently on screen --12/8/26: Changed to disabling the spawners
        foreach (var spawner in spawners)
        {
            spawner.gameObject.SetActive(false); //Disables the spawners
            spawner.DestroyTargets();
        }

        // 2) Display the game over panel here
        OnTimeOver?.Invoke();

        // 3) Disable the main game hud
        UIManager.Instance.HideMainHud();

        // 4) Update the "final score" field displayed on the game over panel
        UIManager.Instance.UpdateFinalScoreUI(ScoreManager.Instance.TotalScore);
    }

    public void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0;
        Debug.Log("Game Currently Paused!");
        //Invoke action here to display pause UI
        OnGamePause?.Invoke();
    }

    public void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1;
        Debug.Log("Game Resumed!");
        //Invoke action here to hide the pause UI
        OnGameResume?.Invoke();
    }

    public void PauseCheck() //Check to see if the bool value is NOT false then execute the code below
    {
        if (!IsPaused) //24/8/26: This was sitting in the "PlayerInputHandler" script for whatever reason (thanks past me!)
        {
            //Call the pause game method
            PauseGame();
            Debug.Log("Actions disabled");
            PlayerInputHandler.instance.DisableAllPlayerActions();
        }
        else
        {
            ResumeGame(); //Call the resume game method
            Debug.Log("Actions enabled");
            PlayerInputHandler.instance.EnableAllPlayerActions();
        }
    }

    private void PlayerMissShot() //Method responsible for the players' misses! 14/4/26: Moved fom the Player Input script to the Game manager
    {
        MissCount++;
        Debug.Log("Missed Counts: " + MissCount);

        //Call the player row decrement method -- 21/7/26: Changed from calling the Decrement method to resetting the value
        targetHitInARow = 0;
        UIManager.Instance.UpdateTargetCounterUI(targetHitInARow);

        StartCoroutine(CameraShake.Instance.BeginScreenShake(0.35f, 0.15f));

        // if (MissCount >= MaxMisses)
        // {
        //     Debug.Log("Game Over");
        //     TimeOver();            
        // }
    }

    public void PlayerHitRowIncrement()
    {
        targetHitInARow++;

        if (targetHitInARow == maxTargetsToHit && !BonusRoundBool) //Additional check to prevent targets hit in the bonus round triggering another bonus round
        {
            Debug.Log("You hit: " + targetHitInARow + " In a row! Entering Bonus Round");
            OnMaxTargetsRowHit?.Invoke();

        }

        UIManager.Instance.UpdateTargetCounterUI(targetHitInARow);
    }

    //Method to track how many targets the player as hit in a row
    public void PlayerHitRowDecrement()
    {
        if (targetHitInARow > 0) //Check to see if the targets hit is greater than 0 before decrementing the value
        {
            targetHitInARow--; //This also prevents the value from going into the negatives
        }

        UIManager.Instance.UpdateTargetCounterUI(targetHitInARow);
    }
}
