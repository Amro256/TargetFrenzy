using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownManager : MonoBehaviour
{

    //This script will handle the 3,2,1 countdown timer that will take place prior to the bonus round! 
    //Decided to isolate this from the UI manager to prevent it from becoming too bloated

    //Singleton
    public static CountdownManager Instance;

    //General variables
    [SerializeField] private int countdownTime;
    [SerializeField] private TextMeshProUGUI countdownText;


    void Awake()
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
        //StartCoroutine(CountdownTimer()); //This will be called somewhere else
    }


    //Use a coroutine to decrement the countdown timer
    
    public IEnumerator CountdownTimer()
    {
        //Check to see if the countdown timer is greater than 0 (use a while loop)

        while (countdownTime > 0)
        {

            //While the timer is greater than 0, assign the timer value to the text value
            countdownText.text = countdownTime.ToString();
            
            //Decrement
            countdownTime--;

            //How long to wait (in seconds) before calling it again?

            yield return new WaitForSeconds(1f);

            countdownText.text = "GO!";
        }

        //How long to wait again (in seconds) before disabling the text gameObject?
        yield return new WaitForSeconds(2f);

        //Disable the gameObject that the countdown text is attached to
        countdownText.gameObject.SetActive(false);

    }

}
