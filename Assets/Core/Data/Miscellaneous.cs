using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Core.Data
{
    public struct SaveInfo
    {
        public string name;
        public string date;

        public SaveInfo(string n, string d)
        {
            name = n;
            date = d;
        }
    }

    public enum ShipSizes
    {
        Small,
        Medium,
        Large,
        XLarge
    }

    [System.Serializable]
    public enum WeaponSize
    {
        Utility = 1,
        Small = 1,
        Medium = 2,
        Large = 4,
        XLarge = 8
    }
}
