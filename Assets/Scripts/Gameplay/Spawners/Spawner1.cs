using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    void Update()
    {
        //Call the target Lerping method here
        //TargetLerping();
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
                spawnedTargets.Add(instantiatedTargets); //Add the instantiatedTargets to the spawned Targets list
                //Debug.Log("Waiting!");
                yield return new WaitForSeconds(SpawnTime); //Uses the SpawnTime float variable declared in the Parent Class
            }
        }
        
    }
}
