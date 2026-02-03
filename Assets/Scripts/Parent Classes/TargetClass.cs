using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Callbacks;
using UnityEngine;

public class TargetClass : MonoBehaviour //Parent class that all the target scripts will inherit from
{
    //General Variables
    [Header("General Variables")]
    [SerializeField] private float moveSpeed; //To control the speed of the targets


    //Variables for score and target effects
    [Header("Target Gameplay Effects")]
    [SerializeField] protected int score;
    [SerializeField] private int scoreMultiplier;
    [SerializeField] private int timePenalty;

    private Rigidbody2D rb; //Reference to target's rigidbody component
    private Vector3 initialSpawnPoint; //Private vector 3 that will be used to store the initial spawn position of the targets

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Gets the Rigidbody2D component of the target

        //Grab a reference to the targets' initial spawn position on start 
        initialSpawnPoint = transform.position;
        Debug.Log(gameObject.transform.position); //Used for debugging to check the targets' position


        rb.velocity = test(-0.5f, -2f).normalized * moveSpeed; //Normalising this ensures consistent movement!
        
    }

    //Add a method to shoot the targets in a random direction when they spawn in
    // public void ShootTargetsInRanDir()
    // {

    // }   
    
    private Vector2 test(float min, float max)
        {
            var x = Random.Range(min, max);
            var y = Random.Range(min, max);
            return new Vector2(x, y);
        }
}
