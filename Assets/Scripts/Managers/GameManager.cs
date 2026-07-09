using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //Singleton Pattern
    public static GameManager Instance; //Static instance so other scripts can access this

    //References
    PlayerInputHandler PlayerInput;
    public int targetHitInARow; //To track the targets hit
    [SerializeField] private int maxTargetsToHit = 10;
    [SerializeField] private Texture2D targetReticleTexture;
    [SerializeField] private SpawnerClass[] spawners;

    //General Variables    
    private bool IsPaused = false;  //Add a bool here for "IsPaused" - Will be used to track if the game is paused or not
    private bool IsBonusRActive = false;

    public bool BonusRoundBool
    {
        get { return IsBonusRActive; }
        set { IsBonusRActive = value; }
    }

    //14/4/26: The variables below were moved from the player input script to the game manager 
    private int MaxMisses = 5; //Max amount of possible clicks the player has before resulting in a game over
    private int MissCount = 0; //Variable that will track the player's misses 


    //Actions 
    public static event Action OnOutOfAmmo; //--Action: For displaying the pause UI when the player is out of ammo
     public static event Action OnMaxTargetsRowHit;
    public static event Action<Canvas> OnGameStart; //--Action: For disabling the pause UI on start
    public static event Action<Canvas> OnGamePause; //--Action: Enables the pause UI when the game is paused
    public static event Action<Canvas> OnGameResume; //--Action: Disables the pause UI when the game resumes
    public static event Action<Canvas> OnTimeOver; //--Action: Enable the timer over canvas when the player runs out of time
   

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

    void Awake() //Singleton pattern
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        PlayerInput = FindObjectOfType<PlayerInputHandler>();
        IsBonusRActive = false;
    }


    public void UpdateMouseCursor() //Call this method when the player is hovering over a target
    {
        Cursor.SetCursor(targetReticleTexture, Vector2.zero, CursorMode.Auto);
    }

    //General Methods 
    public void TimeOver()
    {
        Time.timeScale = 0; //Acts as if the game is "paused"

        //Disable the player's fire and reload input
        PlayerInput.PI.actions.FindAction("Fire").Disable();
        PlayerInput.PI.actions.FindAction("Reload").Disable();

        //Call method to display the "Pause menu". This will be used for testing - 15/6/26: This will now be changed to the game over screen

        // 1) Destroy any targets currently on screen
        foreach (var spawner in spawners)
        {
            spawner.DestroyTargets();
        }

        // 2) Display the game over panel here
        UIManager.Instance.GameOverCanvas.gameObject.SetActive(true);
        OnTimeOver?.Invoke(UIManager.Instance.GameOverCanvas);

        // 3) Disable the main game hud
        UIManager.Instance.DisableMenu(UIManager.Instance.GameHudCanvas);

        // 4) Update the "final score" field displayed on the game over panel
        UIManager.Instance.UpdateFinalScoreUI(ScoreManager.Instance.TotalScore);
    }

    //General Functions
    public void LoadScene(int buildIndex) //Function that will load the level
    {
        SceneManager.LoadSceneAsync(buildIndex);
        Debug.Log("Loading Scene");
    }

    public void QuitGame() //Will quit the game
    {
        Application.Quit();
        Debug.Log("Quitting!");
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
            UIManager.Instance.PauseMenuCanvas.gameObject.SetActive(true);
            OnGamePause?.Invoke(UIManager.Instance.PauseMenuCanvas);
        }
        else
        {
            IsPaused = false;
            Time.timeScale = 1;
            Debug.Log("Game Resumed!");
            //Invoke action here to hide the pause UI
            OnGameResume?.Invoke(UIManager.Instance.PauseMenuCanvas);
        }
    }

    public bool IsGamePaused()
    {
        return IsPaused;
    }

    private void PlayerMissShot() //Method responsible for the players' misses! 14/4/26: Moved fom the Player Input script to the Game manager
    {
        MissCount++;
        Debug.Log("Missed Counts: " + MissCount);

        //Call the player row decrement method
        PlayerHitRowDecrement();

        // if (MissCount >= MaxMisses)
        // {
        //     Debug.Log("Game Over");
        //     TimeOver();            
        // }
    }

    public void PlayerHitRowIncrement()
    {
        targetHitInARow++;

        if (targetHitInARow == maxTargetsToHit && !IsBonusRActive) //Additional check to prevent targets hit in the bonus round triggering another bonus round
        {
            Debug.Log("You hit: " + targetHitInARow + " In a row! Entering Bonus Round");
            OnMaxTargetsRowHit?.Invoke();
               
        }
    }

    //Method to track how many targets the player as hit in a row
    public void PlayerHitRowDecrement()
    {
        if (targetHitInARow > 0) //Check to see if the targets hit is greater than 0 before decrementing the value
        {
            targetHitInARow--; //This also prevents the value from going into the negatives
        }
    }
}
