using UnityEngine;
using TMPro;
using System;
using Mono.Cecil.Cil;

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


    public void DisplayPopUp(Vector3 position)
    {
        GameObject obj = Instantiate(popUpPrefab, position, Quaternion.identity);
        obj.GetComponent<TextMeshPro>().text = ScoreManager.Instance.HitScore.ToString();
    }

}
