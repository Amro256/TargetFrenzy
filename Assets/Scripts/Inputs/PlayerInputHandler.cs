using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PlayerInputHandler : MonoBehaviour
{
    //Variables
    private PlayerInput pi; //Reference to the player Input component so specific actions can be disabled

    public static GameObject currentTarget; //To store the target the mouse is current hovering over


    //Actions to be invoked
    public static event Action OnPlayerMissUI;
    public static event Action OnPlayerReload;
    public static event Action OnPlayerMissedShot;

    private void Start()
    {
        pi = FindObjectOfType<PlayerInput>();
    }
    
    
    //Method for shooting / firing - using Unity Events as the notification behaviour 
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed) //Check if the action has been performed / completed
        {
            if (currentTarget != null) //If the mouse IS currently hovering over a target, destroy the current target
            {
                TargetClass Target = currentTarget.GetComponent<TargetClass>();

                if (Target != null) //Change the If statement to a switch statement (Due to the multiple targets)
                {
                    Target.OnHit();
                }

                Destroy(currentTarget);
            }
            else
            {
                Debug.Log("You have clicked on nothing");

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
            Debug.Log("Cant reload yet bozo");
            return;
        }
        
        //Code here - Invoke any actions here!
        OnPlayerReload?.Invoke();
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
}
