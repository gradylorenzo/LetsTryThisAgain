using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Core;
using Core.Modules;

[CustomEditor(typeof(EquipmentModule))]
public class EquipmentModuleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EquipmentModule wc = (EquipmentModule)target;

        DrawDefaultInspector();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("AutoPopulate"))
        {
            wc.AutoPopulate();
        }
        if(GUILayout.Button("Clear Hardpoints"))
        {
            wc.ClearHardpoints();
        }
        EditorGUILayout.EndHorizontal();
    }

    public void OnSceneGUI()
    {
        EquipmentModule wc = (EquipmentModule)target;

        if(wc.hardpoints != null && wc.hardpoints.Length > 0)
        {
            foreach (HardpointModule hp in wc.hardpoints)
            {
                if (hp != null)
                {
                    if (wc.showArcs)
                    {
                        Transform tf = hp.transform;
                        HardpointConfig hc = hp.config;

                        //Draw traversal arcs
                        Handles.color = new Color(1.0f, 0.5f, 0.5f, 0.1f);
                        if (hc.limitBearing)
                        {
                            Handles.DrawSolidArc(tf.position, tf.up, tf.forward, hc.maxBearing, wc.ArcSize);
                            Handles.DrawSolidArc(tf.position, tf.up, tf.forward, -hc.minBearing, wc.ArcSize);
                        }
                        else
                        {
                            Handles.DrawSolidArc(tf.position, tf.up, tf.forward, 360.0f, wc.ArcSize);
                        }

                        //Draw elevation arc
                        Handles.color = new Color(0.5f, 0.5f, 1.0f, 0.1f);
                        Handles.DrawSolidArc(tf.position, tf.right, tf.forward, -hc.maxAzimuth, wc.ArcSize);

                        //Draw depression arc
                        Handles.color = new Color(0.5f, 1.0f, 0.5f, 0.1f);
                        Handles.DrawSolidArc(tf.position, tf.right, tf.forward, hc.minAzimuth, wc.ArcSize);
                    }
                }
            }
        }
    }
}
