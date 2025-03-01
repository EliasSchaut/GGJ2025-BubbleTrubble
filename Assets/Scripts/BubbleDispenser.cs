using UnityEngine;
using UnityEngine.Serialization;

public class BubbleDispenser : MonoBehaviour
{
    [SerializeField] private GameObject bubbleSpawnMarker;
    [SerializeField] private GameObject bubblePrefab;
    private MultiAudioSourcePlayer soundPlayer = null;
    
    void Start()
    {
        soundPlayer = GetComponent<MultiAudioSourcePlayer>();
    }
    
    public Bubble SpawnBubble()
    {
        Debug.Log("Spawning bubble");
        GameObject bubble = Instantiate(bubblePrefab, GetSpawnPoint(), Quaternion.identity);
        Bubble bubbleComponent = bubble.GetComponent<Bubble>();
        bubbleComponent.SetState(BubbleState.OnBelt);
        bubbleComponent.SetBeltIndex(0);
        soundPlayer.PlaySound(0);
        return bubbleComponent;
    }
    
    
    public Vector3 GetSpawnPoint()
    {
        return bubbleSpawnMarker.transform.position;
    }
}