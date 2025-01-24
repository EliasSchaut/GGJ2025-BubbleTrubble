using UnityEngine;
using UnityEngine.Serialization;

public class BubbleSpawner : MonoBehaviour
{
    
    [SerializeField] private int spawnRate = 1;
    [SerializeField] private GameObject bubble;
    [SerializeField] private GameObject bubbleSpawnMarker;
    private double spawnCounter;
    
    void Start()
    {
        spawnCounter = 0;
    }

    void Update()
    {
        spawnCounter += Time.deltaTime;
        if (spawnCounter >= spawnRate)
        {
            spawnCounter = 0;
            SpawnBubble();
        }
    }

    void SpawnBubble()
    {
        Instantiate(bubble, bubbleSpawnMarker.transform);
    }
}
