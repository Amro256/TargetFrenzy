using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerClass : MonoBehaviour //This is the base class that the spawner scripts will inherit from
{
    #region Custom Class for target weighting
    [System.Serializable] //This exposes the variables below in the inspector 
    public class SpawnEntry
    {
        public GameObject targetPrefab; // The target prefab
        public float targetWeight; // The associated prefab weighting 
    }
    #endregion

    [Header("General Variables")]
    [SerializeField] protected float SpawnTime; //Variable to control the spawn rate of the targets
    private protected List<SpawnEntry> activeTargets;  //Will be used to determine which targets to spawn (regular gameplay targets or bonus rounds targets, if the bonus round is active) 
    private List<GameObject> spawnedTargets = new List<GameObject>(); //Tracks and stores the current instantiated targets
    

    [Header("Regular Target List")] //List of targets to Instantiate
    [SerializeField] protected List<SpawnEntry> targetObjects = new List<SpawnEntry>();

    [Header("Bonus Round Target List")]
    [SerializeField] protected List<SpawnEntry> bonusTargetObjects = new List<SpawnEntry>();

    [Header("Movement Points")]
    [SerializeField] protected Transform[] lerpPoints; //The spawner itself will hold the lerp points' transform, as this will allow me to drag and drop them into the inspector with no issue!
                                                       //NOTE: This is also using a protected access modifier, so each of the spawner classes will be able to access this variable.

    #region unused code
    //-----------------------------------------------------------------------UNUSED---------------------------------------------------------------------------------------------------------
    // [Header("Lerp Positions")]  //Start and end points for lerping
    // [SerializeField] protected GameObject startPos;
    // [SerializeField] protected GameObject EndPos;
    //-----------------------------------------------------------------------UNUSED---------------------------------------------------------------------------------------------------------
    #endregion unused code


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
                PoolManager.Instance.ReturnPooledObject(target); // 07/7/26: This was changed from destroying the target object to calling the returnPooled method
            }
        }
           
    }

    //Method to handle selecting a target based on weighting
    private GameObject SelectRandomTarget()
    {
        float totalWeight = 0f;

        foreach (SpawnEntry entry in activeTargets)
        {
            totalWeight += entry.targetWeight;
        }

        float randomPoint = Random.value * totalWeight;

        foreach (SpawnEntry entry in activeTargets)
        {
            randomPoint -= entry.targetWeight;

            if (randomPoint <= 0)
            {
                return entry.targetPrefab;
            }
        }

        return activeTargets[activeTargets.Count - 1].targetPrefab;
    }

    public IEnumerator InstantiateTargets() //IEnumerator responsible for instantiating and spawning targets
    {
        yield return new WaitUntil(() => GameManager.Instance.IsIntroSeqPlaying == false);

        if (GameManager.Instance.BonusRoundBool) //if the bonus round is active, set the active targets to the bonus round targets (golden target)
        {
            activeTargets = bonusTargetObjects;
        }
        else
        {
            activeTargets = targetObjects; //else set the active targets to the regular gameplay targets
        }


        while (true ) //Using a while loop so the spawning continues
        {
            if (PoolManager.Instance.HasReachedMaxOnScreen)
            {
                Debug.Log("There are enough targets on screen. Pause Spawning");
                yield return new WaitUntil(() => PoolManager.Instance.objectsOnScreen <= 0);

                Debug.Log("Resume Spawning");
            }


            GameObject prefab = SelectRandomTarget();

            GameObject instantiatedTargets = PoolManager.Instance.GetPooledObject(prefab);

            instantiatedTargets.transform.position = transform.position;
            instantiatedTargets.transform.rotation = transform.rotation;

            //(activeTargets[Random.Range(0, activeTargets.Count)], transform.position, transform.rotation); //28/5/26: Changed from "prefabs" to "targetObjects" so that the targets can be randomised on start


            TargetClass target = instantiatedTargets.GetComponent<TargetClass>(); //Grabs a reference to the Target (Parent) class and assigns the Instantiated targets that have the target class attached to it

            if (target != null) //Checks if the Instantiated targets have the target script attached to it, and if so run the code below
            {
                target.initialisePoints(lerpPoints); //Assigns the lerp points to the targets by calling the initialisePoints from the target class
            }
            else
            {
                Debug.LogError("No Target script found on the Instantiated target!"); //Error handling 
            }

            spawnedTargets.Add(instantiatedTargets); //Add the instantiated Targets to the spawned Targets list
            yield return new WaitForSeconds(SpawnTime); //Uses the SpawnTime float variable declared in the Parent Class

        }

    }
        

}
