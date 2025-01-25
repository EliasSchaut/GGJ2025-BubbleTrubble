using UnityEngine;
using UnityEngine.Serialization;

public class BubbleDispenser : MonoBehaviour
{
    [SerializeField] private GameObject bubbleSpawnMarker;
    private MultiAudioSourcePlayer soundPlayer = null;
    
    void Start()
    {
        soundPlayer = GetComponent<MultiAudioSourcePlayer>();
    }

    public void SpawnBubble(GameObject bubble)
    {
        Instantiate(bubble, bubbleSpawnMarker.transform);
        soundPlayer.PlaySound(0);
    }
}