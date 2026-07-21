using UnityEngine;
using TMPro;
using System;
using Mono.Cecil.Cil;
using NUnit.Framework.Internal;

public class ScorePopUpManager : MonoBehaviour //This script will be responsible for floating / pop up score values when a target is hit
{
    public static ScorePopUpManager Instance;

    //General Variables
    [SerializeField] private GameObject popUpPrefab;


    void Awake() //Singleton pattern
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
    }


    public void DisplayScorePopUp(Vector3 position)
    {
        //offset
        Vector3 offset = new Vector3(0, 0.7f, 0);
        GameObject obj = Instantiate(popUpPrefab, position + offset , Quaternion.identity);
        obj.transform.GetChild(0).GetComponent<TextMeshPro>().text = ScoreManager.Instance.HitScore.ToString();
        
        
        Destroy(obj, 3f); //Destroy the game object after 3 seconds
    }

}
