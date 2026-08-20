using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler instance;
    //References
    private TargetFrenzy inputs; //20/8/26: Replaced the reference to the player input component with the C# class for better consistency
    MouseHandler PlayerMH; //Reference to the MouseHandler script, so this script can access the current target (GameObject)

    //Actions to be invoked
    public static event Action OnPlayerMissUI;
    public static event Action OnPlayerReloadInputPress;
    public static event Action OnPlayerMissedShot;
    public static event Action OnConfirmedHit;

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
        inputs.Player.Enable();
        inputs.Pause.Enable(); //Only has the pause functionality

        //Subscribe the actions to the "performed" binding
        inputs.Player.Fire.performed += OnFire;
        inputs.Player.Reload.performed += OnReload;
        inputs.Pause.Pause.performed += OnPause;

        AmmoManager.OnPlayerOutOfAmmo += DisableFiringFunctionality;
        AmmoManager.OnPlayerFullAmmo += EnableFiringFunctionality;
    }

    void OnDisable()
    {
        //Two different actions maps
        inputs.Player.Disable();
        inputs.Pause.Disable(); //Only has the pause functionality

        //unsubscribe the actions from the "performed" binding
        inputs.Player.Fire.performed -= OnFire;
        inputs.Player.Reload.performed -= OnReload;
        inputs.Pause.Pause.performed -= OnPause;  
        
        AmmoManager.OnPlayerOutOfAmmo -= DisableFiringFunctionality;
        AmmoManager.OnPlayerFullAmmo -= EnableFiringFunctionality;
    }

    private void Start()
    {
        PlayerMH = FindObjectOfType<MouseHandler>(); //Finds an object that has the mouse handler script attached to it
    }


    //Method for shooting / firing - using Unity Events as the notification behaviour 
    private void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed) //Check if the action has been performed / completed
        {
            if (PlayerMH.CurrentTarget != null) //If the mouse IS currently hovering over a target, destroy the current target
            {
                TargetClass Target = PlayerMH.CurrentTarget.GetComponent<TargetClass>();
                
                //Screen Shake 
                StartCoroutine(CameraShake.Instance.BeginScreenShake(0.35f, 0.15f));

                if (Target != null) //Change the If statement to a switch statement (Due to the multiple targets)
                {
                    Target.OnHit();
                    //Invoke Action Here
                    OnConfirmedHit?.Invoke(); //This action will consume ammo if the player hits a target and the UI will update accordingly

                }

                //Destroy(PlayerMH.CurrentTarget);
                PoolManager.Instance.ReturnPooledObject(PlayerMH.CurrentTarget);
            }
            else
            {
                //Debug.Log("You have clicked on nothing"); --18/5/26 Commented this debug out to debug other bugs--

                AmmoManager.Instance.UpdateAmmoValue(1);  //As of: 5/5/26 - This has been moved from the UI manager as it was overlapping with the same code 
                //that's responsible for updating the ammo value when the player hits a target, resulting in the ammo value decreasing by two instead of one.

                //Invoke Action
                OnPlayerMissUI?.Invoke();
                OnPlayerMissedShot?.Invoke();

            }
        }
    }

    private void OnReload(InputAction.CallbackContext context) //Reload is mapped to the "R" key as of now
    {
        //For handling reloading
        if (!context.performed) return; //Checks if the R key was NOT PRESSED (performed)


        if (!AmmoManager.Instance.IsAmmoEmpty())
        {
            Debug.Log("You cant reload yet!");
            return;
        }

        //Code here - Invoke any actions here!
        OnPlayerReloadInputPress?.Invoke();
        Debug.Log("Reload performed");
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        GameManager.Instance.PauseGame();

        if (GameManager.Instance.IsGamePaused()) //This script will need to know the status of the game, whether its paused or not, to disable/enable the other actions
        {
            Debug.Log("Actions disabled");
            DisableAllPlayerActions();
            return;
        }
        else
        {
            Debug.Log("Actions enabled");
            EnableAllPlayerActions();
            return;
        }
    }

    //Method to handle enabling and disabling inputs / actions

    public Vector2 ReadMouseValue()
    {
        return inputs.Player.Look.ReadValue<Vector2>();
    }

    public void EnableAllPlayerActions()
    {
        inputs.Player.Enable();
    }

    public void DisableAllPlayerActions()
    {
        inputs.Player.Disable();
    }

     public void EnablePauseAction()
    {
        inputs.Pause.Enable();
    }

    public void DisablePauseAction()
    {
        inputs.Pause.Disable();
    }
    
    //Method to handle ONLY enabling and disabling the fire input / action
    void EnableFiringFunctionality()
    {   
        inputs.Player.Fire.Enable();
    }

    void DisableFiringFunctionality()
    {
        inputs.Player.Fire.Disable();
    }

    
}
