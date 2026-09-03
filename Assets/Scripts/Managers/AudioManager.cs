using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance {get; private set;} //Make the audioManager static so it can accessed by other scripts
    [SerializeField] private AudioData audioData; //Reference to the scriptable Object
    private AudioSource audioSource; //Private reference to the audio source component


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


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Grabs the audio source component on start
    }

    //Method to play Sound - Make it static so other scripts can access and call it!
    public void PlaySound(AudioClip audioClip, float volume)
    {
        audioSource.PlayOneShot(audioClip, audioData.Volume);
        
    }
}
