using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour //Script for object pooling 
{

    //General Variables
    [SerializeField] private List<GameObject> targetPrefabs; //Reference to the objects that I want to pool
    [SerializeField] private int poolSize; //To control the size of the pool

    //1) Create a collection (list or array) to store the objects in. And create a collection for free objects
    private List<GameObject> freeObjectsList;
    private List<GameObject> usedObjectsList;


    //2) Initialise objects on awake
    void Awake()
    {
        freeObjectsList = new List<GameObject>();
        usedObjectsList = new List<GameObject>();

        // 3) Use a for each loop to loop through the prefab list and add the objects to the list
        foreach (GameObject prefab in targetPrefabs)
        {
            // 4) Add all the objects to pool to
            for (int i = 0; i < poolSize; i++)
            {
            // 5) Fill out pool by instantiating the prefabs
            GameObject obj = Instantiate(prefab);

            obj.transform.parent = transform;
            obj.transform.rotation = transform.rotation;
            obj.SetActive(false); //Disables all the objects in the scene
            freeObjectsList.Add(obj);
            }
        }
        
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


}
