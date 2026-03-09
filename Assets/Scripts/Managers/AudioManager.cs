using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private static AudioManager instance; //Make the audioManager static so it can accessed by other scripts

    public static AudioManager Instance;
    private AudioSource audioSource; //Private reference to the audio source component

    [SerializeField] private AudioClip[] audioClips;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Grans the audio source component on start
    }

    //Method to play Sound - Make it static so other scripts can access and call it!
    public void PlaySound(AudioClip clip, float volume)
    {
        instance.audioSource.clip = clip;
        instance.audioSource.volume = volume;

        instance.audioSource.Play();
        
    }
}
