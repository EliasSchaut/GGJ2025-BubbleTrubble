using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    private List<Bubble> bubbles = new List<Bubble>();
    private List<Bubble> bubblesToRemove = new List<Bubble>();
    
    public void Add(Bubble bubble)
    {
        bubbles.Add(bubble);
    }

    public List<Bubble> GetAll()
    {
        return bubbles;
    }

    public void Destroy(Bubble bubble)
    {
        bubbles.Remove(bubble);
        Destroy(bubble.gameObject);
    }

    public void QueueDestroy(Bubble bubble)
    {
        bubblesToRemove.Add(bubble);
    }

    public void DestroyQueued()
    {
        foreach (Bubble bubble in bubblesToRemove)
        {
            Destroy(bubble);
        }

        bubblesToRemove.Clear();
    }
}