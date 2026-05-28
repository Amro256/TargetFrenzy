using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance; //Make the audioManager static so it can accessed by other scripts
    private AudioSource audioSource; //Private reference to the audio source component

    [SerializeField] private AudioClip[] audioClips;

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


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Grabs the audio source component on start
    }

    //Method to play Sound - Make it static so other scripts can access and call it!
    public void PlaySound(AudioClip clip, float volume)
    {
        // audioSource.clip = clip;
        // audioSource.volume = volume;

        audioSource.PlayOneShot(clip, volume);
        
    }
}
