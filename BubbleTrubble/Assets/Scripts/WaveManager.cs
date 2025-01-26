using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject[] motherships;
    [SerializeField] private GameObject[] childships;

    [SerializeField] private Transform spawnA;
    [SerializeField] private Transform spawnB;

    [SerializeField] private GameObject soundManager;
    
    [SerializeField] private float waveDuration = 5.0f;
    
    private Vector3 min;
    private Vector3 max;
    
    private int currentWave;
    private int currentMothershipWave;
    private float timer;
    private float checkTimer;

    private static int enemyCount = 0;
    
    private void Start()
    {
        currentWave = 0;
        currentMothershipWave = 0;
        
        min = Vector3.Min(spawnA.position, spawnB.position);
        max = Vector3.Max(spawnA.position, spawnB.position);

        timer = 15.0f;
    }

    private void StartWave()
    {
        currentWave += 1;
        GameManager.Instance.SetWave(currentWave);
        timer = 0.0f;

        (int childships, int mothership) counts = GetShipsCount();

        for (int i = 0; i < counts.childships; i++) {
            Vector3 spawnPosition = GetSpawnPosition();
            Instantiate(GetElement(childships), spawnPosition, gameObject.transform.rotation);
        }

        if (counts.mothership > 0) {
            currentMothershipWave += 1;
        }

        for (int i = 0; i < counts.mothership; i++) {
            Vector3 spawnPosition = GetSpawnPosition();

            EnemyMothership mothership = Instantiate(GetElement(motherships), spawnPosition, Quaternion.identity)
                .GetComponent<EnemyMothership>();

            mothership.Wave = currentMothershipWave;
        }

        soundManager.GetComponent<SoundManager>().SwitchToIntense();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= waveDuration)
        {
            StartWave();
        }
        
        checkTimer += Time.deltaTime;
        if (checkTimer >= 1.0f) {
            checkTimer = 0;
            if (enemyCount == 0) {
                soundManager.GetComponent<SoundManager>().SwitchToRelaxed();
            }
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

    public static void IncreateEnemyCount()
    {
        enemyCount += 1;
    }

    public static void DecreaseEnemyCount()
    {
        enemyCount -= 1;
    }
    
    
}
