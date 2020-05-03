using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;

public class CameraRaycast : MonoBehaviour
{
    public GameObject readoutPanel;
    public Text readout;
    public float maxDistance = 500.0f;

    public TurretRotation[] turrets;

    private void FixedUpdate()
    {
        DoForwardCheck();
    }

    private void DoForwardCheck()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            readout.text = hit.transform.name;
            readoutPanel.SetActive(true);
            foreach(TurretRotation tc in turrets)
            {
                tc.SetAimpoint(hit.point);
            }
        }
        else
        {
            readoutPanel.SetActive(false);
            foreach(TurretRotation tc in turrets)
            {
                tc.SetAimpoint(transform.forward * maxDistance);
            }
        }
    }
}
