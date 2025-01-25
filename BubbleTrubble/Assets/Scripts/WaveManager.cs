using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject[] motherships;
    [SerializeField] private GameObject[] childships;

    [SerializeField] private Transform spawnA;
    [SerializeField] private Transform spawnB;
    
    [SerializeField] private float waveDuration = 5.0f;

    private Vector3 min;
    private Vector3 max;
    
    private int currentWave;
    private float timer;
    
    private void Start()
    {
        currentWave = 0;
        
        min = Vector3.Min(spawnA.position, spawnB.position);
        max = Vector3.Max(spawnA.position, spawnB.position);
        
        StartWave();
    }
    
    private void StartWave()
    {
        currentWave += 1;
        timer = 0.0f;
        
        (int childships, int mothership) counts = GetCountsCount();
        
        for (int i = 0; i < counts.childships; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition();
            Instantiate(childships[UnityEngine.Random.Range(0, childships.Length)], spawnPosition, Quaternion.identity);
        }
        
        for (int i = 0; i < counts.mothership; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition();
            Instantiate(motherships[UnityEngine.Random.Range(0, motherships.Length)], spawnPosition, Quaternion.identity);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= waveDuration)
        {
            StartWave();
        }
    }

    private (int childships, int mothership) GetCountsCount()
    {
        return currentWave switch
        {
            0 => (1, 0),
            < 3 => (currentWave, 0),
            < 5 => (3, 1),
            < 7 => (3, 2),
            < 9 => (3, 3),
            _ => (3, 4)
        };
    }

    private Vector3 GetSpawnPosition()
    {
        return new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
    }
}
