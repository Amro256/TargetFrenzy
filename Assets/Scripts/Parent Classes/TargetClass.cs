using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Callbacks;
using UnityEngine;

public class TargetClass : MonoBehaviour //Parent class that all the target scripts will inherit from
{
    [Header("References")]
    protected Transform[] lerp_Points; //Array to store the lerpPoints' transform (the lerp points are a empty gameObjects) - ACCESS MODIFIER: Protected (This will give the derived class access to this variable)
    private Rigidbody2D rb; //Reference to target's rigidbody component
    private Vector3 initialSpawnPoint; //Private vector 3 storing the initial spawn position of the target from the spawner
    private int currentPointIndex = 0; //Variable that will be used to store the current point the target is moving to

    //General Variables
    [Header("General Variables")]
    [SerializeField] private float moveSpeed; //To control the speed of the targets

    //Variables for general gameplay effects
    [Header("Target Gameplay Effects")]
    [SerializeField] protected int score;
    [SerializeField] private int scoreMultiplier;
    [SerializeField] private int timePenalty;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Gets the Rigidbody2D component of the target

        //Grab a reference to the targets' initial spawn position on start 
        initialSpawnPoint = transform.position;
        Debug.Log(gameObject.transform.position); //Used for debugging to check the targets' position
        //rb.velocity = test(-0.5f, -2f).normalized * moveSpeed; //Normalising this ensures consistent movement!
    }

    void Update()
    {
        if (currentPointIndex < lerp_Points.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, lerp_Points[currentPointIndex].position, Time.deltaTime * moveSpeed); //This current moves the target to point 1
        }
    }

    public void initialisePoints(Transform[] points) //As gameObjects can not be assigned to a prefab in the inspector, I will need to assign the lerp points to the targets during runtime
    { //So this method is used to Initialise the lerp points by taking in an array of lerp points' transform

        lerp_Points = points; //Assigning the lerp points declared above to the points taken in by this method
    }
    
}
