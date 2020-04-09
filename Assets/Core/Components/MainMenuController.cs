using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Notify;
using System;

public class MainMenuController : MonoBehaviour
{
    [System.Serializable]
    public enum MenuStates
    {
        MainMenu,
        ResumeGame,
        NewGame,
        LoadGame,
        Settings,
        ExitConfirm
    }

    public MenuStates menuState;
    public GameObject NewGamePanel;
    public GameObject ExitConfirm;

    #region NewGamePanel elements
    [Header("New Game")]
    public string[] CareerFlavorTexts;
    public DefaultGameData[] CareerDefaultGameData;
    private enum Careers
    {
        strategist = 0,
        industrialist = 1,
        colonist = 2
    }
    public Text CharacterNameField;
    public Dropdown CharacterSexField;
    public Text CareerFlavorText;
    public Button StartButton;
    private Careers career;
    #endregion

    public void Start()
    {
        menuState = MenuStates.MainMenu;
    }

    public void Update()
    {
        NewGamePanel.SetActive(menuState == MenuStates.NewGame);
        ExitConfirm.SetActive(menuState == MenuStates.ExitConfirm);

        if(menuState == MenuStates.NewGame)
        {
            NewGamePanelUpdate();
        }
    }

    private void NewGamePanelUpdate()
    {
        StartButton.interactable = (CharacterNameField.text != null && CharacterNameField.text != "");
    }

    #region SetMenu methods
    //These methods are only called by the Main Menu buttons.
    public void SetMenuMain()
    {
        menuState = MenuStates.MainMenu;
    }
    public void SetMenuResume()
    {
        menuState = MenuStates.ResumeGame;
        Notify.Log(Notify.Intent.Error, "Not yet implemented");
    }
    public void SetMenuNewGame()
    {
        menuState = MenuStates.NewGame;
    }
    public void SetMenuLoadGame()
    {
        menuState = MenuStates.LoadGame;
        Notify.Log(Notify.Intent.Error, "Not yet implemented");
    }
    public void SetMenuSettings()
    {
        menuState = MenuStates.Settings;
        Notify.Log(Notify.Intent.Error, "Not yet implemented");
    }
    public void SetMenuExitConfirm()
    {
        menuState = MenuStates.ExitConfirm;
    }
    public void CloseApplication()
    {
        Notify.Log(Notify.Intent.Success, "Closing Application");
        Application.Quit();
    }
    #endregion

    #region NewGame methods
    public void StartNewGame()
    {

    }

    public void SetNewGameCareerStrategist()
    {
        career = Careers.strategist;
        Notify.Log(Notify.Intent.Success, "Career changed to Strategist!");
    }
    public void SetNewGameCareerIndustrialist()
    {
        career = Careers.industrialist;
        Notify.Log(Notify.Intent.Success, "Career changed to Industrialist!");
    }
    public void SetNewGameCareerColonist()
    {
        career = Careers.colonist;
        Notify.Log(Notify.Intent.Success, "Career changed to Colonist!");
    }
    #endregion
}
