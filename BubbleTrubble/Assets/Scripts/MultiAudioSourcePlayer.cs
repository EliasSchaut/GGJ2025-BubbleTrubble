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
    		return;
    	}
        if (index >= 0 && index < audioSources.Length) {
            if (audioSources[index]) {
                audioSources[index].Play();
            }
        }
    }
}