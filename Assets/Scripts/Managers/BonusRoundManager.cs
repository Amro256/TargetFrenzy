using UnityEngine;
using System;

public class BonusRoundManager : MonoBehaviour
{
    //General variables
    private int preBonusRoundCountdown; //A private int to give time for UI text to flash on screen before the bonus round starts
[SerializeField] private GameObject spawnerObjects;

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

    public void ActivateBonusRound()
    {
        GameManager.Instance.BonusRoundBool = true;
        Debug.Log("Bonus Round has been activated!");

        //Action here
        OnBonusRoundStartTime?.Invoke();

        spawnerObjects.SetActive(false); //This disables the spawners, so no targets will spawn during the bonus round intro screen.
        Debug.Log("Spawners disabled for now");

        // 1) Call the coroutine from the UI manager here
        StartCoroutine(UIManager.Instance.BonusRoundIntroScreen());

        // 2) Call the coroutine from the countdown manager here
        StartCoroutine(CountdownManager.Instance.CountdownTimer());



        // 3) Re-enable the spanwer objects
        spawnerObjects.SetActive(true);
        Debug.Log("Spawners re-enabled");

    }
}
