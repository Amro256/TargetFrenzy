using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner1 : SpawnerClass //This class inherits from the Spawner Class (Read it like this: Spawner 1 IS A Spawner)
{

    //Variables & Lists
    private List<GameObject> spawnedTargets = new List<GameObject>(); //Tracks and stores the current instantiated targets

    void Start()
    {
        //Call the SpawnTargets method here
        SpawnTargets();
    }


    //Override the Instantiation code here
    public override void SpawnTargets()
    {
        StartCoroutine(InstantiateTargets());
    }

    IEnumerator InstantiateTargets() //IEnumerator responsible for instantiating and spawning targets
    {
        while (true) //Using a while loop so the spawning continues
        {
            foreach (GameObject prefabs in targetObjects)
            {
                GameObject instantiatedTargets = Instantiate(prefabs, transform.position, transform.rotation);
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

                spawnedTargets.Add(instantiatedTargets); //Add the instantiatedTargets to the spawned Targets list
                //Debug.Log("Waiting!");
                yield return new WaitForSeconds(SpawnTime); //Uses the SpawnTime float variable declared in the Parent Class
            }
        }
        
    }
}
