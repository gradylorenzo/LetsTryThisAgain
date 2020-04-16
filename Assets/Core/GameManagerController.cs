using UnityEngine;
using System.Collections;
using Core;
using Core.Data;
using Core.Notify;
using Core.IO;
using UnityEngine.SceneManagement;

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

    /// <summary>
    /// Methods for handling game data. Saving, loading, unloaded, all handled here.
    /// </summary>
    #region GameData handling
    public static bool loaded { get; private set; } = false;
    public static GameData gameData;
    public static SettingsData settings;

    public static void LoadGame(string name)
    {
        if (!loaded)
        {
            gameData = GameData.Load(name);
            loaded = true;
            LoadOpenSpace();
        }
        else
        {
            Notify.Error("A game is already loaded.");
        }
    }

    public static SaveInfo[] GetSaveList()
    {
        return IO.getFileList();
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
            LoadOpenSpace();
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
            LoadMainMenu();
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

    #region Scene management

    private static void LoadOpenSpace()
    {
        PlayerPrefs.SetString("RECENT_SAVE", gameData.playerData.name);
        SceneManager.LoadScene("OpenSpace");
        SceneManager.LoadScene("CommonUI", LoadSceneMode.Additive);
    }

    private static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    #endregion
}
