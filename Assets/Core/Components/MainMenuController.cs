using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;
using Core.Notify;
using System;

public class MainMenuController : MonoBehaviour
{
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
    public string[] Sexes = new string[]
    {
        "Male",
        "Female",
        "Other"
    };
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
    public GameObject SaveExistsWarning;

    private Careers career;
    private string currentName = "";
    private string lastName = "";
    private bool saveExists = false;
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
        ConfirmNewGamePanelCharacterDescription.text = (CharacterNameField.text + ", " + Sexes[CharacterSexField.value] + " " + career.ToString()).ToUpper();
        ExitConfirm.SetActive(menuState == MenuStates.ExitConfirm);

        if(menuState == MenuStates.NewGame)
        {
            NewGamePanelUpdate();
        }

        currentName = CharacterNameField.text;
        if(currentName != lastName)
        {
            saveExists = GameManager.SaveAlreadyExists(currentName);
            SaveExistsWarning.SetActive(saveExists);
            lastName = currentName;
        }
    }

    private void NewGamePanelUpdate()
    {
        StartButton.SetActive(CharacterNameField.text != null && CharacterNameField.text != "" && menuState != MenuStates.ConfirmNewGame && !saveExists);
    }

    #region SetMenu methods
    //These methods are only called by the Main Menu buttons.
    public void SetMenuMain()
    {
        Notify.Log("MainMenu");
        menuState = MenuStates.MainMenu;
    }
    public void SetMenuResume()
    {
        menuState = MenuStates.ResumeGame;
        Notify.Error("Resume, not implemented");
    }
    public void SetMenuNewGame()
    {
        menuState = MenuStates.NewGame;
        Notify.Log("New game");
    }
    public void SetMenuLoadGame()
    {
        menuState = MenuStates.LoadGame;
        Notify.Error("Load, not implemented");
    }
    public void SetMenuSettings()
    {
        menuState = MenuStates.Settings;
        Notify.Error("Settings, not implemented");
    }
    public void SetMenuExitConfirm()
    {
        menuState = MenuStates.ExitConfirm;
    }
    public void CloseApplication()
    {
        Notify.Log("Closing Application");
        Application.Quit();
    }
    public void SetMenuConfirmNewGame()
    {
        Notify.Log("Confirm new game");
        menuState = MenuStates.ConfirmNewGame;
    }
    #endregion

    #region NewGame methods
    public void StartNewGame()
    {
        Notify.Error("START NEW GAME");
        string name = CharacterNameField.text;
        int sex = CharacterSexField.value;
        GameManager.NewGame(name, sex, (int)career);
    }

    public void SetNewGameCareerStrategist()
    {
        career = Careers.strategist;
        Notify.Success("Career changed to Strategist!");
    }
    public void SetNewGameCareerIndustrialist()
    {
        career = Careers.industrialist;
        Notify.Success("Career changed to Industrialist!");
    }
    public void SetNewGameCareerColonist()
    {
        career = Careers.colonist;
        Notify.Success("Career changed to Colonist!");
    }
    #endregion
}