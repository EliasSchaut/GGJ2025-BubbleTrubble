using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public enum BubbleState
{
    OnBelt,
    CarriedByPlayer,
    OnMachine,
    MovingToTurret,
    OnTurret,
    OnSink,
}

public class Bubble : MonoBehaviour, IInteractable
{
    private BubbleManager bubbleManager;
    
    private BubbleColor bubbleColor = BubbleColor.White;

    private MultiAudioSourcePlayer soundPlayer = null;
    
    private BubbleState currentState = BubbleState.OnBelt;

    private int maxShots = 10;
    
    private int shots = 10;
    
    private int beltIndex = 0;
    
    void Start()
    {
        soundPlayer = GetComponent<MultiAudioSourcePlayer>();
        BubbleColors.SetObjectColor(gameObject, bubbleColor);
    }
    
    public void SetColor(BubbleColor color)
    {
        bubbleColor = color;
        BubbleColors.SetObjectColor(gameObject, bubbleColor);
    }
    
    public BubbleColor GetColor()
    {
        return bubbleColor;
    }

    public bool WouldChangeColor(BubbleColor color)
    {
        if (color == bubbleColor) {
            return false;
        }
        return true;
    }
    
    public void MixWithColor(BubbleColor color)
    {
        if (color == bubbleColor)
        {
            return;
        }

        if (color == BubbleColor.Black || color == BubbleColor.Brown || color == BubbleColor.White)
        {
            bubbleColor = color;
        	BubbleColors.SetObjectColor(gameObject, bubbleColor);
            return;
        }

        if ((bubbleColor & color) == 0) {
            bubbleColor = bubbleColor | color;
        }
        else {
            bubbleColor = bubbleColor & color;
        }
        
        BubbleColors.SetObjectColor(gameObject, bubbleColor);
    }

    public void PlayColorizeSound()
    {
        soundPlayer = GetComponent<MultiAudioSourcePlayer>();
        soundPlayer.PlaySound(0);
    }
    
    public void SetState(BubbleState state)
    {
        currentState = state;
    }
    
    public BubbleState GetState()
    {
        return currentState;
    }
    
    public bool UseShot()
    {
        shots--;
        if (shots <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        {
            float scale = Mathf.Clamp01(shots / (float)maxShots);
            transform.localScale = new Vector3(scale, scale, scale);
        }

        return false;
    }

    public void DestroyBubble()
    {
        Destroy(gameObject);
    }
    
    public void SetBeltIndex(int index)
    {
        beltIndex = index;
    }
    
    public int GetBeltIndex()
    {
        return beltIndex;
    }

    public bool Interact(Player player)
    {
        if (player.HoldsBubble()) {
            return false;
        }
        if (currentState == BubbleState.OnBelt) {
            player.SetBubble(gameObject);
            return true;
        }
        return false;
    }
    
    public void SetBubbleManager(BubbleManager bubbleManager)
    {
        this.bubbleManager = bubbleManager;
    }
}
