using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

public class BonusRoundManager : MonoBehaviour
{
    //General variables
    [SerializeField] private GameObject[] spawnerObjects;
    [SerializeField ] private SpawnerClass[] spawners;

    //Actions
    public static event Action OnBonusRoundStartTime;


    void OnEnable()
    {
        ScoreManager.OnBonusRoundActivated += ActivateBonusRound;
        GameManager.OnMaxTargetsRowHit += ActivateBonusRound;
    }


    void OnDisable()
    {
        ScoreManager.OnBonusRoundActivated -= ActivateBonusRound;
        GameManager.OnMaxTargetsRowHit -= ActivateBonusRound;
    }

    public void ActivateBonusRound()
    {
        //Put a check in place to check if the bonus round is not currently active, AND if the multiplier is currently active
        if (!GameManager.Instance.BonusRoundBool)
       {
            GameManager.Instance.BonusRoundBool = true;
            MultiplierBarManager.Instance.ResetMultiBar();

            AmmoManager.Instance.AmmoOnBonusRoundStart(); //The player will be given max ammo when the round starts -- 16/6/26: Moved from the UI manager to here -- 
            // 10/8/26: Moved further up to prevent the reload warning animation from playing during the into sequence
            Debug.Log("Bonus Round Max Ammo: " + AmmoManager.Instance.MaxAmmo);


            GameManager.Instance.targetHitInARow = 0; //Reset the counter + the UI
            UIManager.Instance.UpdateTargetCounterUI(GameManager.Instance.targetHitInARow);
            

        //Action here
        OnBonusRoundStartTime?.Invoke();

        foreach (GameObject spawners in spawnerObjects) //Disables all the spawners
        {
            spawners.SetActive(false);
            Debug.Log("Spawners disabled");
        }

        foreach (var spawner in spawners) //Return any targets to the pool before the round start
        {
            spawner.DestroyTargets();
        }

        // 1) Call the coroutine from the UI manager here
        StartCoroutine(UIManager.Instance.BonusRoundIntroScreen());
        PoolManager.Instance.objectsOnScreen = 1; //Without this, the object on screen value will display -2 in the inspector


        // 2) Call the coroutine from the countdown manager here
        StartCoroutine(CountdownManager.Instance.CountdownTimer());

        // 3) Re-enable the spawners objects
        StartCoroutine(ReEnableSpawners());
        Debug.Log("Spawners re-enabled");

        // 4) Call the method that instantiates the targets from the spawners
        }
        
    }

    private IEnumerator ReEnableSpawners() //This has to be an Ienumerator because by placed a second foreach loop in the method above, the second loop would overwrite the first. 
    {
        yield return new WaitForSeconds(7f);

        foreach (GameObject spawners in spawnerObjects)
        {
            spawners.SetActive(true);
        }

        yield return new WaitForSeconds(2f);

        foreach (var spawner in spawners)
        {
            //Call the spawn target script
            spawner.SpawnTargets();
            Debug.Log("Spawning: " + spawner);
        }
    }
}
