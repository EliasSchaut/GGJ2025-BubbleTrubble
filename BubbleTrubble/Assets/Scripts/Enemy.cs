using System;
using System.Collections.Generic;
using UnityEngine;

enum MoveMode
{
    NotAtAll,
    Direct,
    Corkscrew,
    ZigZag,
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyPart[] parts;
    [SerializeField] private GameObject stickyPrefab;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float swerve = 1.0f;
    [SerializeField] private int selfWorth = 15;
    [SerializeField] private MoveMode moveMode = MoveMode.Direct;

    private static readonly Vector3 direction = new Vector3(0f, 1f, 1f).normalized;

    private float timer = 0f;
    
    private readonly List<Sticky> stickies = new();
    private readonly Dictionary<BubbleColor, List<EnemyPart>> partsByColor = new();

    private void Start()
    {
        foreach (EnemyPart part in parts)
        {
            if (!partsByColor.TryGetValue(part.BubbleColor, out List<EnemyPart> list))
            {
                partsByColor[part.BubbleColor] = list = new List<EnemyPart>();
            }
            
            list.Add(part);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (moveMode == MoveMode.NotAtAll)
            return;
        
        Vector3 dir = direction;
        
        switch (moveMode)
        {
            case MoveMode.ZigZag:
                dir += transform.right * Mathf.Sin(timer * swerve);
                break;
            
            case MoveMode.Corkscrew:
                dir += transform.right * Mathf.Sin(timer * swerve);
                dir += transform.up * Mathf.Cos(timer * swerve);
                break;
        }
        
        dir.Normalize();
        
        transform.position += dir * (speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Window"))
        {
            Destroy(gameObject);
            
            GameManager.Instance.LoseLife();
            
            return;
        }
        
        if (collision.contactCount == 0)
            return;
        
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        
        if (projectile == null)
            return;
        
        BubbleColor bubbleColor = projectile.Color;

        Vector3 position = collision.GetContact(0).point;
        Destroy(projectile.gameObject);

        Sticky sticky = Instantiate(stickyPrefab, position, Quaternion.identity).GetComponent<Sticky>();
        sticky.Initialize(this, bubbleColor);
        stickies.Add(sticky);
        
        sticky.gameObject.transform.SetParent(transform);
        
        UpdateStickies();
    }

    public void OnStickyDestroyed(Sticky sticky)
    {
        stickies.Remove(sticky);
        
        UpdateStickies();
    }

    private void UpdateStickies()
    {
        foreach (EnemyPart part in parts)
        {
            part.SetGray(gray: false);
        }
        
        if (stickies.Count == 0)
            return;

        int notStickiedColorCount = partsByColor.Count;
        HashSet<BubbleColor> stickiedColors = new();
        
        foreach (Sticky sticky in stickies)
        {
            BubbleColor bubbleColor = sticky.BubbleColor;
            
            if (!stickiedColors.Add(bubbleColor))
                return;

            if (!partsByColor.TryGetValue(bubbleColor, out List<EnemyPart> list) || list.Count <= 0) 
                continue;

            foreach (EnemyPart part in list)
            {
                part.SetGray(gray: true);
            }
                
            notStickiedColorCount -= 1;
        }
        
        if (notStickiedColorCount == 0)
        {
            Destroy(gameObject);
            
            GameManager.Instance.EnemyKilled(selfWorth);
        }
    }
}
