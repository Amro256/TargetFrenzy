using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour  //This script's purpose is to isolate the ammo system and make it modular for future use!
{

    public static AmmoManager Instance;

    //General Variables
    [SerializeField] private int MaxAmmo = 4;
    [SerializeField] private int CurrentAmmoAmount;
    private bool IsOutOfAmmo;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        CurrentAmmoAmount = MaxAmmo; //Set the current Ammo amount to the Max Ammo when the game starts
        IsOutOfAmmo = false; //The player will have full ammo when they start the game
    }


    public void UpdateAmmoValue(int amount) //This method will be responsible for updating the Ammo Value
    {

        CurrentAmmoAmount -= amount; //Reduce the ammo value by one
        Debug.Log("Current Ammo: " + CurrentAmmoAmount);

        //If statement to check if the currentAmmo Amount is less than 0

        if (CurrentAmmoAmount < 4)
        {
            Debug.Log("Test! Reload! Reload! Reload!");
            IsOutOfAmmo = true;
            Reload();
        }
    }


    //Add a method for reload functionality. Reloading will be mapped the right mouse button
    public void Reload()
    {
        //First - Check if the player is out of ammo

        if (IsOutOfAmmo)
        {
            //This is where the code goes for handling reloading

            //The reload button will be mapped to the right mouse button, but first just add ammo back

            CurrentAmmoAmount = 4;
        }
    }

}
