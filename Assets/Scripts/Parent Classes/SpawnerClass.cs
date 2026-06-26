using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerClass : MonoBehaviour //This is the base class that the spawner scripts will inherit from
{

    [Header("General Variables")]
    [SerializeField] protected float SpawnTime; //Variable to control the spawn rate of the targets
    private protected List<GameObject> activeTargets;  //Will be used to determine which targets to spawn 
    private List<GameObject> spawnedTargets = new List<GameObject>(); //Tracks and stores the current instantiated targets


    //-----------------------------------------------------------------------UNUSED---------------------------------------------------------------------------------------------------------
    // [Header("Lerp Positions")]  //Start and end points for lerping
    // [SerializeField] protected GameObject startPos;
    // [SerializeField] protected GameObject EndPos;

    [Header("Regular Target List")] //List of targets to Instantiate
    [SerializeField] protected List<GameObject> targetObjects = new List<GameObject>();

    [Header("Bonus Round Target List")]
    [SerializeField] protected List<GameObject> bonusTargetObjects = new List<GameObject>();

    [Header("Movement Points")]
    [SerializeField] protected Transform[] lerpPoints; //The spawner itself will hold the lerp points' transform, as this will allow me to drag and drop them into the inspector with no issue!
    //NOTE: This is also using a protected access modifier, so each of the spawner classes will be able to access this variable.


    //Method to Instantiate target game objects
    public virtual void SpawnTargets()
    {
        //Method will be overridden by derived classes
    }

    public void DestroyTargets() //Method that will handle destroying targets BEFORE the bonus round into plays
    {
        foreach (GameObject target in spawnedTargets)
        {
            if (spawnedTargets != null) //If the spawned targets ARE NOT empty then destroy the targets on screen
            {
                Destroy(target);
            }
        }
        
        spawnedTargets.Clear(); //Removes all spawned targets from the list
    }


    public IEnumerator InstantiateTargets() //IEnumerator responsible for instantiating and spawning targets
    {

        if (GameManager.Instance.BonusRoundBool) //if the bonus round is active, set the active targets to the bonus round targets (golden target)
        {
            activeTargets = bonusTargetObjects;
        }
        else
        {
            activeTargets = targetObjects; //else set the active targets to the regular gameplay targets
        }


        while (true) //Using a while loop so the spawning continues
        {
            foreach (GameObject prefabs in targetObjects)
            {
                GameObject instantiatedTargets = Instantiate(activeTargets[Random.Range(0, activeTargets.Count)], transform.position, transform.rotation); //28/5/26: Changed from "prefabs" to "targetObjects" so that the targets can be randomised on start
                //Debug.Log("Spawning targets!");

                TargetClass target = instantiatedTargets.GetComponent<TargetClass>(); //Grabs a reference to the Target (Parent) class and assigns the Instantiated targets that have the target class attached to it

                if (target != null) //Checks if the Instantiated targets have the target script attached to it, and if so, run the code below
                {
                    target.initialisePoints(lerpPoints); //Assigns the lerp points to the targets by calling the initialisePoints from the target class
                }
                else
                {
                    Debug.LogError("No Target script found on the Instantiated target!"); //Error handling 
                }

                spawnedTargets.Add(instantiatedTargets); //Add the instantiated Targets to the spawned Targets list
                //Debug.Log("Waiting!");
                yield return new WaitForSeconds(SpawnTime); //Uses the SpawnTime float variable declared in the Parent Class
            }
        }


    }
    
    
}
