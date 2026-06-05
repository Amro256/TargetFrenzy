using UnityEngine;
using System;

public class BonusRoundManager : MonoBehaviour
{
    //General variables
    private int preBonusRoundCountdown; //A private int to give time for UI text to flash on screen before the bonus round starts

    //Actions
    public static event Action OnBonusRoundActive;

    public void ActivateBonusRound()
    {
        GameManager.Instance.BonusRoundBool = true;
        // Debug.Log("Bonus Round has been activated!");
        //Action here

        OnBonusRoundActive?.Invoke();

        GameManager.Instance.spawnerObjects.SetActive(false);
        Debug.Log("Spawners disabled for now");

        //Call the coroutine from the UI manager here
    }
}
