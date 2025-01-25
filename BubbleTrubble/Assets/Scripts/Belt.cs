#nullable enable
using UnityEngine;

public enum BeltType
{
    Straight,
    LeftTurn,
    RightTurn,
}

public class Belt : MonoBehaviour, IInteractable
{
    [SerializeField] private BeltGrid beltGrid;
    public BeltType beltType { get; set; } = BeltType.Straight;
    private Vector2Int gridPosition;
    private int segmentIndex;

    public bool Interact(Player player)
    {
        Debug.Log("interacted with belt " + gridPosition);
        if (!player.HoldsBubble()) return false;
        GameObject bubble = player.GetBubble();
        bubble.transform.parent = transform;
        Bubble bubbleComponent = bubble.GetComponent<Bubble>();
        player.SetBubble(null);
        if (beltGrid.DestroyIfCollidingBubbles(bubbleComponent)) return true;
        
        PlaceBubbleOnBeld(bubbleComponent);
        return true;
    }
    
    public void SetGridPosition(Vector2Int gridPosition)
    {
        this.gridPosition = gridPosition;
    }
    
    public void SetBeltGrid(BeltGrid beltGrid)
    {
        this.beltGrid = beltGrid;
    }
    
    public void SetSegmentIndex(int segmentIndex)
    {
        this.segmentIndex = segmentIndex;
    }
    
    void PlaceBubbleOnBeld(Bubble bubble)
    {
        beltGrid.SnapBubbleToGrid(bubble.gameObject, gridPosition);
        bubble.SetState(BubbleState.OnBelt);
        bubble.SetBeltIndex(segmentIndex);
    }
}
