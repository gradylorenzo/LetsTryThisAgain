using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Core.Modules;

public class InputModule : MonoBehaviour
{
    #region Cache
    [SerializeField]
    private CoreModule core;
    public Camera mainCamera { get; private set; }
    #endregion
    #region MB Methods
    void Start()
    {  
    }

    private void Update()
    {
        FIND_MAIN_CAMERA();
        TAKE_INPUT();
    }

    void FixedUpdate()
    {
        
    }
    #endregion
    #region Public Methods
    public void SetCoreModule(CoreModule mod)
    {
        core = mod;
    }
    #endregion
    #region Private Methods
    private void TAKE_INPUT()
    {
        #region Ship Movement
        //Ship only rotates on y axis, so only one value is needed, not a whole-ass Vector3, to represent rotation
        Vector3 direction;
        float r;
        direction = new Vector3(0, Input.GetAxis("VERTICAL"), Input.GetAxis("LONGITUDINAL"));
        r = Input.GetAxis("LATERAL");
        core.SetMobilityVectors(direction, r);
        #endregion
    }

    private void FIND_MAIN_CAMERA()
    {
        if (!mainCamera)
        {
            mainCamera = Camera.main;
        }
    }
    #endregion
}
