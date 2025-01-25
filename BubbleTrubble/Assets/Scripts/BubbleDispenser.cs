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

    public void SpawnBubble()
    {
        GameObject bubble = Instantiate(bubblePrefab, bubbleSpawnMarker.transform);
        Bubble bubbleComponent = bubble.GetComponent<Bubble>();
        bubbleComponent.SetState(BubbleState.OnBelt);
        soundPlayer.PlaySound(0);
    }
}