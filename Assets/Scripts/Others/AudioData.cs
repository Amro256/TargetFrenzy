using UnityEngine;

// Use the CreateAssetMenu attribute to allow creating instances of this ScriptableObject from the Unity Editor.
[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/AudioData")] //Creates a menu in the "CreateAssetsMenu" (Right click --> Create --> ScriptableObjects)
public class AudioData : ScriptableObject
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private float volume = 1;


    //Make properties of the two fields above so the audio manager can access
    public AudioClip[] Clips => clips;
    public float Volume => volume;
}
