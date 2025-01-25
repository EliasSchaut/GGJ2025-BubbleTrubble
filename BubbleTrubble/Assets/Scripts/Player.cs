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

    private bool _isActive = false;
    private bool _inTurret = false;
    
    private bool _holdsBubble = false;
    private GameObject bubbleObject;

    private float interactableRange = 3f;

    private TurretController turret;
    private TurretDoor turretDoor;
    private bool InTurret => turret == null;

    private float lastInteractTime;
    private float interactableInhibitTime = 0.2f;
    
    [SerializeField]private Animator animator;
    
    //private CustomInput input = null;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        lastInteractTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isActive)
            return;
        
        if (_inTurret)
        {
            if (turret)
            {
                turret.Move(inputVector);
            
                return;
            }
            
            moveDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;
            
            rigidbody.MovePosition(rigidbody.position - moveSpeed * Time.deltaTime * moveDirection);
        }
        else
        {
            Move();
        }
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

    public void SetInTurret(TurretController turret, TurretDoor turretDoor) 
    {
        this.turret = turret;
        this.turretDoor = turretDoor;
        _inTurret = true;
    }

    private void LeaveTurret()
    {
        turretDoor.Leave();
        _inTurret = false;
        
        turret = null;
        turretDoor = null;
    }
    
    public bool HoldsBubble()
    {
        return _holdsBubble;
    }

    public void SetBubble(GameObject bubble)
    {
        bubbleObject = bubble;
        if (bubble != null) {
            _holdsBubble = true;
            bubbleObject.GetComponent<Bubble>().SetState(BubbleState.CarriedByPlayer);
            bubble.transform.parent = transform;
            bubble.transform.localPosition = new Vector3(0, 1.5f, 0);
        }
        else {
            _holdsBubble = false;
        }
    }

    public GameObject GetBubble()
    {
        return bubbleObject;
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (turret != null)
        {
            OnY();
            
            return;
        }

        if (Time.time - lastInteractTime < interactableInhibitTime) return;

        GameObject[] interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");
        
        Array.Sort(interactableObjects, (a, b) => {
            float distanceA = Vector3.Distance(transform.position, a.transform.position);
            float distanceB = Vector3.Distance(transform.position, b.transform.position);
            return distanceA.CompareTo(distanceB);
        });
        
        foreach (GameObject interactableObject in interactableObjects)
        {
            float distance = Vector3.Distance(transform.position, interactableObject.transform.position);

            if (distance < interactableRange)
            {
                var interacted = interactableObject.GetComponent<IInteractable>().Interact(this);
                if (interacted) return;
            }
        }
    }

    public void OnX()
    {
        if (turret != null)
        {
            turret.X();
        }
    }
    
    public void OnY()
    {
        if (turret != null)
        {
            turret.Y();
        }
    }
    
    public void OnB()
    {
        if (turret != null)
        {
            turret.B();
        }
    }
    
    public void OnA()
    {
        if (turret != null)
        {
            turret.A();
        }
    }

    public void OnFire()
    {
        if (turret != null)
        {
            turret.Fire();
        }
    }

    public void OnExit()
    {
        if (turret != null)
        {
            LeaveTurret();
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }
    
    public void Move()
    {
        if (!_inTurret && _isActive)
        {
            moveDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;

            rigidbody.MovePosition(rigidbody.position - moveSpeed * Time.deltaTime * moveDirection);
                
            // Animation
            if (moveDirection != Vector3.zero)
            {
                animator.SetBool("Walking", true);
                rigidbody.MoveRotation(Quaternion.LookRotation(moveDirection, Vector3.up));
            }
            else
            {
                animator.SetBool("Walking", false);
            }
        }
    }
}
