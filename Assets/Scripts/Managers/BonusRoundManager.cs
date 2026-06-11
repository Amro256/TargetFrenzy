using UnityEngine;
using System;
using System.Collections;

public class BonusRoundManager : MonoBehaviour
{
    //General variables
    [SerializeField] private GameObject[] spawnerObjects;
    private Spawner1 spawn01;

    //Actions
    public static event Action OnBonusRoundStartTime;


    void OnEnable()
    {
        ScoreManager.OnBonusRoundActivated += ActivateBonusRound;
    }


    void OnDisable()
    {
        ScoreManager.OnBonusRoundActivated -= ActivateBonusRound;
    }

    void Start()
    {
        spawn01 = FindObjectOfType<Spawner1>();
    }

    public void ActivateBonusRound()
    {
        GameManager.Instance.BonusRoundBool = true;
        Debug.Log("Bonus Round has been activated!");

        //Action here
        OnBonusRoundStartTime?.Invoke();

        //spawnerObjects.gameObject.SetActive(false); //This disables the spawners, so no targets will spawn during the bonus round intro screen.

        foreach (GameObject spawners in spawnerObjects)
        {
            spawners.SetActive(false);
        }


        Debug.Log("Spawners disabled for now");

        // 1) Call the coroutine from the UI manager here
        StartCoroutine(UIManager.Instance.BonusRoundIntroScreen());

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

        spawn01.SpawnTargets();
    }
}
