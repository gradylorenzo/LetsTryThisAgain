using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Core.Modules;

public class InputModule : MonoBehaviour
{
    #region Cache
    private CoreModule core;
    private Rigidbody rb;
    #endregion
    #region MB Methods
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    void FixedUpdate()
    {
        TAKE_INPUTS();
    }
    #endregion
    #region Public Methods
    public void SetCoreModule(CoreModule mod)
    {
        core = mod;
    }
    #endregion
    #region Private Methods
    private void TAKE_INPUTS()
    {
        
        #region CameraZoom
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            core.mainCamera.SendMessage("Zoom", true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            core.mainCamera.SendMessage("Zoom", false);
        }
        #endregion
        #region Firing
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            core.equipmentModule.SendMessage("Fire");
        }
        #endregion
        #region Movement
        core.effectsModule.SetThrusters(Input.GetAxis("VERTICAL"), Input.GetAxis("LONGITUDINAL"));
        rb.AddRelativeForce(0, Input.GetAxis("VERTICAL")*20, Input.GetAxis("LONGITUDINAL")*20);
        rb.AddTorque(0, Input.GetAxis("LATERAL") * 20, 0);
        
        #endregion
    }
    #endregion
}
