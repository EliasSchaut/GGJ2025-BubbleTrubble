using UnityEngine;

public class EnemyMothership : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float interArrivalTime = 3f;
    
    private float timer;
    
    public int Wave { get; set; }
    
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer < interArrivalTime) return;

        timer = 0;

        int index = Mathf.Min(Random.Range(0, enemies.Length), Wave - 1);
        Instantiate(enemies[index], spawnPoint.position, Quaternion.identity);
    }
}
