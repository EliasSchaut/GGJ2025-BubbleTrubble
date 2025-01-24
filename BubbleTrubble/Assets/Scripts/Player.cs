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
    
    //private CustomInput input = null;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movement();
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        
        rigidbody.MovePosition(rigidbody.position + moveSpeed * Time.deltaTime * moveDirection);
    }
    
    private void Movement()
    {
        moveDirection += inputVector.x * _rotatedRight;
        moveDirection += inputVector.y * _rotatedForward;

        rigidbody.MovePosition(rigidbody.position + moveDirection.normalized * (moveSpeed * Time.deltaTime));
    }
    


}
