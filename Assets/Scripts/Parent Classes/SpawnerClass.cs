using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerClass : MonoBehaviour //This is the base class that the spawner scripts will inherit from
{
    
    [Header("General Variables")]
    [SerializeField] protected float targetMoveSpeed; //Variable to control the target move speed, which will be exposed in the inspector

    //[SerializeField] private int spawnTime; //Will handle how often the targets will spawn --Will be implemented later--

   
    [Header("Lerp Positions")]  //Start and end points for lerping
    [SerializeField] protected GameObject startPos;
    [SerializeField] protected GameObject EndPos;

    
    [Header("Target Objects List")] //List of targets to Instantiate
    [SerializeField] protected List<GameObject> targetObjects = new List<GameObject>();


    //Method to Instantiate target game objects
    public virtual void SpawnTargets()
    {
        //Method will be overridden by derived classes
    }


    //Method to handle lerping the targets between two points
    public virtual void TargetLerping()
    {
        //Method will be overridden by derived spawner classes
    }
}
