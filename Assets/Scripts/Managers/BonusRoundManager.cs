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
        GameManager.Instance.BonusRoundBool = true;
        Debug.Log("Bonus Round has been activated!");

        //Action here
        OnBonusRoundStartTime?.Invoke();


        foreach (GameObject spawners in spawnerObjects) //Disables all the spawners
        {
            spawners.SetActive(false);
            Debug.Log("Spawners disabled");
        }

        foreach (var spawner in spawners) //Destroys any target(s) that are currently on screen before the bonus round intro animation plays
        {
            spawner.DestroyTargets();
        }

        // 1) Call the coroutine from the UI manager here
        StartCoroutine(UIManager.Instance.BonusRoundIntroScreen());
        PoolManager.Instance.objectsOnScreen = 1;
        
        
        // 2) Call the coroutine from the countdown manager here
        StartCoroutine(CountdownManager.Instance.CountdownTimer());

        // 3) Re-enable the spawners objects
        StartCoroutine(ReEnableSpawners());
        Debug.Log("Spawners re-enabled");

        // 4) Call the method that instantiates the targets from the spawners
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
            break;
        }
    }
}
