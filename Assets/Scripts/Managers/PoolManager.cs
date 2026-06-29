using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour //Script for object pooling 
{

    public static PoolManager Instance;

    //General Variables
    [SerializeField] private List<GameObject> targetPrefabs; //Reference to the objects that I want to pool
    [SerializeField] private int poolSize; //To control the size of the pool

    //1) Create a collection (list or array) to store the objects in. And create a collection for free objects
    private List<GameObject> freeObjectsList;
    private List<GameObject> usedObjectsList;


    //2) Initialise objects on awake
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

        freeObjectsList = new List<GameObject>();
        usedObjectsList = new List<GameObject>();

        GenerateObjectsToPool();
    }

    private void GenerateObjectsToPool()
    {
        // 3) Use a for each loop to loop through the prefab list and add the objects to the list
        foreach (GameObject prefab in targetPrefabs)
        {
            // 4) Add all the objects to pool to
            for (int i = 0; i < poolSize; i++)
            {
                // 5) Fill out pool by instantiating the prefabs
                GameObject obj = Instantiate(prefab);

                // 6) Attached the Instantiated objects to the parent game object
                obj.transform.parent = transform;
                obj.transform.rotation = transform.rotation;


                obj.SetActive(false); //Disables all the objects in the scene
                freeObjectsList.Add(obj); //Add the instantiated objects to the free objects list

            }
        }
    }

    // 7) Create a new public method to get an object from the pool. Other scripts will be able to utilise this method
    public GameObject GetPooledObject()
    {
        // 8) Check if the list is not empty
        int ObjectsFree = freeObjectsList.Count; //Storing the number of free objects in an integer 

        if (ObjectsFree == 0) //Generate an object if there are no free objects
        {
            GenerateObjectsToPool();
        }

        // 9) Add object to the used list and take 1 away from the free list
        GameObject targetObj = freeObjectsList[ObjectsFree - 1];

        //10) Remove a pooled object from the free list
        freeObjectsList.RemoveAt(ObjectsFree - 1);

        // 11) Add the pooled object to the used list
        usedObjectsList.Add(targetObj);

        return targetObj;

    }


    // 12) Create a public method to return an object to the pool. Other scripts will be able to utilise this method
    public void ReturnPooledObject(GameObject targetObj)
    {
        // 13) Remove the object from the used list
        usedObjectsList.Remove(targetObj);

        // 14) Add the pooled object to the free objects list
        freeObjectsList.Add(targetObj);

        // xx) Set the gameobject back to false as it no longer is being used
        targetObj.SetActive(false);
    }

}
