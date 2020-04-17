using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data;

public class DockingArea : MonoBehaviour
{
    public ShipSizes acceptedSize;
    public Material undockedMaterial;
    public Material dockedMaterial;
    public GameObject dockingPrompt;

    private MeshRenderer mr;
    private bool isDocked = false;
    private Transform ship;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (isDocked)
        {
            mr.material = dockedMaterial;
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (!ship.GetComponent<ShipDynamics>().isLocked)
                {
                    ship.position = transform.position;
                    ship.rotation = transform.rotation;
                    ship.SendMessage("Lock");
                    dockingPrompt.SetActive(false);
                }
                else
                {
                    ship.SendMessage("Unlock");
                }
            }
        }
        else
        {
            mr.material = undockedMaterial;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ship"))
        {
            ShipDynamics sd = other.GetComponent<ShipDynamics>();
            if(sd.isPlayerShip)
            {
                isDocked = true;
                ship = sd.transform;
                dockingPrompt.SetActive(true);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ship"))
        {
            if (other.GetComponent<ShipDynamics>().isPlayerShip)
            {
                isDocked = false;
                ship = null;
                dockingPrompt.SetActive(false);
            }
        }
    }
}
