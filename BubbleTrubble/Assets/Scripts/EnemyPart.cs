using System;
using UnityEngine;

public class EnemyPart : MonoBehaviour
{
    [SerializeField] private BubbleColor bubbleColor;
    
    public BubbleColor BubbleColor => bubbleColor;
    
    private void Start()
    {
        BubbleColors.SetObjectColor(gameObject, bubbleColor);
    }

    public void SetGray(Boolean gray)
    {
        BubbleColors.SetObjectColor(gameObject, bubbleColor, gray);
    }
}
