using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;
using Core.Notify;
using System;

public class MainMenuController : MonoBehaviour
{
    public DefaultGameData[] defaultCareers;

    [System.Serializable]
    private enum MenuStates
    {
        MainMenu,
        ResumeGame,
        NewGame,
        LoadGame,
        Settings,
        ExitConfirm,
        ConfirmNewGame
    }
    private MenuStates menuState;
    public GameObject ExitConfirm;

    #region NewGamePanel elements
    public GameObject NewGamePanel;
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
    public GameObject StartButton;
    private Careers career;
    #endregion

    #region ConfirmNewGamePanel elements
    [Header("Confirm New Game")]
    public GameObject ConfirmNewGamePanel;
    public Text ConfirmNewGamePanelCharacterDescription;
    #endregion

    public void Start()
    {
        menuState = MenuStates.MainMenu;
    }

    public void Update()
    {
        NewGamePanel.SetActive(menuState == MenuStates.NewGame || menuState == MenuStates.ConfirmNewGame);
        ConfirmNewGamePanel.SetActive(menuState == MenuStates.ConfirmNewGame);
        ConfirmNewGamePanelCharacterDescription.text = (CharacterNameField.text + ", " + GameData.Sexes[CharacterSexField.value] + " " + career.ToString()).ToUpper();
        ExitConfirm.SetActive(menuState == MenuStates.ExitConfirm);

        if(menuState == MenuStates.NewGame)
        {
            NewGamePanelUpdate();
        }
    }

    private void NewGamePanelUpdate()
    {
        StartButton.SetActive(CharacterNameField.text != null && CharacterNameField.text != "" && menuState != MenuStates.ConfirmNewGame);
    }

    #region SetMenu methods
    //These methods are only called by the Main Menu buttons.
    public void SetMenuMain()
    {
        Notify.Log(Notify.Intent.Error, "MainMenu");
        menuState = MenuStates.MainMenu;
    }
    public void SetMenuResume()
    {
        menuState = MenuStates.ResumeGame;
        Notify.Log(Notify.Intent.Error, "Resume, not implemented");
    }
    public void SetMenuNewGame()
    {
        menuState = MenuStates.NewGame;
        Notify.Log(Notify.Intent.Error, "New game");
    }
    public void SetMenuLoadGame()
    {
        menuState = MenuStates.LoadGame;
        Notify.Log(Notify.Intent.Error, "Load, not implemented");
    }
    public void SetMenuSettings()
    {
        menuState = MenuStates.Settings;
        Notify.Log(Notify.Intent.Error, "Settings, not implemented");
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
    public void SetMenuConfirmNewGame()
    {
        Notify.Log(Notify.Intent.Error, "Confirm new game");
        menuState = MenuStates.ConfirmNewGame;
    }
    #endregion

    #region NewGame methods
    public void StartNewGame()
    {
        Notify.Log(Notify.Intent.Error, "START NEW GAME");
        GameData newData = defaultCareers[(int)career].gameData;
        string name = CharacterNameField.text;
        int sex = CharacterSexField.value;
        GameManager.Create(newData, name, sex);
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
