using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Core.Data;
using System.Xml.Serialization;

namespace Core.IO
{
    //Don't fucking touch this shit.
    public static class IO
    {
        public static bool SaveExists(string name)
        {
            if (Directory.Exists(Constants.savesLocation))
            {
                if(File.Exists(Constants.savesLocation + "/" + name))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static FileInfo[] getFileList()
        {
            List<FileInfo> newList = new List<FileInfo>();

            string[] files = Directory.GetFiles(Constants.savesLocation);
            
            foreach(string s in files)
            {
                FileInfo newInfo = new FileInfo(s, File.GetLastWriteTime(Constants.savesLocation + "/" + s).ToString());
                newList.Add(newInfo);
            }

            return newList.ToArray();
        }

        public static void Serialize(GameData gd)
        {
            //Make sure Saves directory exists. If it does not, create it.
            if (!Directory.Exists(Constants.savesLocation))
            {
                Directory.CreateDirectory(Constants.savesLocation);
            }

            XmlSerializer xs = new XmlSerializer(typeof(GameData));
            TextWriter tw = new StreamWriter(Constants.savesLocation + "/" + gd.playerData.name);
            xs.Serialize(tw, gd);
            tw.Close();
            Notify.Notify.Success("Successfuly saved GameData to " + Constants.savesLocation + "/" + gd.playerData.name);
        }

        public static GameData Deserialize(string name)
        {
            GameData newGD = new GameData();
            GameData tmp = new GameData();
            XmlSerializer xs = new XmlSerializer(typeof(GameData));
            TextReader tr = new StreamReader(Constants.savesLocation + "/" + name + "dat");
            try
            {
                tmp = (GameData)xs.Deserialize(tr);
            }
            catch (ApplicationException e)
            {
                Notify.Notify.Error("Failed to deserialize " + name + " , see console for more details");
                Debug.LogError(e.InnerException);
                tr.Close();
            }
            finally
            {
                Notify.Notify.Success("Successfully deserialized " + name);
                tr.Close();
                newGD = tmp;
            }

            return newGD;
        }
    }

    public struct FileInfo
    {
        public string name;
        public string date;

        public FileInfo(string n, string d)
        {
            name = n;
            date = d;
        }
    }
}