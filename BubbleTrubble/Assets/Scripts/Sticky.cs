using System;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    [SerializeField] private float timeToLive = 5;
    [SerializeField] private float minGrowFactor = 0.25f;
    [SerializeField] private float maxGrowFactor = 1.50f;
    
    private Enemy enemy = null;
    private BubbleColor bubbleColor = BubbleColor.White;
    private float timer = 0;

    public BubbleColor BubbleColor => bubbleColor;
    
    public void Initialize(Enemy enemy, BubbleColor bubbleColor)
    {
        BubbleColors.SetObjectColor(gameObject, bubbleColor);
        
        this.enemy = enemy;
        this.bubbleColor = bubbleColor;
    }
    
    private void Update()
    {
        timer += Time.deltaTime;
        
        UpdateGrowth();

        if (timer < timeToLive) 
            return;

        enemy.OnStickyDestroyed(this);
        Destroy(gameObject);
    }

    private void UpdateGrowth()
    {
        float growth = Mathf.Clamp01(timer / timeToLive);
        float scale = Mathf.Lerp(minGrowFactor, maxGrowFactor, growth);
        
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
