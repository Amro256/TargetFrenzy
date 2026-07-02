using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.Search;

public class PoolManager : MonoBehaviour //Script for object pooling 
{

    public class PoolMember : MonoBehaviour // 30/6/26: Added a class that acts as a data container 
    {
        public GameObject prefab;
    }

    //Singleton
    public static PoolManager Instance;

    //General Variables
    [SerializeField] private List<GameObject> targetPrefabs; //Reference to the objects I want to pool
    [SerializeField] private int poolSize; //To control the size of the pool

    //30/6/26: Refactoring to use a dictionary
    
    // 1) Create a dictionary with the key of "Game Object" and a queue that will store (a collection of) objects of type "GameObject"
    private Dictionary<GameObject, Queue<GameObject>> poolDictionary;

    //) Create a collection (list or array) to store the objects in. And create a collection for free objects -- 30:6:26: The lists were removed and refactored to use a dictionary instead


    //2) Initialise objects on awake / Singleton pattern
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // 2) Initialise the dictionary, which create an empty dictionary waiting to be populated
        poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

        GenerateObjectsToPool();
    }

    private void GenerateObjectsToPool()
    {
        // 3) Use a for each loop to loop through the prefab list and add the objects to the list
        foreach (GameObject objectPrefab in targetPrefabs)
        {
            // 4) Create a new queue of objects that will hold and add the instantiated objects to it
            Queue<GameObject> targetPool = new Queue<GameObject>();

            // 5) Add all the objects to the target pool
            for (int i = 0; i < poolSize; i++)
            {

                // 6) Fill out pool by instantiating the prefabs
                GameObject obj = Instantiate(objectPrefab);

                //Object (copy of the class)
                PoolMember member = obj.AddComponent<PoolMember>(); //Adds the "PoolMember" component to the instantiated objects
                member.prefab = objectPrefab; //Holds a reference to the original prefab


                // 7) Attached the Instantiated objects to the parent game object
                obj.transform.parent = transform;
                obj.transform.rotation = transform.rotation;


                obj.SetActive(false); //Disables all the objects in the scene
                targetPool.Enqueue(obj); //Add the Instantiated object to the queue
            }

            // 8) Add this queue of objects to the dictionary
            poolDictionary.Add(objectPrefab, targetPool);
        }
    }

    // 9) Create a new public method to get an object from the pool. Other scripts will be able to utilise this method
    public GameObject GetPooledObject(GameObject prefab)
    {
        // 10) Get the object queue that is linked to the "prefab" key
        Queue<GameObject> pool = poolDictionary[prefab]; //Note to self the "prefab" used here is the dictionary key

        if (pool.Count == 0) //If there are no elements in the pool queue then generate objects
        {
            GenerateObjectsToPool();
        }

        // 11) Remove an object from the queue for use in game
        GameObject targetObj = pool.Dequeue();
        targetObj.SetActive(true); //Set the object to true so it becomes visible 

        return targetObj;
    }


    // 12) Create a public method to return an object to the pool. Other scripts will be able to utilise this method
    public void ReturnPooledObject(GameObject targetObj)
    {
        // 13) Fetch the "PoolMember" component that is attached to the current object
        PoolMember member = targetObj.GetComponent<PoolMember>();

        // 14) Return the correct objects to the queue based on the store prefab reference 
        poolDictionary[member.prefab].Enqueue(targetObj);

        // 15) Set the gameobject back to false as it no longer is being used
        targetObj.SetActive(false);

    }

}
