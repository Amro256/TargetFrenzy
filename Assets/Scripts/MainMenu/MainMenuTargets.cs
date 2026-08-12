using UnityEngine;

public class MainMenuTargets : MonoBehaviour
{

    //References
    [SerializeField] private Transform spawnPoint;
    

    //Prefabs
    [SerializeField] private GameObject[] mainMenuTargets;

    void Start()
    {
        InstantiateMainMenuTargets();
    }

    void InstantiateMainMenuTargets()
    {
        foreach (GameObject target in mainMenuTargets)
        {
            Instantiate(target, spawnPoint.position, Quaternion.identity);

           
        }
        
    }
}
