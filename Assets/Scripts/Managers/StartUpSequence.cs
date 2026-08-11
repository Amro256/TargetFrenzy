using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class StartUpSequence : MonoBehaviour //Reusing code from the countdown manager
{
    //Singleton
    public static StartUpSequence Instance { get; private set; }

    //Variables

    [SerializeField] private TextMeshProUGUI StartUpText;

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
    }

    public IEnumerator BeginStartUpSequence()
    {
        if (GameManager.Instance.IsIntroSeqPlaying != true) //Check with the bool first to see if the sequence has not been played
        {

            yield return new WaitForSeconds(2f);

            StartUpText.text = "GO!";

            //How long to wait again (in seconds) before disabling the text gameObject?
            yield return new WaitForSeconds(2f);

            //Disable the gameObject that the countdown text is attached to
            this.StartUpText.transform.parent.gameObject.SetActive(false);
        }


    }
    
}
