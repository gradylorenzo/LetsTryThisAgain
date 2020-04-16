using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Notify;


public class MMFileListButton : MonoBehaviour
{
    public string name
    {
        get { return file.text; }
    }
    public MainMenuController mmc;
    public Text file;
    public Text date;

    public void OnClick()
    {
        mmc.LoadGame(name);
    }
}
