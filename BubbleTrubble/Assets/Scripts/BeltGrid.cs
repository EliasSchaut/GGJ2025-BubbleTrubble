using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BeltGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int[] beltCorners;
    [SerializeField] private GameObject belt;
    [SerializeField] private GameObject bubbleDispenser;
    
    void Start()
    {
        if (beltCorners.Length < 2)
        {
            throw new System.Exception("BeltGrid must have at least 2 corners");
        }
        
        for (int i = 1; i < beltCorners.Length; i++)
        {
            Vector2Int start = beltCorners[i - 1];
            Vector2Int end = beltCorners[i];
            Vector2Int direction = end - start;
            Vector2Int step = new Vector2Int(Math.Sign(direction.x), Math.Sign(direction.y));
            Vector2Int position = start;
            while (position != end)
            {
                GameObject newBelt = Instantiate(belt, new Vector3(position.x, 1.0f, position.y), Quaternion.identity);
                IBelt beltComponent = newBelt.GetComponent<IBelt>();
                beltComponent.MovementDirection = new Vector2(step.x, step.y);
                beltComponent.NextBelt = null;
                position += step;
            }
        }
    }

    void Update()
    {
        
    }
}
