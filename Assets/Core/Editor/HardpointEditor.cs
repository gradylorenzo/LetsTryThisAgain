using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Hardpoint))]
[CanEditMultipleObjects]
public class HardpointEditor : Editor
{
    private const float ArcSize = 10.0f;

    public override void OnInspectorGUI()
    {
        Hardpoint hp = (Hardpoint)target;

        DrawDefaultInspector();
    }

    private void OnSceneGUI()
    {
        Hardpoint hp = (Hardpoint)target;
        Transform tf = hp.transform;
        HardpointConfig hc = hp.config;

        //Draw traversal arcs
        if (hc.limitTraverse)
        {
            Handles.color = new Color(1.0f, 0.5f, 0.5f, 0.1f);
            Handles.DrawSolidArc(tf.position, tf.up, tf.forward, hc.rightTraverse, ArcSize);
            Handles.DrawSolidArc(tf.position, tf.up, tf.forward, -hc.leftTraverse, ArcSize);
        }
        else
        {
            Handles.DrawSolidArc(tf.position, tf.up, tf.forward, 360.0f, ArcSize);
        }

        //Draw elevation arc
        Handles.color = new Color(0.5f, 0.5f, 1.0f, 0.1f);
        Handles.DrawSolidArc(tf.position, tf.right, tf.forward, -hc.elevation, ArcSize);

        //Draw depression arc
        Handles.color = new Color(0.5f, 1.0f, 0.5f, 0.1f);
        Handles.DrawSolidArc(tf.position, tf.right, tf.forward, hc.depression, ArcSize);
    }
}
