using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AmmoManager : MonoBehaviour  //This script's purpose is to isolate the ammo system and make it modular for future use!
{

    public static AmmoManager Instance { get; private set; }

    //General Variables
    [SerializeField] private int maxAmmo = 4;
    [SerializeField] public int CurrentAmmoAmount;
    private bool IsOutOfAmmo; //The player will have full ammo when they start the game

    #region actions
    public static event Action OnReloadSprites; // For the UI Manager 
    public static event Action OnFullAmmo; //For the Player Input UI;
    public static event Action OnOutOfAmmo; //For the Player Input UI;
    #endregion

    #region Properties
    //Properties
    public int MaxAmmo
    {
        get { return maxAmmo; }
    }

    public bool IsAmmoEmpty()
    {
        return IsOutOfAmmo;
    }
    #endregion

    #region singleton
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
    #endregion

    private void OnEnable()
    {
        PlayerInputHandler.OnReloadPress += Reload;
    }

    private void OnDisable()
    {
        PlayerInputHandler.OnReloadPress -= Reload;
    }

    void Start()
    {
        CurrentAmmoAmount = MaxAmmo; //Set the current Ammo amount to the Max Ammo when the game starts 
    }

    public void UpdateAmmoValue(int amount) //This method will be responsible for updating the Ammo Value
    {
        CurrentAmmoAmount -= amount; //Reduce the ammo value by one

        //If statement to check if the currentAmmo Amount is less than 0

        if (CurrentAmmoAmount <= 0)
        {
            //Disable the Player's fire input
            OnOutOfAmmo?.Invoke();
            Debug.LogError("Please Reload!");
            IsOutOfAmmo = true;

            UIManager.Instance.ShowReloadWarning();

            //Play animation here
            AnimationManager.Instance.StartAnimation("IsLowOnAmmo");
        }
    }


    //Add a method for reload functionality. Reloading will be mapped the "R" key
    public void Reload()
    {
        //Set the "isOutOfAmmo" bool back to false as the player will have full ammo after reloading
        IsOutOfAmmo = false;
        Debug.Log("Is player out of ammo: " + IsOutOfAmmo);
           
        //Re-Enable the Player's firing input
        OnFullAmmo.Invoke();

        CurrentAmmoAmount = MaxAmmo; //Set the current ammo back to the max ammo
        OnReloadSprites?.Invoke(); //Reload the ammo sprites

        //Disable the Reload Warning gameObject
        UIManager.Instance.HideReloadWarning();
    }
    
    public void AmmoOnBonusRoundStart()
    {
        IsOutOfAmmo = false; //We need to check if the player is out of ammo first, otherwise the reload can still be performed with max ammo
        CurrentAmmoAmount = maxAmmo;
    }

    

}
