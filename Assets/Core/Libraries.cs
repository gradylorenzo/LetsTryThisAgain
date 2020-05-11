using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Core.Data;
using Core.Notify;

namespace Core.Data
{
    public static class Libraries
    {
        #region Skills
        [System.Serializable]
        public struct Skill
        {
            #region Fields
            public string name;
            public int multiplier;
            #endregion

            #region Constructors
            public Skill(string n, int m)
            {
                name = n;
                multiplier = m;
            }
            #endregion
        }

        /// <summary>
        /// Skills library contains readonly data for all available skills.
        /// </summary>
        public static readonly Dictionary<string, Skill> SkillLibrary = new Dictionary<string, Skill>
        {
            {"ship_management", new Skill("Ship Management", 10)},
            {"combat_ships", new Skill("Combat Ships", 10)},
            {"mining_ships", new Skill("Mining Ships",10)},
            {"transport_ships", new Skill("Transport Ships", 10)},
        };

        /// <summary>
        /// Returns the name of a given skill if the Skills library contains a value for the specified key.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetSkillName(string id)
        {
            string n = null;
            if (SkillLibrary.ContainsKey(id))
            {
                n = SkillLibrary[id].name;
            }
            return n;
        }

        /// <summary>
        /// Returns the multiplier value of a given skill if the Skills library contains a value for the specified key.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetSkillMultiplier(string id)
        {
            int m = -1;
            if (SkillLibrary.ContainsKey(id))
            {
                m = SkillLibrary[id].multiplier;
            }
            return m;
        }
        #endregion
        #region Ships
        [System.Serializable]
        public struct Ship
        {
            public string name;
            public string prefab;

            public Ship(string n, string p)
            {
                name = n;
                prefab = p;
            }
        }
        public readonly static Dictionary<string, Ship> ShipLibrary = new Dictionary<string, Ship>
        {
            {"thrush", new Ship("Thush", "thrush")}
        };
        public static GameObject GetShipPrefab(string id)
        {
            GameObject newShip;
            string shipPrefab = ShipLibrary[id].prefab;
            newShip = (GameObject)Resources.Load(Constants.ShipPrefabLocation + shipPrefab);

            return newShip;
        }
        #endregion
        #region Equipment
        [System.Serializable]
        public struct Equipment
        {
            public WeaponSize size;
            public string prefab;

            public Equipment(WeaponSize s, string p)
            {
                size = s;
                prefab = p;
            }
        }
        public static readonly Dictionary<string, Equipment> PrimaryWeaponLibrary = new Dictionary<string, Equipment>
        {
            {"small_cannon", new Equipment(WeaponSize.Small, "cannon")},
            {"medium_cannon", new Equipment(WeaponSize.Medium, "cannon")}
        };
        public static readonly Dictionary<string, Equipment> SecondaryWeaponLibrary = new Dictionary<string, Equipment>
        {

        };
        public static GameObject GetPrimaryWeaponPrefab(string id)
        {
            if (id != "" && id != null)
            {
                if (PrimaryWeaponLibrary.ContainsKey(id))
                {
                    GameObject newWeapon = new GameObject();
                    string weaponPrefab = PrimaryWeaponLibrary[id].prefab;
                    try
                    {
                        newWeapon = (GameObject)Resources.Load(Constants.WeaponPrefabLocation + weaponPrefab);
                    }
                    catch
                    {
                        Notify.Notify.Error("No prefab found with name '" + id + "'");
                    }
                    return newWeapon;
                }
                else
                {
                    Notify.Notify.Error("No definition for '" + id + "' found in PrimaryWeaponLibrary");
                }
            }
            return null;
        }
        public static GameObject GetSecondaryWeaponPrefab(string id)
        {
            if (id != "" && id != null)
            {
                if (SecondaryWeaponLibrary.ContainsKey(id))
                {
                    GameObject newWeapon = new GameObject();
                    string weaponPrefab = SecondaryWeaponLibrary[id].prefab;
                    try
                    {
                        newWeapon = (GameObject)Resources.Load(Constants.WeaponPrefabLocation + weaponPrefab);
                    }
                    catch
                    {
                        Notify.Notify.Error("No prefab found with name '" + id + "'");
                    }
                    return newWeapon;
                }
                else
                {
                    Notify.Notify.Error("No definition for '" + id + "' found in SecondaryWeaponLibrary");
                }
            }
            return null;
        }
        #endregion
    }
}


