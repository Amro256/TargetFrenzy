using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour //This script will hold and manage animations
{
    public static AnimationManager Instance; //Singleton Pattern


    //General Variables
    [SerializeField] private Animator[] animators; //Array to hold the animator components attached to different game objects


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
    }



    //Methods to start and stop the animation
    public void StartAnimation(string aniParam)
    {
        foreach (Animator am in animators)
        {
            am.SetBool(aniParam, true);
        }
    }

    public void StopAnimation(string aniParam)
    {
        foreach (Animator am in animators)
        {
            am.SetBool(aniParam, false );
        }
    }
    
}
