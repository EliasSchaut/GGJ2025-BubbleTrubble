using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyPart[] parts;
    [SerializeField] private GameObject stickyPrefab;
    [SerializeField] private int selfWorth = 15;

    private static readonly Vector3 direction = new(0f, 0f, -1f);

    private List<Sticky> stickies = new();
    private Dictionary<BubbleColor, List<EnemyPart>> partsByColor = new();

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
        transform.position += direction * 0.5f * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Window"))
        {
            Destroy(collision.gameObject);
            
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

            if (!partsByColor.TryGetValue(bubbleColor, out List<EnemyPart> list) || list.Count <= 0) continue;

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
