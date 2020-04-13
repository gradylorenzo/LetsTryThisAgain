using UnityEngine;
using System.Collections;
using Core;
using Core.Data;
using Core.Notify;
using Core.IO;

public class GameManagerController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        GameManager.Tick(Time.time);        
    }
}

public static class GameManager
{
    #region General Routines
    public static float Time;
    public static float timeOfLastAutosave = 0;

    /// <summary>
    /// General tick method for periodic repeated actions.
    /// </summary>
    /// <param name="t"></param>
    public static void Tick(float t)
    {
        if (loaded)
        {
            Time = t;

            //If Constants.AutosaveInterval seconds has passed since the last autosave, autosave.
            if (timeOfLastAutosave + Constants.autosaveInterval < Time)
            {
                SaveGame();
                timeOfLastAutosave = Time;
            }
        }
    }
    #endregion

    #region GameData handling
    public static bool loaded { get; private set; } = false;
    public static GameData gameData;
    public static SettingsData settings;

    public static void InitializeGame()
    {

    }

    public static void LoadGame(string name)
    {
        if (!loaded)
        {
            gameData = GameData.Load(name);
            loaded = true;
        }
        else
        {
            Notify.Error("A game is already loaded.");
        }
    }

    public static void SaveGame()
    {
        if (loaded)
        {
            GameData.Save(gameData);
        }
        else
        {
            Notify.Error("Cannot save game. No game loaded.");
        }
    }

    public static void NewGame(string name, int sex, int career)
    {
        if (!loaded)
        {
            gameData = GameData.StartNewGame(name, sex, career);
            loaded = true;
        }
        else
        {
            Notify.Error("Cannot start save, save already loaded.");
        }
    }

    public static void UnloadGame()
    {
        if (loaded)
        {
            loaded = false;
        }
        else
        {
            Notify.Error("Cannot unload save. No save loaded. You bad. Stop hacking.");
        }
    }

    public static bool SaveAlreadyExists(string n)
    {
        return IO.SaveExists(n);
    }
    #endregion

    #region IO Wrapper

    #endregion
}
