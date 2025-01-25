using UnityEngine;

public class MultiAudioSourcePlayer : MonoBehaviour
{
    private AudioSource[] audioSources;

    void Start()
    {
        // Get all AudioSources on the GameObject
        audioSources = GetComponents<AudioSource>();
    }

    public void PlaySound(int index)
    {
        if (index >= 0 && index < audioSources.Length) {
            audioSources[index].Play();
        }
    }
}