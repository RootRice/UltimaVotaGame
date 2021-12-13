using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TutorialPauseScript : MonoBehaviour
{

    public GameObject pausePanel;
    public GameObject pauseMenu;
    public GameObject settingMenu;
    public GameObject dragon;

    public GameObject GameSpawner;

    bool gameLost = false;
    bool gameWon = false;
    public GameObject lossPanel;
    public GameObject winPanel;

    public GameObject pauseButton, optionsButton, lossButton;

    public float counter = 3;
    public float currentCount = 1;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
                pauseMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(pauseButton);
                dragon.GetComponent<MainCharacter_Script>().enabled = false;
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
                pauseMenu.SetActive(false);
                settingMenu.SetActive(false);
                dragon.GetComponent<MainCharacter_Script>().enabled = true;
            }
        }

        if (dragon.GetComponent<MainCharacter_Script>().health == -3)
        {
            gameLost = true;
            Destroy(dragon);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(lossButton);
        }

        if (gameLost == true)
        {
            Time.timeScale = 0;
            lossPanel.SetActive(true);
        }


        if (GameSpawner.GetComponent<SpawnManager_Script>().currentWave == 6)
        {
            gameWon = true;
        }

        if (gameWon == true)
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
        }
    }

    public void Resumeclick()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        dragon.GetComponent<MainCharacter_Script>().enabled = true;
    }

    public void Exitclick()
    {
        Application.Quit();
        Debug.Log("exiting...");
    }

    public void SettingsClick()
    {
        pauseMenu.SetActive(false);
        settingMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsButton);
    }
    public void ReturnClick()
    {
        pauseMenu.SetActive(true);
        settingMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseButton);
    }
    public void RestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
     
    public void MainMenuClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
