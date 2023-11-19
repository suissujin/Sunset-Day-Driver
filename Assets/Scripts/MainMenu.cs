using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public OnStartScript onStartScript;
    public AudioManagerScript audioManagerScript;

    public GameObject mainMenu;
    public GameObject carSelectMenu;
    public GameObject settingsMenu;
    public TextMeshProUGUI musicText;
    public TextMeshProUGUI sfxText;

    private void Awake()
    {
        mainMenu.SetActive(true);
        carSelectMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    private void Update()
    {
        // Update the music and sfx text
        if (audioManagerScript.musicSource.mute)
        {
            musicText.text = "Off";
        }
        else
        {
            musicText.text = "On";
        }
        if (audioManagerScript.sfxSource.mute)
        {
            sfxText.text = "Off";
        }
        else
        {
            sfxText.text = "On";
        }
    }

    public void SwitchToCarSelect()
    {
        // Switch to the car select menu
        mainMenu.SetActive(false);
        carSelectMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
    public void SwitchToSettings()
    {
        // Switch to the settings menu
        mainMenu.SetActive(false);
        carSelectMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void SwitchtoMenu()
    {
        // Switch to the main menu
        mainMenu.SetActive(true);
        carSelectMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }

    public void SetCarType1()
    {
        // Set the car type to 1
        onStartScript.carIndex = 1;
        SceneManager.LoadScene(1);
    }
    public void SetCarType2()
    {
        // Set the car type to 2
        onStartScript.carIndex = 2;
        SceneManager.LoadScene(1);
    }
    public void SetCarType3()
    {
        // Set the car type to 3
        onStartScript.carIndex = 3;
        SceneManager.LoadScene(1);
    }
    public void SetCarType4()
    {
        // Set the car type to 4
        onStartScript.carIndex = 4;
        SceneManager.LoadScene(1);
    }
}
