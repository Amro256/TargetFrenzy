using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PlayerInputHandler : MonoBehaviour
{
    //References
    private PlayerInput pi; //Reference to the player Input component so specific actions can be disabled
    MouseHandler PlayerMH; //Reference to the MouseHandler script, so this script can access the current target (GameObject)

    //Actions to be invoked
    public static event Action OnPlayerMissUI;
    public static event Action OnPlayerReloadInputPress;
    public static event Action OnPlayerMissedShot;

    void OnEnable()
    {
        AmmoManager.OnPlayerOutOfAmmo += DisableFiringFunctionality;
        AmmoManager.OnPlayerFullAmmo += EnableFiringFunctionality;
    }

    void OnDisable()
    {
        AmmoManager.OnPlayerOutOfAmmo -= DisableFiringFunctionality;
        AmmoManager.OnPlayerFullAmmo -= EnableFiringFunctionality;
    }

    private void Start()
    {
        pi = FindObjectOfType<PlayerInput>();
        PlayerMH = FindObjectOfType<MouseHandler>(); //Finds an object that has the mouse handler script attached to it
    }


    //Method for shooting / firing - using Unity Events as the notification behaviour 
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed) //Check if the action has been performed / completed
        {
            if (PlayerMH.CurrentTarget != null) //If the mouse IS currently hovering over a target, destroy the current target
            {
                TargetClass Target = PlayerMH.CurrentTarget.GetComponent<TargetClass>();

                if (Target != null) //Change the If statement to a switch statement (Due to the multiple targets)
                {
                    Target.OnHit();
                }

                Destroy(PlayerMH.CurrentTarget);
            }
            else
            {
                Debug.Log("You have clicked on nothing");

                AmmoManager.Instance.UpdateAmmoValue(1);  //As of: 5/5/26 - This has been moved from the UI manager as it was overlapping with the same code 
                 //that's responsible for updating the ammo value when the player hits a target, resulting in the ammo value decreasing by two instead of one.
                
                //Invoke Action
                OnPlayerMissUI?.Invoke();
                OnPlayerMissedShot?.Invoke();

            }
        }
    }

    public void OnReload(InputAction.CallbackContext context) //Reload is mapped to the "R" key as of now
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

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        GameManager.Instance.PauseGame();

        if (GameManager.Instance.IsGamePaused()) //This script will need to know the status of the game, whether its paused or not, to disable/enable the other actions
        {
            Debug.Log("Actions disabled");

            pi.actions.FindAction("Look").Disable();
            pi.actions.FindAction("Fire").Disable();
            pi.actions.FindAction("Reload").Disable();
            return;
        }
        else
        {
            Debug.Log("Actions enabled");

            pi.actions.FindAction("Look").Enable();
            pi.actions.FindAction("Fire").Enable();
            pi.actions.FindAction("Reload").Enable();
            return;
        }
    }

    void DisableFiringFunctionality()
    {
        pi.actions.FindAction("Fire").Disable();
        return;
        //Invoke action here
    }

    void EnableFiringFunctionality()
    {
        pi.actions.FindAction("Fire").Enable();
        return;
        //Invoke action here
    }
}
