using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class StartUpSequence : MonoBehaviour //Reusing code from the countdown manager
{
    //Singleton
    public static StartUpSequence Instance { get; private set; }
    private PlayerInput pi;

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

        pi = FindObjectOfType<PlayerInput>();
    }


    public IEnumerator BeginStartUpSequence()
    {
        if (GameManager.Instance.IsIntroSeqPlaying) //Check with the bool first to see if the sequence has not been played
        {
            Debug.Log("Bool Value On Start: " + GameManager.Instance.IsIntroSeqPlaying); //Displays true in the console
            //Disable Player Input
            pi.enabled = false;
            yield return new WaitForSeconds(2f);

            StartUpText.text = "GO!";

            //How long to wait again (in seconds) before disabling the text gameObject?
            yield return new WaitForSeconds(2f);

            //Re-enable Player Input
            pi.enabled = true;
            //Disable the gameObject that the countdown text is attached to
            this.StartUpText.transform.parent.gameObject.SetActive(false);

            //Reset the value back to false to resume 
            GameManager.Instance.IsIntroSeqPlaying = false;
            Debug.Log("Bool Value After: " + GameManager.Instance.IsIntroSeqPlaying);
        }
        
    }
    
}
