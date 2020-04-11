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
        public static void Serialize(Data.GameData gd)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Data.GameData));
            TextWriter tw = new StreamWriter(Constants.savesLocation + gd.playerData.name + ".dat");
            xs.Serialize(tw, gd);
            tw.Close();
            Notify.Notify.Success("Successfuly saved GameData to " + Constants.savesLocation + gd.playerData.name + ".dat");
        }

        public static Data.GameData Deserialize(string name)
        {
            Data.GameData newGD = new Data.GameData();
            Data.GameData tmp = new Data.GameData();
            XmlSerializer xs = new XmlSerializer(typeof(Data.GameData));
            TextReader tr = new StreamReader(Constants.savesLocation + name + "dat");


            try
            {
                tmp = (Data.GameData)xs.Deserialize(tr);
            }
            catch (ApplicationException e)
            {
                Notify.Notify.Error("Failed to deserialize " + name + ".dat, see console for more details");
                Debug.LogError(e.InnerException);
                tr.Close();
            }
            finally
            {
                Notify.Notify.Success("Successfully deserialized " + name + ".dat!");
                tr.Close();
                newGD = tmp;
            }

            return newGD;
        }
    }
}