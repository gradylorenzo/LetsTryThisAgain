using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using Core;
using Core.Notify;

[System.Serializable]
public struct GameData
{
    //Game data contains data that is read-write data. 
    #region Fields
    public PlayerData player;
    public SkillData skills;
    public StandingsData standings;
    public SettingsData settings;
    #endregion

    #region Constructors
    public GameData(PlayerData pl, SkillData sk, StandingsData st, SettingsData se)
    {
        player = pl;
        skills = sk;
        standings = st;
        settings = se;
    }
    #endregion

    #region Public IO methods
    public static GameData Create(GameData defaultgd, string name, int sex)
    {
        GameData gd = defaultgd;
        gd.player.name = name;
        gd.player.sex = sex;
        gd.skills.list = Libraries.skills;
        SERIALIZE(gd);
        return gd;
    }
    public static void Save(GameData gd)
    {
        SERIALIZE(gd);
    }
    public static GameData Load(string name)
    {
        GameData gd = new GameData();
        gd = DESERIALIZE(name);
        return gd;
    }
    #endregion

    #region Public gameplay methods
    public void AddCredits(int amount)
    {
        if (amount >= 0)
        {
            player.credits = (int)Mathf.Clamp(player.credits + amount, 0, Constants.maxCredits);
        }
        else
        {
            Notify.Log(Notify.Intent.Error, "Cannot add a negative value to credits.\nUse GameData.SpendCredits() instead");
        }
    }
    public bool SpendCredits(int amount)
    {
        bool b = false;
        if (amount <= 0)
        {
            if (player.credits > amount)
            {
                player.credits -= amount;
                b = true;
            }
            else
            {
                Notify.Log(Notify.Intent.Warning, "Not have enough credits");
            }
        }
        return b;
    }

    public void AddSkillpoints(int amount)
    {
        if (amount >= 0)
        {
            player.skillpoints = (int)Mathf.Clamp(player.skillpoints + amount, 0, Constants.maxSkillpoints);
        }
    }
    public bool ApplySkillpoints(string skillID, int amount)
    {
        bool b = false;
        if (amount <= 0)
        {
            //Does the player have the skill?
            for (int i = 0; i < skills.list.Length; i++)
            {
                if (skills.list[i].id == skillID)
                {
                    if (player.skillpoints > amount)
                    {
                        player.skillpoints -= amount;
                        skills.list[i].currentSP += amount;
                        skills.list[i].currentSP = Mathf.Clamp(skills.list[i].currentSP, 0, 10000);
                        Notify.Log(Notify.Intent.Success, (amount + " skillpoints applied to " + skills.list[i].name));
                        b = true;
                    }
                    else
                    {
                        Notify.Log(Notify.Intent.Warning, "Not enough skillpoints");
                    }
                }
            }


        }
        return b;
    }
    #endregion

    #region Private methods
    private static void SERIALIZE(GameData gd)
    {
        XmlSerializer xs = new XmlSerializer(typeof(GameData));
        TextWriter tw = new StreamWriter(gd.player.name + ".dat");
        xs.Serialize(tw, gd);
        tw.Close();
        Notify.Log(Notify.Intent.Success, "GameData serialized for " + gd.player.name + ".dat");
    }
    private static GameData DESERIALIZE(string name)
    {
        GameData tmp = new GameData();
        GameData gd = new GameData();
        XmlSerializer xs = new XmlSerializer(typeof(GameData));
        TextReader tr = new StreamReader(name + ".dat");
        try
        {
            tmp = (GameData)xs.Deserialize(tr);
        }
        catch (ApplicationException e)
        {
            Notify.Log(Notify.Intent.Error, "Failed to deserialize " + name + ", see Debug.Log");
            Debug.Log(e.InnerException);
            tr.Close();
            return gd;
        }
        finally
        {
            Notify.Log(Notify.Intent.Success, "Successfully deserialized " + name + "!");
            tr.Close();
            gd = tmp;
        }

        return gd;
    }
    #endregion

    #region static data
    public static readonly Dictionary<int, string> Sexes = new Dictionary<int, string> {{0, "Male"}, {1, "Female"}};
    #endregion
}

[System.Serializable]
public struct PlayerData
{
    public string name;
    public int sex;
    public long credits;
    public long skillpoints;
    public int time;
    //Constructors
    public PlayerData(string n, long c, long s, int t, int x)
    {
        name = n;
        sex = x;
        credits = c;
        skillpoints = s;
        time = t;
    }
}

[System.Serializable]
public struct SkillData
{
    public Skill[] list;
}

[System.Serializable]
public struct StandingsData
{
    //This will hold data in reference to where the player stands with various factions.
    //These numbers will range from -1.0 to +1.0.
}

[System.Serializable]
public struct SettingsData
{
    //Volume Settings
    public float sfxVolume;
    public float musicVolume;
    public float dialogVolume;


    //Constructors
    public SettingsData(float s, float m, float d)
    {
        sfxVolume = s;
        musicVolume = m;
        dialogVolume = d;
    }
}

[System.Serializable]
public struct Skill
{
    public string name;
    public string id;
    public int currentSP;
    public int multiplier;
    public int currentTier()
    {
        int t = 0;
        if (currentSP > 0)
        {
            float sp = currentSP / multiplier;
            t = (int)(Mathf.Log10(sp) + 1);
        }
        return t;
    }

    public Skill(string i, string n, int c, int m)
    {
        id = i;
        name = n;
        currentSP = c;
        multiplier = m;
    }
}