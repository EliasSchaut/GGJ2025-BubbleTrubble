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

    public void Interact(Player player)
    {
        if (!player.HoldsBubble()) return;
        GameObject bubble = player.GetBubble();
        player.SetBubble(null);
        Bubble bubbleComponent = bubble.GetComponent<Bubble>();
        if (beltGrid.DestroyIfCollidingBubbles(bubbleComponent)) return;
        
        PlaceBubbleOnBeld(bubbleComponent);
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
