using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

public class BonusRoundManager : MonoBehaviour
{
    //General variables
    [SerializeField ] private SpawnerClass[] spawners;
    [SerializeField] private AudioData audioSFX;


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
        GameManager.Instance.BonusRoundIntro = true;
        UIManager.Instance.HideTargetCountGroup();

        ResetHudElements();
            
        //Action here
        OnBonusRoundStartTime?.Invoke();

        foreach (SpawnerClass spawners in spawners) //Disables all the spawners
        {
            spawners.gameObject.SetActive(false);
            Debug.Log("Spawners disabled");
        }

        PoolManager.Instance.DestroyAllTargets();

        // 1) Call the coroutine from the UI manager here
        StartCoroutine(UIManager.Instance.BonusRoundIntroScreen());
        AudioManager.Instance.PlayUISFX(audioSFX.Clips[8], 1f);


        // 2) Call the coroutine from the countdown manager here
        StartCoroutine(CountdownManager.Instance.CountdownTimer());

        // 3) Re-enable the spawners objects
        StartCoroutine(ReEnableSpawners());
        Debug.Log("Spawners re-enabled");
        }
        
    }
    
    private void ResetHudElements()
    {
        MultiplierBarManager.Instance.ResetMultiBar();

        AmmoManager.Instance.AmmoOnBonusRoundStart(); //The player will be given max ammo when the round starts -- 16/6/26: Moved from the UI manager to here -- 
        // 10/8/26: Moved further up to prevent the reload warning animation from playing during the into sequence
        Debug.Log("Bonus Round Max Ammo: " + AmmoManager.Instance.MaxAmmo);
        UIManager.Instance.HideReloadWarning();

        GameManager.Instance.targetHitInARow = 0; //Reset the counter + the UI
        UIManager.Instance.UpdateTargetCounterUI(GameManager.Instance.targetHitInARow);
    }

    private IEnumerator ReEnableSpawners() //This has to be an Ienumerator because by placed a second foreach loop in the method above, the second loop would overwrite the first. 
    {
        yield return new WaitForSeconds(7f);

        foreach (SpawnerClass spawner in spawners)
        {
            spawner.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(2f);

        GameManager.Instance.BonusRoundIntro = false;

    }
}
