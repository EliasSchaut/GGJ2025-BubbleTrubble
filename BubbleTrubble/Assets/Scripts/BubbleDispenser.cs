using UnityEngine;
using UnityEngine.Serialization;

public class BubbleDispenser : MonoBehaviour, IBelt
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
        IBelt self = this;
        self.BeldUpdate();
        
        spawnCounter += Time.deltaTime;
        if (!self.IsOccupied && spawnCounter >= spawnRate)
        {
            spawnCounter = 0;
            SpawnBubble();
        }
    }

    void SpawnBubble()
    {
        Instantiate(bubble, bubbleSpawnMarker.transform);
    }

    bool IBelt.IsOccupied { get; set; } = false;
    IBelt IBelt.NextBelt { get; set; }
    GameObject IBelt.Bubble { get; set; }
    float IBelt.MovementSpeed { get; set; } = 1.0f;
    Vector2 IBelt.MovementDirection { get; set; }
    Vector2 IBelt.Position => transform.position;
}