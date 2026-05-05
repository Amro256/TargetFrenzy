using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TargetClass : MonoBehaviour //Parent class that all the target scripts will inherit from
{
    [Header("References")]
    protected Transform[] lerp_Points; //Array to store the lerpPoints' transform (the lerp points are empty game Objects) - ACCESS MODIFIER: Protected (This will give the derived class access to this variable)
    private Vector3 initialSpawnPoint; //Private vector 3 storing the initial spawn position of the target from the spawner
    private int currentPointIndex = 0; //Variable that will be used to store the current point the target is moving to

    //General Variables
    [Header("Movement Speed")]
    [SerializeField] private float moveSpeed; //To control the speed of the targets

    void Start()
    {
        //Grab a reference to the targets' initial spawn position on start 
        initialSpawnPoint = transform.position;

        Debug.Log(gameObject.transform.position); //Used for debugging to check the targets' position

        moveSpeed = Random.Range(10, 17);
    }

    void Update()
    {
        if (currentPointIndex < lerp_Points.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, lerp_Points[currentPointIndex].position, Time.deltaTime * moveSpeed); //This current moves the target to point 1

            if (Vector3.Distance(transform.position, lerp_Points[currentPointIndex].position) < 0.5f)
            {
                currentPointIndex = Random.Range(0, lerp_Points.Length);
            }
        }
    }

    public void initialisePoints(Transform[] points) //As gameObjects can not be assigned to a prefab in the inspector, I will need to assign the lerp points to the targets during runtime
    { //So this method is used to Initialise the lerp points by taking in an array of the lerp points' transform

        lerp_Points = points; //Assigning the lerp points declared above to the points taken in by this method

        if (currentPointIndex < lerp_Points.Length)
        {
            currentPointIndex = Random.Range(0, lerp_Points.Length);
        }
    }

    public virtual void OnHit() //Child classes will override this method
    {
        
    } 
    
}
