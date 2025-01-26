using UnityEngine;

public class MultiAudioSourcePlayer : MonoBehaviour
{
    private AudioSource[] audioSources = null;

    void Start()
    {
        // Get all AudioSources on the GameObject
        audioSources = GetComponents<AudioSource>();
    }

    public void PlaySound(int index)
    {
        if (audioSources == null) {
            audioSources = GetComponents<AudioSource>();
        }
        if (audioSources == null) {
    		return;
    	}
        if (index >= 0 && index < audioSources.Length) {
            if (audioSources[index]) {
                audioSources[index].Play();
            }
        }
    }

    public void SetVolume(int index, float volume)
    {
        if (audioSources == null) {
            audioSources = GetComponents<AudioSource>();
        }
        if (audioSources == null) {
            return;
        }

        if (index >= 0 && index < audioSources.Length) {
            if (audioSources[index]) {
                audioSources[index].volume = volume;
            }
        }
    }
}