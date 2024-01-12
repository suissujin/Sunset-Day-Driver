using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    public OnStartScript onStartScript;
    // public AudioManagerScript audioManagerScript;

    public GameObject mainMenu;
    public GameObject carSelectMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject firstSelectedMain;
    public GameObject firstSelectedCar;
    public GameObject firstSelectedSettings;
    public GameObject firstSelectedCredits;
    public TextMeshProUGUI musicText;
    public bool inMainMenu = true;

    private void Awake()
    {
        SwitchtoMenu();
    }
    private void Start()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        if (AudioManagerScript.instance.isMuted)
        {
            musicText.text = "Off";
        }
        else
        {
            musicText.text = "On";
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inMainMenu)
            {
                QuitGame();
            }
            else
            {
                SwitchtoMenu();
            }
        }
    }

    public void SwitchToCarSelect()
    {
        // Switch to the car select menu
        mainMenu.SetActive(false);
        carSelectMenu.SetActive(true);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        inMainMenu = false;
        EventSystem.current.SetSelectedGameObject(firstSelectedCar);
    }
    public void SwitchToSettings()
    {
        // Switch to the settings menu
        mainMenu.SetActive(false);
        carSelectMenu.SetActive(false);
        settingsMenu.SetActive(true);
        creditsMenu.SetActive(false);
        inMainMenu = false;
        EventSystem.current.SetSelectedGameObject(firstSelectedSettings);
    }

    public void SwitchToCredits()
    {
        mainMenu.SetActive(false);
        carSelectMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(true);
        inMainMenu = false;
        EventSystem.current.SetSelectedGameObject(firstSelectedCredits);
    }

    public void SwitchtoMenu()
    {
        // Switch to the main menu
        mainMenu.SetActive(true);
        carSelectMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        inMainMenu = true;
        EventSystem.current.SetSelectedGameObject(firstSelectedMain);
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }

    public void SetCarType1()
    {
        // Set the car type to 1
        OnStartScript.instance.carIndex = 1;
        SceneManager.LoadScene(1);
    }
    public void SetCarType2()
    {
        // Set the car type to 2
        OnStartScript.instance.carIndex = 2;
        SceneManager.LoadScene(1);
    }
    public void SetCarType3()
    {
        // Set the car type to 3
        OnStartScript.instance.carIndex = 3;
        SceneManager.LoadScene(1);
    }
    public void SetCarType4()
    {
        // Set the car type to 4
        OnStartScript.instance.carIndex = 4;
        SceneManager.LoadScene(1);
    }
}
