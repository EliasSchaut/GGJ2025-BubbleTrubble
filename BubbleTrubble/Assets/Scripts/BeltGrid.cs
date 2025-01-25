using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BeltGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int[] beltCorners;
    [SerializeField] private GameObject beltPrefab;
    [SerializeField] private GameObject bubbleDispenserPrefab;
    [SerializeField] private GameObject bubbleSinkPrefab;
    [SerializeField] private float bubbleSpawnTime = 2.0f;
    [SerializeField] private float beltSpeed = 1.0f;
    private GameObject bubbleDispenser;
    private BubbleDispenser bubbleDispenserComponent;
    private List<Vector2Int> segmentMovementDirection = new List<Vector2Int>();
    private List<Bubble> bubbles = new List<Bubble>();
    private List<Bubble> bubblesToRemove = new List<Bubble>();
    private float spawnCounter;

    void Start()
    {
        if (beltCorners.Length < 2)
        {
            throw new System.Exception("BeltGrid must have at least 2 corners");
        }

        bubbleDispenser = InstantiateOnGrid(bubbleDispenserPrefab, beltCorners.First());
        bubbleDispenserComponent = bubbleDispenser.GetComponent<BubbleDispenser>();
        InstantiateOnGrid(bubbleSinkPrefab, beltCorners.Last());

        for (int i = 1; i < beltCorners.Length; i++)
        {
            Vector2Int start = beltCorners[i - 1];
            Vector2Int end = beltCorners[i];
            Vector2Int direction = end - start;
            Vector2Int step = new Vector2Int(Math.Sign(direction.x), Math.Sign(direction.y));
            Vector2Int position = start;
            segmentMovementDirection.Add(step);
            while (position != end)
            {
                InstantiateOnGrid(beltPrefab, position);
                position += step;
            }
        }
    }

    void Update()
    {
        UpdateBubbleSpawn();
        UpdateBubbleMovement();
    }

    void UpdateBubbleSpawn()
    {
        spawnCounter += Time.deltaTime;
        if (spawnCounter >= bubbleSpawnTime)
        {
            spawnCounter -= bubbleSpawnTime;
            SpawnBubble();
        }
    }

    void UpdateBubbleMovement()
    {
        foreach (Bubble bubble in bubbles)
        {
            if (!IsBubbleOnBelt(bubble)) continue;
            int bubbleSegment = bubble.GetBeltIndex();
            Vector2Int movementDirection = segmentMovementDirection[bubbleSegment];
            MoveBubble(bubble, movementDirection);
            Vector2Int nextCorner = GetBeltCornerAtEndOfSegment(bubbleSegment);

            if (HasBubbleReachedCorner(bubble, nextCorner))
            {
                if (IsLastSegment(bubbleSegment))
                {
                    QueueDestroyBubble(bubble);
                    continue;
                }

                SnapBubbleToCorner(bubble, nextCorner);
                bubble.SetBeltIndex(bubbleSegment + 1);
            }
        }

        DestroyQueuedBubbles();
    }

    void MoveBubble(Bubble bubble, Vector2Int movementDirection)
    {
        bubble.transform.position += GridVectorToWorld(Time.deltaTime * beltSpeed * (Vector2)movementDirection);
    }

    void SnapBubbleToCorner(Bubble bubble, Vector2Int corner)
    {
        bubble.transform.position = new Vector3(
            corner.x,
            bubble.transform.position.y,
            corner.y
        );
    }

    void SpawnBubble()
    {
        Bubble newBubble = bubbleDispenserComponent.SpawnBubble();
        bubbles.Add(newBubble);
    }

    void QueueDestroyBubble(Bubble bubble)
    {
        bubblesToRemove.Add(bubble);
    }

    void DestroyQueuedBubbles()
    {
        foreach (Bubble bubble in bubblesToRemove)
        {
            bubbles.Remove(bubble);
            Destroy(bubble.gameObject);
        }
        bubblesToRemove.Clear();
    }

    GameObject InstantiateOnGrid(GameObject prefab, Vector2Int position)
    {
        return Instantiate(prefab, GridVectorToWorld(position), Quaternion.identity);
    }

    Vector2Int GetBeltCornerAtEndOfSegment(int segment)
    {
        return beltCorners[segment + 1];
    }

    Vector3 GridVectorToWorld(Vector2 gridVector)
    {
        return new Vector3(gridVector.x, 0.0f, gridVector.y);
    }

    bool IsLastSegment(int segmentIndex)
    {
        return segmentIndex == segmentMovementDirection.Count() - 1;
    }

    bool HasBubbleReachedCorner(Bubble bubble, Vector2Int corner)
    {
        return Mathf.Abs(bubble.transform.position.x - corner.x) < 0.05f
               && Mathf.Abs(bubble.transform.position.z - corner.y) < 0.05f;
    }

    bool IsBubbleOnBelt(Bubble bubble)
    {
        return bubble.GetState() == BubbleState.OnBelt;
    }
}