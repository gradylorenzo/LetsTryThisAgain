using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.IO;
using Core.Notify;

namespace Core.Data
{
    public struct GameData
    {
        #region Fields
        public PlayerData playerData;
        public SkillsData skillsData;
        #endregion

        #region Internal methods
        #region PlayerData wrapper
        /// <summary>
        /// Adds a specific amount of time in seconds to the game clock
        /// </summary>
        /// <param name="t"></param>
        internal void StepTime(int time)
        {
            playerData.time += time;
        }

        /// <summary>
        /// Adds a specified amount of credits to the player's credit balance
        /// </summary>
        /// <param name="amount"></param>
        internal void AddCredits(int amount)
        {
            playerData.credits = Mathf.Clamp(playerData.credits + amount, 0, Constants.maxCredits);
        }

        /// <summary>
        /// Returns true if the player has more than the specific amount of credits
        /// in their credit balance, and removes that amount from their balance.
        /// Otherwise, returns false.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        internal bool SpendCredits(int amount)
        {
            bool b = false;
            if (playerData.credits > amount)
            {
                playerData.credits = Mathf.Clamp(playerData.credits -= amount, 0, Constants.maxCredits);
                b = true;
            }

            return b;
        }
        #endregion

        #region SkillsData wrapper
        internal void AddSkillpoints(int amount)
        {
            skillsData.unallocatedPoints = Mathf.Clamp(skillsData.unallocatedPoints + amount, 0, Constants.maxSkillpoints);
        }
        internal void ApplySkillpoints(string id, int amount)
        {
            int i = skillsData.GetSkillIndex(id);
            if (i > -1)
            {
                int u = skillsData.unallocatedPoints;
                int r = (skillsData.playerSkills[i].multiplier * 10000) - skillsData.playerSkills[i].points;
                int a = Mathf.Clamp(Mathf.Min(amount, u, r), 0, Constants.maxSkillpoints);

                skillsData.playerSkills[i].points = Mathf.Clamp(skillsData.playerSkills[i].points + a, 0, skillsData.playerSkills[i].multiplier * 10000);
                skillsData.unallocatedPoints = Mathf.Clamp(skillsData.unallocatedPoints - a, 0, Constants.maxSkillpoints);
            }
        }
        internal void InitializeSkills(PlayerSkill[] defaults)
        {
            //Load the Skills Library into the player's skillset.
            List<PlayerSkill> tempSkills = new List<PlayerSkill>();
            foreach(KeyValuePair<string, Libraries.Skill> s in Libraries.SkillLibrary)
            {
                tempSkills.Add(new PlayerSkill(s.Key, 0));
            }

            //Load PlayerSkill[] defaults to set the player up with their first skills.
            foreach(PlayerSkill s in defaults)
            {
                int i = -1;
                for(int t = 0; t < tempSkills.Count; t++)
                {
                    if(tempSkills[t].id == s.id)
                    {
                        i = t;
                    }
                }
                if (i >= 0)
                {
                    tempSkills[i] = new PlayerSkill(tempSkills[i].id, s.points);
                }
                else
                {
                    Notify.Notify.Error("GameData.InitializeSkills() error, check Console");
                    Debug.Log("GameData.InitializeSkills() could not find a skill with the ID of " + s.id + ", did you mispell the ID in GameData.DefaultSkills ? ");
                }
            }

            //Print the results to the screen
            if (true)
            {
                foreach(PlayerSkill s in tempSkills)
                {
                    string str = Libraries.SkillLibrary[s.id].name + " : " + s.points;
                    Notify.Notify.Success(str);
                }
            }
        }
        #endregion

        #region IO wrapper
        internal static void Save(GameData gd)
        {
            IO.IO.Serialize(gd);
        }

        internal static void Load(string name)
        {
            IO.IO.Deserialize(name);
        }
        #endregion
        #endregion
    }

    public struct PlayerData
    {
        #region Fields
        public string name;
        public int sex; //0 = Male, 1 = Female, 3 = Other
        public int credits; //Limited to one billion
        public int time; //Time in seconds since the start of the save.
        #endregion

        #region Constructors
        public PlayerData(string n, int s, long c, int t)
        {
            name = n;
            sex = s;
            credits = (int)Mathf.Clamp(c, 0, Constants.maxCredits);
            time = t;
        }
        #endregion
    }

    public struct SkillsData
    {
        public int unallocatedPoints;
        public PlayerSkill[] playerSkills;
        public int GetSkillIndex (string id)
        {
            int i = -1;
            for(int c = 0; c < playerSkills.Length; c++)
            {
                if(playerSkills[c].id == id)
                {
                    i = c;
                }
            }
            if(i < 0)
            {
                Notify.Notify.Error("Player does not have skill " + id);
            }
            return i;
        }
    }

    public struct PlayerSkill
    {
        public string id;
        public int points;
        public string name
        {
            get
            {
                return Libraries.GetSkillName(id);
            }
        }
        public int multiplier
        {
            get
            {
                return Libraries.GetSkillMultiplier(id);
            }
        }
        public int tier
        {
            get
            {
                int t = 0;
                if(points > 0)
                {
                    float s = points / multiplier;
                    t = (int)(Mathf.Log10(s) + 1);
                }
                return t;
            }
        }

        public PlayerSkill(string i, int p)
        {
            id = i;
            points = p;
        }
    }

    public static class DefaultSkills
    {
        public static PlayerSkill[] strategistSkills = new PlayerSkill[]
        {
            new PlayerSkill("ship_management", 10),
            new PlayerSkill("combat_ships", 10),
        };
        public static PlayerSkill[] industrialistSkills = new PlayerSkill[]
        {
            new PlayerSkill("ship_management", 10),
            new PlayerSkill("mining_ships", 10),
        };
        public static PlayerSkill[] capitalistSkills = new PlayerSkill[]
        {
            new PlayerSkill("ship_management", 10),
            new PlayerSkill("transport_ships", 10),
        };
    }
}
