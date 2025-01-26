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
    private float timer;
    private float checkTimer;

    private static int enemyCount = 0;
    
    private void Start()
    {
        currentWave = 0;
        
        min = Vector3.Min(spawnA.position, spawnB.position);
        max = Vector3.Max(spawnA.position, spawnB.position);

        timer = 15.0f;
    }

    private void StartWave()
    {
        currentWave += 1;
        GameManager.Instance.SetWave(currentWave);
        timer = 0.0f;

        (int c1, int c2, int c3, int m) = GetShipsCount();

        for (int i = 0; i < c1; i++) {
            Vector3 spawnPosition = GetSpawnPosition();
            Instantiate(childships[0], spawnPosition, gameObject.transform.rotation);
        }
        
        for (int i = 0; i < c2; i++) {
            Vector3 spawnPosition = GetSpawnPosition();
            Instantiate(childships[1], spawnPosition, gameObject.transform.rotation);
        }
        
        for (int i = 0; i < c3; i++) {
            Vector3 spawnPosition = GetSpawnPosition();
            Instantiate(childships[2], spawnPosition, gameObject.transform.rotation);
        }

        for (int i = 0; i < m; i++) {
            Vector3 spawnPosition = GetSpawnPosition();

            EnemyMothership mothership = Instantiate(motherships[0], spawnPosition, Quaternion.identity)
                .GetComponent<EnemyMothership>();

            mothership.Wave = currentWave;
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

    private (int ship1, int ship2, int ship3, int mothership) GetShipsCount()
    {
        return currentWave switch
        {
            0 => (1, 0, 0, 0),
            < 3 => (currentWave, 0, 0, 0),
            < 6 => (2, currentWave, 0, 0),
            < 9 => (3, 2, currentWave, 0),
            < 10 => (0, 0, 0, 1),
            < 13 => (5, 2, 1, 2),
            _ => (5, 4, 3, 3),
        };
    }

    private Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
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
