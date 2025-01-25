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
        
        (int childships, int mothership) counts = GetShipsCount();
        
        for (int i = 0; i < counts.childships; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition();
            Instantiate(GetElement(childships), spawnPosition, Quaternion.identity);
        }
        
        for (int i = 0; i < counts.mothership; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition();
            
            EnemyMothership mothership = Instantiate(GetElement(motherships), spawnPosition, Quaternion.identity).GetComponent<EnemyMothership>();
            
            mothership.Wave = currentWave;
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

    private (int childships, int mothership) GetShipsCount()
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
        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
    }
    
    private GameObject GetElement(GameObject[] elements)
    {
        return elements[Mathf.Min(Random.Range(0, elements.Length), currentWave - 1)];
    }
}
