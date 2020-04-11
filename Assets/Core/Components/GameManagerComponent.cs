using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core;
using Core.Notify;

namespace Core
{
    public class GameManagerComponent : MonoBehaviour
    {
        

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
            SceneManager.LoadScene("CommonUI", LoadSceneMode.Additive);
        }

        private void Update()
        {
            GameManager.GMUpdate(Time.time);
        }
    }

    public static class GameManager
    {
        #region Clock Management
        public static bool Loaded { get; private set; }
        private static int TimeOfLastAutosave = 0;
        private static int currentTime;
        public static GameData data;

        public static void GMUpdate(float t)
        {
            if (Loaded)
            {
                int newTime = (int)t;
                if (newTime > currentTime)
                {
                    data.player.time += newTime - currentTime;
                    currentTime = newTime;
                    GMTick();
                }
                if (currentTime >= TimeOfLastAutosave + Constants.autosaveInterval)
                {
                    TimeOfLastAutosave = currentTime;
                    Autosave();
                }
            }
        }
        private static void GMTick()
        {

        }
        private static void Autosave()
        {
            GameData.Save(data);
        }
        #endregion

        #region IO Wrapper
        public static void Create(GameData defaultgd, string name, int sex)
        {
            data = GameData.Create(defaultgd, name, sex);
            Load(name);
        }
        public static void Load(string name)
        {
            data = GameData.Load(name);
            Loaded = true;
        }
        public static void Save()
        {
            GameData.Save(data);
        }
        #endregion
    }
}