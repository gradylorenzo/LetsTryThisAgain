using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Core;
using Core.Modules;


[CustomEditor(typeof(HardpointModule))]
[CanEditMultipleObjects]
public class HardpointEditor : Editor
{
    private const float ArcSize = 10.0f;

    public override void OnInspectorGUI()
    {
        HardpointModule hp = (HardpointModule)target;

        DrawDefaultInspector();
    }

    private void OnSceneGUI()
    {
        HardpointModule hp = (HardpointModule)target;
        Transform tf = hp.transform;
        HardpointConfig hc = hp.config;

        //Draw traversal arcs
        Handles.color = new Color(1.0f, 0.5f, 0.5f, 0.1f);
        if (hc.limitBearing)
        {
            Handles.DrawSolidArc(tf.position, tf.up, tf.forward, hc.maxBearing, ArcSize);
            Handles.DrawSolidArc(tf.position, tf.up, tf.forward, -hc.minBearing, ArcSize);
        }
        else
        {
            Handles.DrawSolidArc(tf.position, tf.up, tf.forward, 360.0f, ArcSize);
        }

        //Draw elevation arc
        Handles.color = new Color(0.5f, 0.5f, 1.0f, 0.1f);
        Handles.DrawSolidArc(tf.position, tf.right, tf.forward, -hc.maxAzimuth, ArcSize);

        //Draw depression arc
        Handles.color = new Color(0.5f, 1.0f, 0.5f, 0.1f);
        Handles.DrawSolidArc(tf.position, tf.right, tf.forward, hc.minAzimuth, ArcSize);
    }
}
