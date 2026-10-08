using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour //This script will hold and manage animations
{
    public static AnimationManager Instance { get; private set; } //Singleton Pattern

    //General Variables
    [SerializeField] private Animator[] animators; //Array to hold the animator components attached to different game objects

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    //Methods to start and stop the animation
    public void StartAnimation(int animatorIndex, string aniParam)
    {
        if (animatorIndex >= 0 && animatorIndex < animators.Length)
        {
            if (animators[animatorIndex].runtimeAnimatorController != null)
            {
                animators[animatorIndex].SetBool(aniParam, true);
            }
            
        }
    }

    public void StopAnimation(Animator animator, string aniParam)
    {
    
        animator.SetBool(aniParam, false);
        
    }
    
}
