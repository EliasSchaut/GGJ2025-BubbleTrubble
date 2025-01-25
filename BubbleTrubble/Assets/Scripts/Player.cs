using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private Vector2 inputVector = Vector2.zero;
    
    private Vector3 _rotatedForward = Vector3.forward;
    private Vector3 _rotatedRight = Vector3.right;

    private bool _isActive = false;
    private bool _inTurret = false;
    
    private bool _holdsBubble = false;
    private GameObject bubbleObject;

    private float interactableRange = 3f;
    
    //private CustomInput input = null;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_inTurret && _isActive)
        {
            Debug.Log("inside fixed update2");
            
            moveDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;
            Debug.Log(moveDirection);
            rigidbody.MovePosition(rigidbody.position - moveSpeed * Time.deltaTime * moveDirection);
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    public bool IsPlayerActive()
    {
        return _isActive;
    }

    public void SetPlayerActive(bool active, Transform spawnPoint)
    {
        _isActive = active;
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }

    public bool IsInTurret()
    {
        return _inTurret;
    }

    public void SetInTurret(bool active)
    {
        _inTurret = active;
    }
    
    public bool HoldsBubble()
    {
        return _holdsBubble;
    }

    public void SetBubble(GameObject bubble)
    {
        bubbleObject = bubble;
        bubbleObject.GetComponent<Bubble>().SetState(BubbleState.CarriedByPlayer);
        bubble.transform.parent = transform;
        bubble.transform.localPosition = new Vector3(0, 1.5f, 0);
    }

    public GameObject GetBubble()
    {
        GameObject bubble = bubbleObject;
        bubbleObject = null;
        return bubble;
    }



    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact!");
        
        GameObject[] interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (GameObject interactableObject in interactableObjects)
        {
            float distance = Vector3.Distance(transform.position, interactableObject.transform.position);

            if (distance < interactableRange)
            {
                interactableObject.GetComponent<IInteractable>().Interact(this);
                return;
            }
        }
    }
    
    
    
    
    

    

}
