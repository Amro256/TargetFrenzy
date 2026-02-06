using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : SpawnerClass //This class inherits from the Spawner Class (Read it like this: Spawner 1 IS A Spawner)
{

    //Variables & Lists
    private List<GameObject> spawnedTargets = new List<GameObject>(); //Tracks and stores the current instantiated targets


    void Start()
    {
        SpawnTargets(); //Call the SpawnTargets method here
    }


    //Override the Instantiation code here
    public override void SpawnTargets()
    {
        StartCoroutine(InstantiateTargets());
    }

    IEnumerator InstantiateTargets() //IEnumerator responsible for instantiating and spawning targets
    {
        while (true) //Using a while loop so spawning continues
        {
            foreach (GameObject prefabs in targetObjects)
            {
                GameObject instantiatedTargets = Instantiate(prefabs, transform.position, transform.rotation);
                //Debug.Log("Spawning targets!");

                TargetClass target = instantiatedTargets.GetComponent<TargetClass>(); //Grabs a reference to the Target (Parent) class and assigns the Instantiated targets that derive from it, to it!

                if (target != null)
                {
                    target.initialisePoints(lerpPoints); //Assign the lerp points to the targets by calling the initialisePoints from the target class
                }

                spawnedTargets.Add(instantiatedTargets); //Add the instantiatedTargets to the spawned Targets list
                //Debug.Log("Waiting!");
                yield return new WaitForSeconds(SpawnTime); //Uses the SpawnTime float variable declared in the Parent Class
            }
        }
    }
}
