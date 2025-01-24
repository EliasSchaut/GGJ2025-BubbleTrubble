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
        // Set the color of the bubble
        switch (bubbleColor)
        {
            case BubbleColor.White:
                GetComponent<Material>().color = Color.white;
                break;
            case BubbleColor.Red:
                GetComponent<Material>().color = Color.red;
                break;
            case BubbleColor.Blue:
                GetComponent<Material>().color = Color.blue;
                break;
            case BubbleColor.Yellow:
                GetComponent<Material>().color = Color.yellow;
                break;
            case BubbleColor.Green:
                GetComponent<Material>().color = Color.green;
                break;
            case BubbleColor.Purple:
                GetComponent<Material>().color = new Color(0.5f, 0, 0.5f);
                break;
            case BubbleColor.Orange:
                GetComponent<Material>().color = new Color(1, 0.5f, 0);
                break;
            case BubbleColor.Brown:
                GetComponent<Material>().color = new Color(0.5f, 0.25f, 0);
                break;
            case BubbleColor.Black:
                GetComponent<Material>().color = Color.black;
                break;
        }
    }
}
