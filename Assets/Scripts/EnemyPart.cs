using System;
using UnityEngine;

public class EnemyPart : MonoBehaviour
{
    private BubbleColor bubbleColor;
    
    public BubbleColor BubbleColor
    {
        get => bubbleColor;
        set
        {
            bubbleColor = value;
            BubbleColors.SetObjectColor(gameObject, bubbleColor);
        }
    }

    public void SetGray(Boolean gray)
    {
        BubbleColors.SetObjectColor(gameObject, bubbleColor, gray);
    }
}
