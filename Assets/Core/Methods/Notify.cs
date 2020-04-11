using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Notify
{
    public static class Notify
    {
        public enum Intent
        {
            Success = 0,
            Message = 1,
            Warning = 2,
            Error = 3
        }
        private static bool useDebugLog = false;

        public static void Log(string text)
        {
            if (useDebugLog)
            {
                Debug.Log(text);
            }

            ENotifyLog(Intent.Message, text);
        }

        public static void Success(string text)
        {
            if (useDebugLog)
            {
                Debug.Log(text);
            }

            ENotifyLog(Intent.Success, text);
        }

        public static void Warning(string text)
        {
            if (useDebugLog)
            {
                Debug.Log(text);
            }

            ENotifyLog(Intent.Warning, text);
        }

        public static void Error(string text)
        {
            if (useDebugLog)
            {
                Debug.Log(text);
            }

            ENotifyLog(Intent.Error, text);
        }

        public delegate void e_NotifyLog(Intent intent, string text);
        public static e_NotifyLog ENotifyLog;
    }
}
