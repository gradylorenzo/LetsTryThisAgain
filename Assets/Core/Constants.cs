using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class Constants
    {
        public const int autosaveInterval = 600; //Autosave every ten minutes
        public const int maxCredits = 999999999; //Max credits is one billion
        public const int maxSkillpoints = 999999999; //Max SP is one billion
        public const string savesLocation = "Saves";
        public const float FloatingOriginUpdateThreshold = 10; //How far can the player move from the origin before the floating origin correction is applied?
        public const string ShipPrefabLocation = "PREFABS/SHIPS";
        public const string WeaponPrefabLocation = "PREFABS/WEAPONS";
    }
}