using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Core.Modules;

public class EffectsModule : MonoBehaviour
{
    public GameObject rearThrusters;
    public GameObject topThrusters;
    public GameObject bottomThrusters;

    #region Cache
    private CoreModule core;
    private Vector3 direction;
    #endregion

    private void Update()
    {
        UPDATE_THRUSTERS();
    }

    public void SetCoreModule(CoreModule mod)
    {
        core = mod;
    }

    public void SetVectors(Vector3 d, float r)
    {
        direction = d;
    }

    private void UPDATE_THRUSTERS()
    {
        rearThrusters.SetActive(direction.z > 0);
        topThrusters.SetActive(direction.y < 0);
        bottomThrusters.SetActive(direction.y > 0);
    }

}
