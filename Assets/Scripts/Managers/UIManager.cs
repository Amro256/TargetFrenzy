using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    //Migrate UI functionality from the game manager here!!!

    //General variables / others
    public static UIManager Instance; //Static instance so other scripts can access this
    [SerializeField] public TMP_Text scoreText;
    [SerializeField] private GameObject[] ammoSprites;
   

    void Awake() //Singleton pattern
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
           DontDestroyOnLoad(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

}
