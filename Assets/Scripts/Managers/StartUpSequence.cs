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

    void Start()
    {
        StartCoroutine(BeginStartUpSequence());
    }


    public IEnumerator BeginStartUpSequence()
    {
        if (GameManager.Instance.IsIntroSeqPlaying) //Check with the bool first to see if the sequence has not been played
        {
            //Disable Player Input
            PlayerInputHandler.instance.DisableAllPlayerActions();
            PlayerInputHandler.instance.DisablePauseAction();

            yield return new WaitForSeconds(2f);

            StartUpText.text = "GO!";

            //How long to wait again (in seconds) before disabling the text gameObject?
            yield return new WaitForSeconds(2f);

            //Re-enable Player Input
            PlayerInputHandler.instance.EnableAllPlayerActions();
            PlayerInputHandler.instance.EnablePauseAction();

            //Disable the gameObject that the countdown text is attached to
            this.StartUpText.transform.parent.gameObject.SetActive(false);

            //Reset the value back to false to resume 
            GameManager.Instance.IsIntroSeqPlaying = false;
        }

    }
    
}
