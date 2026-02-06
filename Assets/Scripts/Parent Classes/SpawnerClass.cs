using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerClass : MonoBehaviour //This is the base class that the spawner scripts will inherit from
{

    [Header("General Variables")]
    [SerializeField] protected float SpawnTime; //Variable to control the spawn rate of the targets


    //-----------------------------------------------------------------------UNUSED---------------------------------------------------------------------------------------------------------
    // [Header("Lerp Positions")]  //Start and end points for lerping
    // [SerializeField] protected GameObject startPos;
    // [SerializeField] protected GameObject EndPos;


    [Header("Target Objects List")] //List of targets to Instantiate
    [SerializeField] protected List<GameObject> targetObjects = new List<GameObject>();

    [Header("Movement Points")]
    [SerializeField] protected Transform[] lerpPoints; //The spawner itself will hold the lerp points' transform, as this will allow me to drag and drop them into the inspector with no issue!
    //NOTE: This is also using a protected access modifier, so each for the spawner classes will be able to access this variable.


    //Method to Instantiate target game objects
    public virtual void SpawnTargets()
    {
        //Method will be overridden by derived classes

    }
    

    //-- Disregard this method! Target Lerping will be done by the targets itself and not the spawner!! ---//

    //Method to handle lerping the targets between two points 

    // public virtual void TargetLerping()
    // {
    //     //Method will be overridden by derived spawner classes
    // }

    // protected IEnumerator SpawnRate() -- Used for inital Debugging
    // {
    //     while (true)
    //     {
    //        Debug.Log("Test)");
    //        yield return new WaitForSeconds(SpawnTime); 
    //     }

    // }
}
