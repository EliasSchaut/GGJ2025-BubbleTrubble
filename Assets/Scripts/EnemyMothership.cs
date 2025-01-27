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
        
        Instantiate(enemies[0], spawnPoint.position, Quaternion.identity);
    }
}
