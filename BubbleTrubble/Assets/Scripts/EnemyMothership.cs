using UnityEngine;

public class EnemyMothership : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float interArrivalTime = 3f;
    
    private float timer;
    
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer < interArrivalTime) return;

        timer = 0;
            
        int index = Random.Range(0, enemies.Length);
        Instantiate(enemies[index], spawnPoint.position, Quaternion.identity);
    }
}
