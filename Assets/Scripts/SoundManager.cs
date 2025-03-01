using Unity.Mathematics.Geometry;
using UnityEngine;
using Math = System.Math;

public class SoundManager : MonoBehaviour
{
    private MultiAudioSourcePlayer soundPlayer = null;

    private float backgroundVolume;
    
    private float dynamicVolume;

    private float backgroundVolumeTarget;
    
    private float dynamicVolumeTarget;

    private float fadeDuration = 5.0f;
    
    protected static SoundManager instance = null;
    
    void Start()
    {
        soundPlayer = GetComponent<MultiAudioSourcePlayer>();
        backgroundVolume = 0;
        dynamicVolume = 0;
        backgroundVolumeTarget = 1.0f;
        dynamicVolumeTarget = 0;
        soundPlayer.SetVolume(0, backgroundVolume);
        soundPlayer.PlaySound(0);
        soundPlayer.SetVolume(1, dynamicVolume);
        soundPlayer.PlaySound(1);
        instance = this;
    }

    public static SoundManager Instance()
    {
        return instance;
    }
    
    private void Update()
    {
        var delta = backgroundVolumeTarget - backgroundVolume;
        if (delta != 0) {
            backgroundVolume += Math.Sign(delta) * (1.0f / fadeDuration) * Time.deltaTime;
            if (Math.Sign(delta) == 1 && backgroundVolume > backgroundVolumeTarget) {
                backgroundVolume = backgroundVolumeTarget;
            }

            if (Math.Sign(delta) == -1 && backgroundVolume < 0) {
                backgroundVolume = 0;
            }
            soundPlayer.SetVolume(0, backgroundVolume);
        }
        delta = dynamicVolumeTarget - dynamicVolume;
        if (delta != 0) {
            dynamicVolume += Math.Sign(delta) * (1.0f / fadeDuration) * Time.deltaTime;
            if (Math.Sign(delta) == 1 && dynamicVolume > dynamicVolumeTarget) {
                dynamicVolume = dynamicVolumeTarget;
            }

            if (Math.Sign(delta) == -1 && dynamicVolume < 0) {
                dynamicVolume = 0;
            }
            soundPlayer.SetVolume(1, dynamicVolume);
        }
    }

    public void SwitchToIntense()
    {
        dynamicVolumeTarget = 1.0f;
        backgroundVolumeTarget = 0;
    }
    
    public void SwitchToRelaxed()
    {
        dynamicVolumeTarget = 0;
        backgroundVolumeTarget = 1.0f;
    }

    public void PlayElevatorSound()
    {
        soundPlayer.PlaySound(2);
    }
    
    public void PlayEnemyExplosionSound()
    {
        soundPlayer.PlaySound(3);
    }

}
