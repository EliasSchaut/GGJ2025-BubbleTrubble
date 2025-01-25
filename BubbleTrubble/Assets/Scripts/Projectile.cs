using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1000;
    [SerializeField] private float timeToLive = 5;
    
    [SerializeField] private Rigidbody rb;
    
    public BubbleColor Color { get; private set; }

    private float timer;
    
    private void Start()
    {
        rb.AddForce(transform.forward * speed);
    }
    
    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= timeToLive)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(BubbleColor bubbleColor)
    {
        BubbleColors.SetObjectColor(gameObject, bubbleColor);
        
        Color = bubbleColor;
    }
}
