using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;

public class Navbar : MonoBehaviour
{
    public Text UIname;
    public Text UIcredits;
    public Text UIskillpoints;

    public void Update()
    {
        if (GameManager.Loaded)
        {
            UIname.text = GameManager.data.player.name;
            UIcredits.text = GameManager.data.player.credits.ToString();
            UIskillpoints.text = GameManager.data.player.skillpoints.ToString();
        }
    }
}
