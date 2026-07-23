using UnityEngine;
using TMPro;
using System;
using Mono.Cecil.Cil;
using NUnit.Framework.Internal;

public class ScorePopUpManager : MonoBehaviour //This script will be responsible for floating / pop up score values when a target is hit
{
    public static ScorePopUpManager Instance;

    //General Variables
    [SerializeField] private GameObject popUpPrefab; //The Game object that has the text component attached to it
    [SerializeField] private GameObject MultiTextPrefab;
    [SerializeField] private GameObject TimerTextPrefab;
    
    [SerializeField] private GameObject multiPosition;
    [SerializeField] private GameObject timerInstantiatePosition;


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


    //Create a method to display for the score pop up. This method can be called in the individual target scripts
    public void DisplayScorePopUp(Vector3 position, int score, string text, Color textColour)
    {
        //1) Create a slight offset of the y axis, so the text doesnt spawn in the middle of the target
        Vector3 textOffset = new Vector3(0, 0.7f, 0);

        //2) Instantiate the prefab that will take in the targets' position plus the offset!
        GameObject obj = Instantiate(popUpPrefab, position + textOffset, Quaternion.identity);

        //3) Get the text mesh pro component attached to the child object. The 0 refers to the index, so 0 = the first child object
        TextMeshPro scoreText = obj.transform.GetChild(0).GetComponent<TextMeshPro>();

        scoreText.text = text + score.ToString(); //Converts the int score to a string
        scoreText.color = textColour; //Sets the colour of the text

        Destroy(obj, 1.5f); //Destroy the game object after 1.5 seconds
    }

    //Method for the multiplier pop up. This will only be called in the Multiplier target script
    public void DisplayMultiplierPopUp(int multi, string text, Color textColour)
    {
        //1) Instantiate the prefab that will take in the targets' position plus the offset!
        GameObject obj = Instantiate(MultiTextPrefab, multiPosition.transform.position, Quaternion.identity);

        //2) Get the text mesh pro component attached to the child object. The 0 refers to the index, so 0 = the first child object
        TextMeshPro scoreText = obj.transform.GetComponent<TextMeshPro>();

        scoreText.text = text + multi.ToString(); //Converts the int score to a string
        scoreText.color = textColour; //Sets the colour of the text

        Destroy(obj, 1.5f); //Destroy the game object after 1.5 seconds
    }
    

    public void DisplayTimerPopUp(int timer, string text, string sectext, Color textColour)
    {
        //1) Instantiate the prefab that will take in the targets' position plus the offset!
        GameObject obj = Instantiate(TimerTextPrefab, timerInstantiatePosition.transform.position, Quaternion.identity); 

        //2) Get the text mesh pro component attached to the child object. The 0 refers to the index, so 0 = the first child object
        TextMeshPro scoreText = obj.transform.GetComponent<TextMeshPro>();

        scoreText.text = text + timer.ToString() + sectext; //Converts the int score to a string
        scoreText.color = textColour; //Sets the colour of the text

        Destroy(obj, 1.5f); //Destroy the game object after 1.5 seconds
    }

}
