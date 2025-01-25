using UnityEngine;

public class Machine : MonoBehaviour, IInteractable
{
    public BubbleColor machineColor;
    
    private Bubble currentBubble = null;
    
    private bool isProcessing = false;
    
    private float processingStartTime = 0f;
    
    private float processingTime = 2f;

    private void Start()
    {
    }
    
    private void Update()
    {
        if (currentBubble && isProcessing && (Time.time - processingStartTime) > processingTime)
        {
            isProcessing = false;
            currentBubble.MixWithColor(machineColor);
        }
    }

    public void PutBubble(Bubble bubble)
    {
        if (!HasBubble())
        {
            bubble.SetState(BubbleState.OnMachine);
            bubble.transform.parent = transform;
            bubble.transform.localPosition = new Vector3(0, 0.5f, 0);
            currentBubble = bubble;
            processingStartTime = Time.time;
            isProcessing = true;
        }
    }

    public bool HasBubble()
    {
        return !currentBubble;
    }
    
    public float GetProcessingProgress()
    {
        if (!HasBubble())
        {
            return 0f;
        }
        return Mathf.Clamp01((Time.time - processingStartTime) / processingTime);
    }

    public void Interact(Player player)
    {
        if (player.HoldsBubble()) {
            if (!)
            
        }
        
    }
}