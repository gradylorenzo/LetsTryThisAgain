using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Data
{
    public struct SettingsData
    {
        public VolumeSettings volume;
    }

    public struct VolumeSettings
    {
        public float sfx;
        public float music;
        public float dialog;

    }
}
