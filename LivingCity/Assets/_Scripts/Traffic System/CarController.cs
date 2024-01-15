using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{

    Rigidbody rb;

    [SerializeField]
    private float power;
    [SerializeField]
    private float torque;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private Vector2 movementVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 movementInput)
    {
        this.movementVector = movementInput;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(movementVector.y * transform.forward * power);
        }

        rb.AddTorque(movementVector.x * Vector3.up * torque * movementVector.y);
    }
}
