using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler instance { get; private set; }

    //References
    private TargetFrenzy inputs; //20/8/26: Replaced the reference to the player input component with the C# class for better consistency
    [SerializeField] private AudioData audioSFX;
    MouseHandler PlayerMH; //Reference to the MouseHandler script, so this script can access the current target (GameObject)

    //Graphic Raycaster 
    [SerializeField] private GraphicRaycaster[] graphicRaycasters; //Array containing all the GraphicRaycasters in the scene
    private PointerEventData touchData; //Struct to hold the position data of the pointer (in this case, the touch position)
    private List<RaycastResult> raycastResults; //List containing all the UI elements hit by the raycast

    #region Actions
    //Actions to be invoked
    public static event Action OnPlayerMissUI;
    public static event Action OnReloadPress;
    public static event Action OnPlayerMissedShot;
    public static event Action OnConfirmedHit;
    #endregion

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        inputs = new TargetFrenzy();

    }

    void OnEnable()
    {
        //Two different actions maps
        inputs.Touch.Enable();

        //Subscribe the actions to the "performed" binding
        inputs.Touch.TouchPressed.performed += OnTouchPressed;

        AmmoManager.OnOutOfAmmo += DisableFiringFunctionality;
        AmmoManager.OnFullAmmo += EnableFiringFunctionality;

        UIManager.OnInputsEnable += EnablePlayerAndPauseActions;
        UIManager.OnInputsDisable += DisablePlayerAndPauseActions;
    }

    void OnDisable()
    {
        //Two different actions maps
        inputs.Touch.Disable();

        //unsubscribe the actions from the "performed" binding
        inputs.Touch.TouchPressed.performed -= OnTouchPressed;

        AmmoManager.OnOutOfAmmo -= DisableFiringFunctionality;
        AmmoManager.OnFullAmmo -= EnableFiringFunctionality;

        UIManager.OnInputsEnable -= EnablePlayerAndPauseActions;
        UIManager.OnInputsDisable -= DisablePlayerAndPauseActions;
    }

    private void Start()
    {
        PlayerMH = FindAnyObjectByType<MouseHandler>(); //Finds an object that has the mouse handler script attached to it

        //Initialisation
        touchData = new PointerEventData(EventSystem.current);
        raycastResults = new List<RaycastResult>();
    }

    #region Methods for Input Actions
    //Method for shooting / firing - using Unity Events as the notification behaviour 
    private void OnTouchPressed(InputAction.CallbackContext context)
    {
        if (!context.performed) return; //Check if the action has been performed / completed  

        if (HasTouchedOverUI()) return; //Check if the player did not touch a UI element

        if (PlayerMH.CurrentTarget != null) //If the mouse IS currently hovering over a target, destroy the current target
        {
            TargetClass Target = PlayerMH.CurrentTarget.GetComponent<TargetClass>();
            GameObject currentTarget = PlayerMH.CurrentTarget;

            //Screen Shake 
            StartCoroutine(CameraShake.Instance.BeginScreenShake(0.45f, 0.25f));
            AudioManager.Instance.PlaySFX(audioSFX.Clips[3], 1f);

            if (Target != null) //Change the If statement to a switch statement (Due to the multiple targets)
            {
                StartCoroutine(ReturnTargetToPoolAfterAnimation(Target, currentTarget));
            }

            //PoolManager.Instance.ReturnPooledObject(PlayerMH.CurrentTarget);
        }
        else
        {
            //Debug.Log("You have clicked on nothing"); --18/5/26 Commented this debug out to debug other bugs--

            AmmoManager.Instance.UpdateAmmoValue(1);  //As of: 5/5/26 - This has been moved from the UI manager as it was overlapping with the same code 
            //that's responsible for updating the ammo value when the player hits a target, resulting in the ammo value decreasing by two instead of one.

            AudioManager.Instance.PlaySFX(audioSFX.Clips[2], 1f);
            //Invoke Action
            OnPlayerMissUI?.Invoke();
            OnPlayerMissedShot?.Invoke();
        }
    }

    private IEnumerator ReturnTargetToPoolAfterAnimation(TargetClass target, GameObject currentTarget)
    {
        target.OnHit();

        //Invoke Action Here
        OnConfirmedHit?.Invoke(); //This action will consume ammo if the player hits a target and the UI will update accordingly

        yield return new WaitForSeconds(35f / 60f); //Wait for the animation to finish playing before return a target to the pool
        PoolManager.Instance.ReturnPooledObject(currentTarget);
    }

    // private void OnReload(InputAction.CallbackContext context) //Reload is mapped to the "R" key as of now
    // {
    //     //For handling reloading
    //     if (!context.performed) return; //Checks if the R key was NOT PRESSED (performed)

    //     if (!AmmoManager.Instance.IsAmmoEmpty())
    //     {
    //         Debug.Log("You cant reload yet!");
    //         return;
    //     }

    //     //Code here - Invoke any actions here!
    //     OnReloadPress?.Invoke();
    //     Debug.Log("Reload performed");
    // }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        GameManager.Instance.PauseCheck();
    }
    #endregion

    #region General Input Related Methods
    //Method to handle enabling and disabling inputs / actions
    public Vector2 ReadTouchPosition()
    {
        return inputs.Touch.ScreenPosition.ReadValue<Vector2>();
    }

    //Method to check whether the player touched a UI element
     private bool HasTouchedOverUI()
    {
        //Get the current touch position
        touchData.position = ReadTouchPosition();

        //Clear the list of previous results
        raycastResults.Clear();

        //Loop through the graphicRaycasters array and perform a raycast from the current touch position. 
        //Store the results in the raycastResults list
        foreach (var graphicRaycaster in graphicRaycasters)
        {
            graphicRaycaster.Raycast(touchData, raycastResults);
        }

        //Debug: Log UI elements hit by the raycast
        foreach (var raycastResult in raycastResults)
        {
            Debug.Log("Hit " + raycastResult.gameObject.name);
        }

        //Return a bool that will tell whether the player has touched a UI element
        return raycastResults.Count > 0;
    }

    public void EnableTouchActions()
    {
        inputs.Touch.Enable();
    }

    public void DisableTouchActions()
    {
        inputs.Touch.Disable();
    }

    public void EnablePlayerAndPauseActions()
    {
        inputs.Touch.Enable();
        //inputs.Pause.Enable();
    }

    public void DisablePlayerAndPauseActions()
    {
        inputs.Touch.Disable();
        //inputs.Pause.Disable();
    }


    //  public void EnablePauseAction()
    // {
    //     inputs.Pause.Enable();
    // }

    // public void DisablePauseAction()
    // {
    //     inputs.Pause.Disable();
    // }

    //Method to handle ONLY enabling and disabling the fire input / action
    void EnableFiringFunctionality()
    {
        inputs.Touch.ScreenPosition.Enable(); //Remember to chance the method name later
    }

    void DisableFiringFunctionality()
    {
        inputs.Touch.ScreenPosition.Disable(); //Remember to chance the method name later

    }

   

    #endregion

}
