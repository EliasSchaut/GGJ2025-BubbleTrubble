using UnityEngine;

public static class BubbleColors
{
    public static void SetObjectColor(GameObject gameObject, BubbleColor bubbleColor)
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
        Renderer objectRenderer = gameObject.GetComponent<Renderer>();

        // Check if the GameObject has a Renderer component
        if (objectRenderer == null)
        {
            objectRenderer = gameObject.GetComponentInChildren<Renderer>();
        }
        
        if (objectRenderer != null)
        {
            // Change the color of the material
            objectRenderer.material.color = newColor;
        }
    }
}
