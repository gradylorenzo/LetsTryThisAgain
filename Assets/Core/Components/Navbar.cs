using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;

public class Navbar : MonoBehaviour
{
    public Text name;
    public Text credits;
    public Text skillpoints;

    private void FixedUpdate()
    {
        name.text = GameManager.gameData.playerData.name;
        credits.text = GameManager.gameData.playerData.credits.ToString();
        skillpoints.text = GameManager.gameData.skillsData.unallocatedPoints.ToString();
    }
}
