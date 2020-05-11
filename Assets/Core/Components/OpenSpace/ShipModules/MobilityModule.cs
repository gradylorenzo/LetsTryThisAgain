using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Modules;

public class MobilityModule : MonoBehaviour
{
    #region Cache
    private CoreModule core;
    private Vector3 direction = new Vector3();
    private float rotation = 0.0f;
    #endregion

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        core.rb.AddRelativeForce(direction * core.currentStats.mobility.thrust);
        core.rb.AddRelativeTorque(new Vector3(0, rotation * core.currentStats.mobility.torque, 0));
    }

    public void SetCoreModule(CoreModule c)
    {
        core = c;
    }

    public void SetVectors(Vector3 d, float r)
    {
        direction = d;
        rotation = r;
    }
}
