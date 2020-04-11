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
        public static readonly Dictionary<string, Skill> Skills = new Dictionary<string, Skill>
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
            if (Skills.ContainsKey(id))
            {
                n = Skills[id].name;
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
            if (Skills.ContainsKey(id))
            {
                m = Skills[id].multiplier;
            }
            return m;
        }
    }
}


