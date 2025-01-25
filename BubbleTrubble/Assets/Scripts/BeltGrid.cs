using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class BeltGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int[] beltCorners;
    [SerializeField] private GameObject beltPrefab;
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private GameObject bubbleDispenserPrefab;
    [SerializeField] private GameObject bubbleSinkPrefab;
    [SerializeField] private float bubbleSpawnRate = 1.0f;
    [SerializeField] private float beltSpeed = 1.0f;
    private GameObject bubbleDispenser;
    private BubbleDispenser bubbleDispenserComponent;
    private Vector2Int[] segmentMovementDirection;
    private GameObject[] bubbles;
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
        if (spawnCounter >= bubbleSpawnRate)
        {
            spawnCounter -= bubbleSpawnRate;
            bubbleDispenserComponent.SpawnBubble();
        }
    }

    void UpdateBubbleMovement()
    {
        foreach (GameObject bubblePrefab in bubbles)
        {
            Bubble bubble = bubblePrefab.GetComponent<Bubble>();
            if (IsBubbleOnBelt(bubble)) continue;
            int bubbleSegment = bubble.GetBeltIndex();
            Vector2Int movementDirection = segmentMovementDirection[bubbleSegment];
            MoveBubble(bubble, movementDirection);
            if (HasBubbleReachedCorner(bubble, GetBeltCornerAtEndOfSegment(bubbleSegment)))
            {
                if (HasBubbleReachedSink(bubble, bubbleSegment))
                {
                    Destroy(bubblePrefab);
                    continue;
                }

                bubble.SetBeltIndex(bubbleSegment + 1);
            }
        }
    }

    void MoveBubble(Bubble bubble, Vector2Int movementDirection)
    {
        bubble.transform.position += GridVectorToWorld(Time.deltaTime * beltSpeed * (Vector2)movementDirection);
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

    bool HasBubbleReachedSink(Bubble bubble, int segmentIndex)
    {
        return segmentIndex == beltCorners.Length - 1 && HasBubbleReachedCorner(bubble, beltCorners.Last());
    }

    bool HasBubbleReachedCorner(Bubble bubble, Vector2Int corner)
    {
        return Mathf.Approximately(bubble.transform.position.x, corner.x) &&
               Mathf.Approximately(bubble.transform.position.z, corner.y);
    }

    bool IsBubbleOnBelt(Bubble bubble)
    {
        return bubble.GetState() != BubbleState.OnBelt;
    }
}