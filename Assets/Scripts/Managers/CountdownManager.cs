using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownManager : MonoBehaviour
{

    //This script will handle the 3,2,1 countdown timer that will take place prior to the bonus round! 
    //I decided to isolate this from the UI manager to prevent it from becoming too bloated

    //General variables
    [SerializeField] private int countdownTime;
    [SerializeField] private TextMeshProUGUI countdownText;



    void Start()
    {  
        countdownText.text = "READY?!";
        StartCoroutine(CountdownTimer());
    }
    //Use a coroutine to decrement the countdown timer

    private IEnumerator CountdownTimer()
    {
        //Check to see if the countdown timer is greater than 0 (use a while loop)

        while (countdownTime > 0)
        {
           

            //While the timer is greater than 0, assign the timer value to the text value
            countdownText.text = countdownTime.ToString();

            countdownTime--;

            //How long to wait (in seconds) before calling it again?

            yield return new WaitForSeconds(1f);

            countdownText.text = "GO!";
        }
        
        //How long to wait again (in seconds) before disabling the text gameObject?
        yield return new WaitForSeconds(2f);

        countdownText.gameObject.SetActive(false);
        
    }

}
