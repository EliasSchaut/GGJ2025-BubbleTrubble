using UnityEngine;
using UnityEngine.Serialization;

public class BubbleSpawner : MonoBehaviour
{
    
    [SerializeField] private float spawnRate = 1.0f;
    [SerializeField] private GameObject bubble;
    [SerializeField] private GameObject bubbleSpawnMarker;
    private double spawnCounter;
    
    private MultiAudioSourcePlayer soundPlayer = null;
    
    void Start()
    {
        soundPlayer = GetComponent<MultiAudioSourcePlayer>();
        spawnCounter = 0;
    }

    void Update()
    {
        spawnCounter += Time.deltaTime;
        if (spawnCounter >= spawnRate)
        {
            spawnCounter -= spawnRate;
            SpawnBubble();
        }
    }

    void SpawnBubble()
    {
        Instantiate(bubble, bubbleSpawnMarker.transform);
        soundPlayer.PlaySound(0);
    }
}
