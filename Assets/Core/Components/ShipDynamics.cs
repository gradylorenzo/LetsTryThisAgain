using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Core.Data;

[RequireComponent(typeof(Rigidbody))]
public class ShipDynamics : MonoBehaviour
{
    public Vector3 power;
    public float torque;
    public Rigidbody rb;
    public bool isLocked { get; private set; }
    public Vector3 clampedVelocity { get; private set; }
    public bool isPlayerShip { get; private set; }
    private Vector3 initialPosition;

    #region MonoBehaviour inherited methods
    private void Awake()
    {
        Events.EUpdateFloatingOrigin += EUpdateFloatingOrigin;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = rb.position;
    }

    private void FixedUpdate()
    {
        if (isPlayerShip)
        {
            CheckForOffset();
        }

        UpdateVelocity();
    }
    #endregion

    /// <summary>
    /// Checks to see if the object's position is further from the origin than the threshold. If
    /// it is, correction is applied.
    /// </summary>
    private void CheckForOffset()
    {
        if(Vector3.Distance(rb.position, Vector3.zero) >= Constants.FloatingOriginUpdateThreshold)
        {
            Vector3 rbVelocity = rb.velocity;
            Vector3 rbSpin = rb.angularVelocity;

            Events.EUpdateFloatingOrigin(rb.position);
        }
    }

    /// <summary>
    /// Update the velocity field with current local velocity
    /// </summary>
    private void UpdateVelocity()
    {
        float x = Mathf.Clamp(transform.InverseTransformDirection(rb.velocity).x, -1, 1);
        float y = Mathf.Clamp(transform.InverseTransformDirection(rb.velocity).y, -1, 1);
        float z = Mathf.Clamp(transform.InverseTransformDirection(rb.velocity).z, -1, 1);
        clampedVelocity = new Vector3(x, y, z);
    }

    #region Public methods
    public void Move(Vector3 direction)
    {
        if (!isLocked)
        {
            float x = direction.x * power.x;
            float y = direction.y * power.y;
            float z = direction.z * power.z;
            rb.AddRelativeForce(new Vector3(x, y, z));
        }
    }

    public void Rotate(float input)
    {
        if (!isLocked)
        {
            rb.AddRelativeTorque(Vector3.up * input * torque);
        }
    }

    public void Lock()
    {
        isLocked = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void Unlock()
    {
        isLocked = false;
    }

    public void setPlayerShip(bool value)
    {
        isPlayerShip = value;
    }

    /// <summary>
    /// Callback for floating origin event.
    /// </summary>
    /// <param name="offset"></param>
    public void EUpdateFloatingOrigin(Vector3 offset)
    {
        if (isPlayerShip)
        {
            rb.MovePosition(Vector3.zero);
        }
        else
        {
            rb.MovePosition(initialPosition - offset);
        }
    }
    #endregion
}
