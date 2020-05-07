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
    #endregion

    public void SetCoreModule(CoreModule mod)
    {
        core = mod;
    }

    public void SetThrusters(float y, float z)
    {
        rearThrusters.SetActive(z > 0);
        topThrusters.SetActive(y < 0);
        bottomThrusters.SetActive(y > 0);
    }
}
