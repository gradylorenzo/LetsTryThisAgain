using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTransform : MonoBehaviour
{
    private Vector3 initialPosition;

    private void Awake()
    {
        Events.EUpdateFloatingOrigin += EUpdateFloatingOrigin;
        initialPosition = transform.position;
    }

    public void EUpdateFloatingOrigin(Vector3 offset)
    {
        transform.position -= offset;
    }
}
