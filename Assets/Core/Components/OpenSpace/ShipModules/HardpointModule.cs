using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public HardpointSize size = HardpointSize.Small;
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

    [System.Serializable]
    public enum HardpointSize
    {
        Utility = 1,
        Small = 1,
        Medium = 2,
        Large = 4,
        XLarge = 8
    }
}
