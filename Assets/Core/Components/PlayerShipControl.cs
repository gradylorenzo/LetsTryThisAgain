using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipDynamics))]
public class PlayerShipControl : MonoBehaviour
{
    private ShipDynamics sd;

    private void Start()
    {
        sd = GetComponent<ShipDynamics>();
        sd.setPlayerShip(true);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }
    
    private void Move()
    {
        float x = Input.GetAxis("LATERAL");
        float y = Input.GetAxis("VERTICAL");
        float z = Input.GetAxis("LONGITUDINAL");

        sd.Move(new Vector3(x, y, z));
    }

    private void Rotate()
    {
        float input = Input.GetAxis("ROTATE");
        sd.Rotate(input);
    }
}
