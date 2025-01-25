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
        Bubble bubbleComponent = bubble.GetComponent<Bubble>();
        SnapBubbleToBelt(bubbleComponent);
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
    
    void SnapBubbleToBelt(Bubble bubble)
    {
        beltGrid.SnapBubbleToGrid(bubble.gameObject, gridPosition);
        bubble.SetState(BubbleState.OnBelt);
        bubble.SetBeltIndex(segmentIndex);
    }
}
