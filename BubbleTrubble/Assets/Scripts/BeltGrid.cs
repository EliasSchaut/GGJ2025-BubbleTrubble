using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeltGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int[] beltCorners;
    [SerializeField] private GameObject bubbleManagerPrefab;
    [SerializeField] private GameObject beltPrefab;
    [SerializeField] private GameObject bubbleDispenserPrefab;
    [SerializeField] private GameObject bubbleSinkPrefab;
    [SerializeField] private float bubbleSpawnTime = 2.0f;
    [SerializeField] private float beltSpeed = 1.0f;
    private List<Vector2Int> segmentMovementDirection = new List<Vector2Int>();
    private BubbleManager bubbleManager;
    private GameObject bubbleDispenser;
    private BubbleDispenser bubbleDispenserComponent;
    private float spawnCounter;

    void Start()
    {
        if (beltCorners.Length < 2)
        {
            throw new System.Exception("BeltGrid must have at least 2 corners");
        }
        
        bubbleManager = Instantiate(bubbleManagerPrefab).GetComponent<BubbleManager>();
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
                GameObject belt = InstantiateOnGrid(beltPrefab, position);
                Belt beltComponent = belt.GetComponent<Belt>();
                beltComponent.SetBeltGrid(this);
                beltComponent.SetGridPosition(position);
                beltComponent.SetSegmentIndex(i - 1);
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
        foreach (Bubble bubble in bubbleManager.GetAll())
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
                    bubble.SetState(BubbleState.OnSink);
                    continue;
                }

                SnapBubbleToGrid(bubble.gameObject, nextCorner);
                bubble.SetBeltIndex(bubbleSegment + 1);
            }
        }

        bubbleManager.DestroyQueued();
    }

    void MoveBubble(Bubble bubble, Vector2Int movementDirection)
    {
        bubble.transform.position += GridVectorToWorld(Time.deltaTime * beltSpeed * (Vector2)movementDirection);
    }

    public void SnapBubbleToGrid(GameObject bubble, Vector2Int gridPosition)
    {
        bubble.transform.position = new Vector3(
            gridPosition.x,
            bubble.transform.position.y,
            gridPosition.y
        );
    }

    void SpawnBubble()
    {
        Bubble newBubble = bubbleDispenserComponent.SpawnBubble();
        bubbleManager.Add(newBubble);
    }

    public GameObject InstantiateOnGrid(GameObject prefab, Vector2Int position)
    {
        return Instantiate(prefab, GridVectorToWorld(position), Quaternion.identity);
    }

    public Vector2Int GetBeltCornerAtEndOfSegment(int segment)
    {
        return beltCorners[segment + 1];
    }

    public Vector3 GridVectorToWorld(Vector2 gridVector)
    {
        return new Vector3(gridVector.x, 0.0f, gridVector.y);
    }

    public Vector2Int SnapWorldToGrid(Vector3 worldPosition)
    {
        return new Vector2Int(
            Mathf.RoundToInt(worldPosition.x),
            Mathf.RoundToInt(worldPosition.z)
        );
    }

    public bool IsLastSegment(int segmentIndex)
    {
        return segmentIndex == segmentMovementDirection.Count() - 1;
    }

    public bool HasBubbleReachedCorner(Bubble bubble, Vector2Int corner)
    {
        return Mathf.Abs(bubble.transform.position.x - corner.x) < 0.05f
               && Mathf.Abs(bubble.transform.position.z - corner.y) < 0.05f;
    }

    public bool IsBubbleOnBelt(Bubble bubble)
    {
        return bubble.GetState() == BubbleState.OnBelt;
    }
}