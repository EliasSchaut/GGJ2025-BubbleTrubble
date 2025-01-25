using UnityEngine;

public enum BubbleState
{
    OnBelt,
    CarriedByPlayer,
    OnMachine,
    MovingToTurret,
    OnTurret
}

public class Bubble : MonoBehaviour
{
    private BubbleColor bubbleColor = BubbleColor.White;

    private MultiAudioSourcePlayer soundPlayer = null;
    
    private BubbleState currentState = BubbleState.OnBelt;

    private int shots = 10;
    
    private int beltIndex = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundPlayer = GetComponent<MultiAudioSourcePlayer>();
        UpdateComponentColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetColor(BubbleColor color)
    {
        bubbleColor = color;
        UpdateComponentColor();
    }
    
    public BubbleColor GetColor()
    {
        return bubbleColor;
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
            PlayColorizeSound();
            UpdateComponentColor();
            return;
        }

        if ((bubbleColor & color) == 0) {
            bubbleColor = bubbleColor | color;
        }
        else {
            bubbleColor = bubbleColor & color;
        }
        
        PlayColorizeSound();
        UpdateComponentColor();
    }

    private void UpdateComponentColor()
    {
        var newColor = Color.white;
        // Set the color of the bubble
        switch (bubbleColor)
        {
            case BubbleColor.White:
                newColor = Color.white;
                break;
            case BubbleColor.Red:
                newColor = Color.red;
                break;
            case BubbleColor.Blue:
                newColor = Color.blue;
                break;
            case BubbleColor.Yellow:
                newColor = Color.yellow;
                break;
            case BubbleColor.Green:
                newColor = Color.green;
                break;
            case BubbleColor.Purple:
                newColor = new Color(0.5f, 0, 0.5f);
                break;
            case BubbleColor.Orange:
                newColor = new Color(1, 0.5f, 0);
                break;
            case BubbleColor.Brown:
                newColor = new Color(0.5f, 0.25f, 0);
                break;
            case BubbleColor.Black:
                newColor = Color.black;
                break;
        }
        
        // Get the Renderer component from the GameObject
        Renderer objectRenderer = GetComponent<Renderer>();

        // Check if the GameObject has a Renderer component
        if (objectRenderer != null)
        {
            // Change the color of the material
            objectRenderer.material.color = newColor;
        }
        else
        {
            Debug.LogWarning("No Renderer found on the GameObject!");
        }
    }
    
    private void PlayColorizeSound()
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
    
    public void UseShot()
    {
        shots--;
        if (shots <= 0)
        {
            Destroy(gameObject);
            // FIXME: inform ammo demo that the bubble is destroyed
        }
    }
    
    public void SetBeltIndex(int index)
    {
        beltIndex = index;
    }
    
    public int GetBeltIndex()
    {
        return beltIndex;
    }
}
