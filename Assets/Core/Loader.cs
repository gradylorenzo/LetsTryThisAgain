using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core.Notify;

public class Loader : MonoBehaviour
{
    private void Start()
    {
        Notify.Success("Standby..");
        Invoke("LoadScene", 5);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        Notify.Success("MainMenu Loaded");
    }
}
