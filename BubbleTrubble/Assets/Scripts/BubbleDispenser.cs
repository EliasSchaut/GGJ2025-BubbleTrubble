using UnityEngine;
using UnityEngine.Serialization;

public class BubbleDispenser : MonoBehaviour, IBelt
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
        IBelt self = this;
        self.BeldUpdate();
        
        spawnCounter += Time.deltaTime;
        if (!self.IsOccupied && spawnCounter >= spawnRate)
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

    bool IBelt.IsOccupied { get; set; } = false;
    IBelt IBelt.NextBelt { get; set; }
    GameObject IBelt.Bubble { get; set; }
    float IBelt.MovementSpeed { get; set; } = 1.0f;
    Vector2 IBelt.MovementDirection { get; set; }
    Vector2 IBelt.Position => transform.position;
}