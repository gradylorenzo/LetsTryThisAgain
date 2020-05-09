using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data;

namespace Core.Modules
{
    public class HardpointModule : MonoBehaviour
    {
        public HardpointConfig config;
        public Mesh placeholderMesh;

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
            Gizmos.DrawMesh(placeholderMesh, transform.position, transform.rotation, Vector3.one * (int)config.size);
        }
    }

    [System.Serializable]
    public class HardpointConfig
    {
        public WeaponSize size = WeaponSize.Small;
        [Range(0.0f, 180.0f)]
        public float minBearing = 45.0f;
        [Range(0.0f, 180.0f)]
        public float maxBearing = 45.0f;
        [Range(0.0f, 90.0f)]
        public float maxAzimuth = 45.0f;
        [Range(0.0f, 90.0f)]
        public float minAzimuth = 45.0f;
        public bool limitBearing = true;
    }
}
