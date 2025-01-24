using UnityEngine;

public class Bubble : MonoBehaviour
{
    private BubbleColor bubbleColor;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbleColor = BubbleColor.White;
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
        if (color == BubbleColor.White || color == bubbleColor)
        {
            return;
        }

        if (color == BubbleColor.Black || color == BubbleColor.Brown)
        {
            bubbleColor = color;
            UpdateComponentColor();
            return;
        }

        switch (bubbleColor)
        {
            case BubbleColor.White:
                bubbleColor = color;
                break;
            case BubbleColor.Red:
                if (color == BubbleColor.Yellow) {
                    bubbleColor = BubbleColor.Orange;
                } else if (color == BubbleColor.Blue) {
                    bubbleColor = BubbleColor.Purple;
                } else if (color == BubbleColor.Orange || color == BubbleColor.Purple) {
                } else {
                    bubbleColor = BubbleColor.Brown;
                }
                break;
            case BubbleColor.Yellow:
                if (color == BubbleColor.Red) {
                    bubbleColor = BubbleColor.Orange;
                } else if (color == BubbleColor.Blue) {
                    bubbleColor = BubbleColor.Green;
                } else if (color == BubbleColor.Orange || color == BubbleColor.Green) {
                } else {
                    bubbleColor = BubbleColor.Brown;
                }
                break;
            case BubbleColor.Blue:
                if (color == BubbleColor.Red) {
                    bubbleColor = BubbleColor.Purple;
                } else if (color == BubbleColor.Yellow) {
                    bubbleColor = BubbleColor.Green;
                } else if (color == BubbleColor.Purple || color == BubbleColor.Green) {
                } else {
                    bubbleColor = BubbleColor.Brown;
                }
                break;
            case BubbleColor.Orange:
                if (color == BubbleColor.Red || color == BubbleColor.Yellow) {
                } else {
                    bubbleColor = BubbleColor.Brown;
                }
                break;
            case BubbleColor.Green:
                if (color == BubbleColor.Blue || color == BubbleColor.Yellow) {
                } else {
                    bubbleColor = BubbleColor.Brown;
                }
                break;
            case BubbleColor.Purple:
                if (color == BubbleColor.Red || color == BubbleColor.Blue) {
                } else {
                    bubbleColor = BubbleColor.Brown;
                }
                break;
        }
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
}
