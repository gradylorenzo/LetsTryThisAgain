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
}
