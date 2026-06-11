using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner4 : SpawnerClass //This class inherits from the Spawner Class (Read it like this: Spawner 1 IS A Spawner)
{

    //Variables & Lists

    //11/6/26: Most of the code in this script (and the other spawner scripts) has been moved to the spawner class. They are inherit the same behaviour, so why copy them into their individual scripts.
    // It'll also make debugging easier, as I only have to go through one centralised script. 

    void Start()
    {
        SpawnTargets(); //Call the SpawnTargets method here
    }

    //Override the Instantiation code here
    public override void SpawnTargets()
    {
        StartCoroutine(InstantiateTargets());
    }
}
