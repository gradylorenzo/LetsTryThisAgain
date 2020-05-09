using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Modules;

public class MobilityModule : MonoBehaviour
{
    #region Cache
    private CoreModule core;
    private Rigidbody rb;
    private Vector3 direction;
    private float rotation;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(direction);
    }

    public void SetCoreModule(CoreModule c)
    {
        core = c;
    }

    public void SetVectors(Vector3 d, float r)
    {
        direction = new Vector3(0, d.y, d.z);
        rotation = r;
    }
}
