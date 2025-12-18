using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner1 : SpawnerClass //This class inherits from the Spawner Class (Read it like this: Spawner 1 IS A Spawner)
{

    //Variables & Lists
    private List<GameObject> spawnedTargets = new List<GameObject>(); //Tracks and stores the current instantiated targets
    private float lerptime = 0;

    void Start()
    {
        //Call the SpawnTargets method here
        SpawnTargets();
    }

    void Update()
    {
        //Call the target Lerping method here
        TargetLerping();
    }


    //Override the Instantiation code here
    public override void SpawnTargets()
    {
        foreach (GameObject prefabs in targetObjects)
        {
            GameObject instantiatedTargets = Instantiate(prefabs, transform.position, transform.rotation);
            spawnedTargets.Add(instantiatedTargets); //Add the instantiatedTargets to the spawned Targets list
        }
    }


    //Override the TargetLerping method Header
    public override void TargetLerping()
    {
        for (int i = 0; i < spawnedTargets.Count; i++)
        {
            GameObject target = spawnedTargets[i]; //Accessing the targets in the spawned target list

            lerptime += Time.deltaTime * targetMoveSpeed; //Increments the interpolation value using time.delta time multiplied by the move speed. Ensures consistent movement regardless of framerate

            target.transform.position = Vector2.Lerp(startPos.transform.position, EndPos.transform.position, lerptime);
        }
    }

}
