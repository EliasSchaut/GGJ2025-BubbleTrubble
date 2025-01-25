using UnityEngine;

public class Machine : MonoBehaviour, IInteractable
{
    public BubbleColor machineColor;
    
    private GameObject currentBubble = null;
    
    private bool isProcessing = false;
    private bool soundPlayed = false;
    
    private float processingStartTime = 0f;
    
    private float processingTime = 2f;
    
    [SerializeField] private GameObject bubbleManager;
    
    private BubbleManager bubbleManagerScript;

    private void Start()
    {
        bubbleManagerScript = bubbleManager.GetComponent<BubbleManager>();
    }
    
    private void Update()
    {
        if (currentBubble && isProcessing && (Time.time - processingStartTime) > processingTime)
        {
            isProcessing = false;
            soundPlayed = true;
            currentBubble.GetComponent<Bubble>().MixWithColor(machineColor);
        }
        if (currentBubble && isProcessing && !soundPlayed && (Time.time - processingStartTime) > processingTime / 2.0f)
        {
            var bubbleComponent = currentBubble.GetComponent<Bubble>();
            if (bubbleComponent.WouldChangeColor(machineColor)) {
                bubbleComponent.PlayColorizeSound();
            }
            soundPlayed = true;
        }
    }

    public void PutBubble(GameObject bubble)
    {
        if (!HasBubble())
        {
            bubble.GetComponent<Bubble>().SetState(BubbleState.OnMachine);
            bubble.transform.parent = transform;
            bubble.transform.localPosition = new Vector3(0, 0.5f, 0);
            currentBubble = bubble;
            processingStartTime = Time.time;
            isProcessing = true;
            soundPlayed = false;
        }
    }

    public bool HasBubble()
    {
        return currentBubble != null;
    }
    
    public float GetProcessingProgress()
    {
        if (!HasBubble())
        {
            return 0f;
        }
        return Mathf.Clamp01((Time.time - processingStartTime) / processingTime);
    }

    public bool Interact(Player player)
    {
        if (player.HoldsBubble()) {
            if (HasBubble()) {
                Debug.Log("Player has Bubble and Machine has bubble!");
                // both bubbles will be destroyed
                var playerBubble = player.GetBubble();
                bubbleManagerScript.QueueDestroy(playerBubble.GetComponent<Bubble>());
                player.SetBubble(null);
                var machineBubble = currentBubble;
                currentBubble = null;
                isProcessing = false;
                soundPlayed = true;
                bubbleManagerScript.QueueDestroy(machineBubble.GetComponent<Bubble>());
            } else {
                Debug.Log("Player has Bubble and Machine is empty!");
                // put bubble on machine
                var bubble = player.GetBubble();
                PutBubble(bubble);
                player.SetBubble(null);
            }
            return true;
        } else {
            if (HasBubble()) {
                Debug.Log("Machine has bubble, player wants to carry!");
                isProcessing = false;
                soundPlayed = true;
                player.SetBubble(currentBubble);
                currentBubble = null;
                return true;
            }
        }
        return false;
    }
}