#nullable enable
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    private List<Bubble> bubbles = new List<Bubble>();
    private List<Bubble> bubblesToRemove = new List<Bubble>();
    private Bubble? bubbleOnSink = null;

    public void Add(Bubble bubble)
    {
        bubbles.Add(bubble);
    }

    public List<Bubble> GetAll()
    {
        return bubbles;
    }

    public void QueueDestroy(Bubble bubble)
    {
        bubblesToRemove.Add(bubble);
    }

    public void DestroyQueued()
    {
        foreach (Bubble bubble in bubblesToRemove)
        {
            DestroyBubble(bubble);
        }

        bubblesToRemove.Clear();
    }
    
    public void DestroyBubble(Bubble bubble)
    {
        bubbles.Remove(bubble);
        if (bubble.GetState() == BubbleState.OnSink)
        {
            bubbleOnSink = null;
        }
        
        bubble.DestroyBubble();
    }
    
    public Bubble? PopBubbleOnSink()
    {
        Bubble? bubble = bubbleOnSink;
        bubbleOnSink = null;
        return bubble;
    }
    
    public bool HasBubbleOnSink()
    {
        return bubbleOnSink != null;
    }
    
    public void SetBubbleOnSink(Bubble bubble)
    {
        bubbleOnSink = bubble;
    }
}
