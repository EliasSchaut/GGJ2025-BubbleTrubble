using UnityEngine;

public class Belt : MonoBehaviour
{
    [SerializeField] private GameObject nextBelt;
    private GameObject bubble;
    private Vector3 movementDirection;
    private float movementSpeed = 1.0f;
    private bool isOccupied = false;
    private float epsilon = 0.01f;

    void Update()
    {
        if (!isOccupied || !nextBelt) return;
        
        Belt nextBeltComponent = nextBelt.GetComponent<Belt>();
        if (IsBubbleMovedToNextBelt())
        {
            GiveBubbleToNextBelt(nextBeltComponent);
        }
        
        if (!nextBeltComponent.isOccupied)
        {
            MoveBubble();
        }
    }
    
    bool IsBubbleMovedToNextBelt()
    {
        return (bubble.transform.position.x - nextBelt.transform.position.x) < epsilon 
               && (bubble.transform.position.y - nextBelt.transform.position.y) < epsilon;
    }
    
    void GiveBubbleToNextBelt(Belt nextBelt)
    {
        if (isOccupied)
        {
            isOccupied = false;
            nextBelt.ReceiveBubble(bubble);
        }
    }

    void ReceiveBubble(GameObject newBubble)
    {
        if (isOccupied)
        {
            throw new System.Exception("Belt is already occupied");
        }
        isOccupied = true;
        this.bubble = newBubble;
    }

    void MoveBubble()
    {
        bubble.transform.position += Time.deltaTime * movementSpeed * movementDirection;
    }
    
    GameObject PickUpBubble()
    {
        if (isOccupied)
        {
            isOccupied = false;
            return bubble;
        }

        return null;
    }
}
