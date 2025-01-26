using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAxis = Vector3.up; // Axis of rotation
    [SerializeField] private float rotationSpeed = 30f; // Speed of rotation in degrees per second
    [SerializeField] private Space rotationSpace = Space.World; // Rotation in world or local space

    private void FixedUpdate()
    {
        // Rotate the object around the specified axis
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, rotationSpace);
    }
}

