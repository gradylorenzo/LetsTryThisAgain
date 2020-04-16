using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

[RequireComponent(typeof(Rigidbody))]
public class ShipDynamics : MonoBehaviour
{
    public float power;
    public float torque;
    public Rigidbody rb
    {
        get
        {
            return GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        CheckForOffset();
    }

    private void Move(Vector3 direction)
    {
        rb.AddRelativeForce(direction * power);
    }

    private void Rotate(float input)
    {
        rb.AddRelativeTorque(Vector3.up * input * torque);
    }

    private void CheckForOffset()
    {
        if(Vector3.Distance(rb.position, Vector3.zero) >= Constants.FloatingOriginUpdateThreshold)
        {
            Vector3 rbVelocity = rb.velocity;
            Vector3 rbSpin = rb.angularVelocity;

            Events.EUpdateFloatingOrigin(rb.position);

        }
    }
}
