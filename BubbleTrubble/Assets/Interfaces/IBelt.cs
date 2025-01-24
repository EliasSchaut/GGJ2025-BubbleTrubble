#nullable enable
using UnityEngine;

public interface IBelt
{
    public bool IsOccupied { get; set; }
    public IBelt? NextBelt { get; set; }
    public GameObject? Bubble { get; set; }
    public float MovementSpeed { get; set; }
    public Vector2 MovementDirection { get; set; }
    public Vector2 Position { get; }


    void BeldUpdate()
    {
        if (!IsOccupied || NextBelt == null) return;

        if (IsBubbleMovedToNextBelt())
        {
            GiveBubbleToNextBelt();
        }

        if (!NextBelt.IsOccupied)
        {
            MoveBubble();
        }
    }

    bool IsBubbleMovedToNextBelt()
    {
        return Mathf.Approximately(Bubble.transform.position.x, Position.x + MovementDirection.x)
               && Mathf.Approximately(Bubble.transform.position.y, Position.y + MovementDirection.y);
    }

    void GiveBubbleToNextBelt()
    {
        if (IsOccupied)
        {
            IsOccupied = false;
            NextBelt?.ReceiveBubble(Bubble);
        }
    }

    void ReceiveBubble(GameObject newBubble)
    {
        if (IsOccupied)
        {
            throw new System.Exception("Belt is already occupied");
        }

        IsOccupied = true;
        this.Bubble = newBubble;
    }

    void MoveBubble()
    {
        Bubble.transform.position += new Vector3(Time.deltaTime * MovementSpeed * MovementDirection.x, Time.deltaTime * MovementSpeed * MovementDirection.y, .0f);
    }

    GameObject? PickUpBubble()
    {
        if (IsOccupied)
        {
            IsOccupied = false;
            return Bubble;
        }

        return null;
    }
}