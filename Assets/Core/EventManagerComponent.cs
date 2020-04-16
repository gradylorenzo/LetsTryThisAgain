using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Notify;

public class EventManagerComponent : MonoBehaviour
{
    public void Awake()
    {
        Events.EUpdateFloatingOrigin += EUpdateFloatingOrigin;
    }

    public void EUpdateFloatingOrigin(Vector3 offset)
    {
        Notify.Event("EUpdateFloatingOrigin");
    }
}

public static class Events
{
    public delegate void UpdateFloatingOrigin(Vector3 offset);
    public static UpdateFloatingOrigin EUpdateFloatingOrigin;
}
