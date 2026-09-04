using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set; } //Make the audioManager static so it can accessed by other scripts
    [SerializeField] private AudioData audioData; //Reference to the scriptable Object

    //Audio Source References
    [SerializeField] AudioSource SFXSource; //Private reference to the audio source component
    [SerializeField] AudioSource UISource;
    [SerializeField] AudioSource MenuSource;


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

    //Method to play Sound - Make it static so other scripts can access and call it!
    public void PlaySFX(AudioClip audioClip, float volume)
    {
        SFXSource.PlayOneShot(audioClip, audioData.Volume);

    }

    public void PlayUISFX(AudioClip audioClip, float volume)
    {
        UISource.PlayOneShot(audioClip, audioData.Volume);

    }
    
    public void PlayMenuSFX(AudioClip audioClip, float volume)
    {
        MenuSource.PlayOneShot(audioClip, audioData.Volume);
        
    }
}
