using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AmmoManager : MonoBehaviour  //This script's purpose is to isolate the ammo system and make it modular for future use!
{

    public static AmmoManager Instance;

    //General Variables
    [SerializeField] private int maxAmmo = 4;
    [SerializeField] public int CurrentAmmoAmount;
    private bool IsOutOfAmmo;

    public static event Action OnPlayerReloadUI; // For the UI Manager 
    public static event Action OnPlayerFullAmmo; //For the Player Input UI;
    public static event Action OnPlayerOutOfAmmo; //For the Player Input UI;

    //property
    public int MaxAmmo
    {
        get { return maxAmmo; }
     }

    private void OnEnable()
    {
        PlayerInputHandler.OnPlayerReloadInputPress += Reload;
    }

    private void OnDisable()
    {
        PlayerInputHandler.OnPlayerReloadInputPress -= Reload;
    }

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
        CurrentAmmoAmount = maxAmmo; //Set the current Ammo amount to the Max Ammo when the game starts
        IsOutOfAmmo = false; //The player will have full ammo when they start the game
    }


    public void UpdateAmmoValue(int amount) //This method will be responsible for updating the Ammo Value
    {

        CurrentAmmoAmount -= amount; //Reduce the ammo value by one

        //Debug.Log("Current Ammo: " + CurrentAmmoAmount); --18/5/26 Commented this debug out to debug other bugs--

        //If statement to check if the currentAmmo Amount is less than 0

        if (CurrentAmmoAmount <= 0)
        {
            //Disable the Player's fire input
            OnPlayerOutOfAmmo?.Invoke();
            Debug.LogError("Please Reload!");
            IsOutOfAmmo = true;
        }

    }


    //Add a method for reload functionality. Reloading will be mapped the right mouse button
    public void Reload()
    {
        //This is where the code goes for handling reloading

        //First - Check if the player is NOT out of ammo
        if (!IsOutOfAmmo)
        {
            return;
        }
        //The reload button will be mapped to the right mouse button, but first just add ammo back

        //Re-Enable the Player's firing input
        OnPlayerFullAmmo.Invoke();

        CurrentAmmoAmount = maxAmmo; //Set the current ammo back to the max ammo
        OnPlayerReloadUI?.Invoke(); //-- Not working as the function does not get called
        IsOutOfAmmo = false;

 
        //Debug.Log("Ammo After reload: " + CurrentAmmoAmount); --18/5/26 Commented this debug out to debug other bugs--
    }

    public bool IsAmmoEmpty()
    {
        return IsOutOfAmmo;
    }

}
