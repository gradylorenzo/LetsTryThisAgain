using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;
using Core.Data;
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
    [Header("New Game")]
    public GameObject NewGamePanel;
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

    #region LoadGamePanel elements
    [Header("Load Game")]
    public GameObject LoadGamePanel;
    public GameObject LoadGameButtonPrefab;
    public GameObject LoadGameScrollingContent;
    public GameObject NoGamesFound;

    private List<GameObject> LoadGameButtons = new List<GameObject>();
    #endregion

    #region Resume elements
    [Header("Resume Elements")]
    public GameObject ResumeButton;
    #endregion

    #region ConfirmNewGamePanel elements
    [Header("Confirm New Game")]
    public GameObject ConfirmNewGamePanel;
    public Text ConfirmNewGamePanelCharacterDescription;
    #endregion

    public void Start()
    {
        menuState = MenuStates.MainMenu;
        ResumeButton.GetComponent<Button>().interactable = PlayerPrefs.HasKey("RECENT_SAVE");
    }

    public void Update()
    {
        NewGamePanel.SetActive(menuState == MenuStates.NewGame || menuState == MenuStates.ConfirmNewGame);
        LoadGamePanel.SetActive(menuState == MenuStates.LoadGame);
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
        GameManager.LoadGame(PlayerPrefs.GetString("RECENT_SAVE"));
        Notify.Log("Resume");
    }
    public void SetMenuNewGame()
    {
        menuState = MenuStates.NewGame;
        Notify.Log("New game");
    }
    public void SetMenuLoadGame()
    {
        menuState = MenuStates.LoadGame;
        foreach(GameObject go in LoadGameButtons)
        {
            Destroy(go);
        }
        LoadGameButtons.Clear();
        BuildFileList();
        Notify.Log("Load");
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
        menuState = MenuStates.NewGame;
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

    #region LoadGame methods
    public void LoadGame(string name)
    {
        GameManager.LoadGame(name);
    }

    public void BuildFileList()
    {
        SaveInfo[] files = GameManager.GetSaveList();
        NoGamesFound.SetActive(files.Length == 0);
        for(int i = 0; i < files.Length; i++)
        {
            GameObject newSaveButton = Instantiate(LoadGameButtonPrefab, transform.position, transform.rotation);
            newSaveButton.transform.parent = LoadGameScrollingContent.transform;
            newSaveButton.GetComponent<MMFileListButton>().file.text = files[i].name;
            newSaveButton.GetComponent<MMFileListButton>().date.text = files[i].date;
            newSaveButton.GetComponent<MMFileListButton>().mmc = this;
            Rect buttonRect = newSaveButton.GetComponent<RectTransform>().rect;
            newSaveButton.GetComponent<RectTransform>().rect.Set(buttonRect.x, buttonRect.y, 400, buttonRect.height);

            LoadGameButtons.Add(newSaveButton);
        }
    }

    #endregion
}