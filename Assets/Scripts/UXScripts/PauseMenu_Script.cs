using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu_Script : MonoBehaviour
{

    public GameObject pausePanel;
    public GameObject pauseMenu;
    public GameObject settingMenu;
    public GameObject Ambience;
    public GameObject dragon;

    bool gameLost = false;
    bool gameWon = false;
    public GameObject lossPanel;
    public GameObject winPanel;

    public GameObject pauseButton, optionsButton, lossButton;

    public GameObject GameHolder;
    public GameObject GameSpawner;
    public GameObject startScreen;
    bool ScreenActive = true;
    public GameObject panel1, panel2, panel3;
    public Image image1, image2, image3;
    bool clip1 = false;
    bool clip2 = false;
    bool clip3 = false;
    bool screen1 = false;
    public float counter = 3;
    public float currentCount = 1;

    void Start()
    {
        Time.timeScale = 1; 
    }

    void Update()
    {
        if (Input.GetButtonDown("Pausekey2") || Input.GetButtonDown("Pausekey"))
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

        if(ScreenActive == true)
        {
            Ambience.SetActive(false);
        }
        if (ScreenActive == false)
        {
            Ambience.SetActive(true);
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

        if (ScreenActive == true)
        {
            dragon.GetComponent<MainCharacter_Script>().enabled = false;
            GameSpawner.SetActive(false);
            Time.timeScale = 1;
        }
        if (ScreenActive == false)
        {
            dragon.GetComponent<MainCharacter_Script>().enabled = true;
            startScreen.SetActive(false);
            GameSpawner.SetActive(true);
        }

        if (ScreenActive == true && currentCount == 1)
        {
            clip1 = true;
        }
        if (ScreenActive == true && currentCount == 2)
        {
            clip2 = true;
        }
        if (ScreenActive == true && currentCount == 3)
        {
            clip3 = true;
        }
        if (ScreenActive == true && currentCount == 4)
        {
            screen1 = true;
        }

        if (counter <= 0)
        {
            currentCount++;
            counter = 3;
        }

        if (clip1 == true)
        {
            panel1.SetActive(true);
            counter -= Time.deltaTime;
        }

        if (clip2 == true)
        {
            clip1 = false;
            panel1.SetActive(false);
            panel2.SetActive(true);
            counter -= Time.deltaTime;
        }

        if (clip3 == true)
        {
            clip2 = false;
            panel3.SetActive(true);
            panel2.SetActive(false);
            counter -= Time.deltaTime;
        }
        if (screen1 == true)
        {
            clip3 = false;
            panel3.SetActive(false);
        }

        if (screen1 == true && Input.GetButton("Jump"))
        {
            ScreenActive = false;
            dragon.GetComponent<MainCharacter_Script>().enabled = true;
            if (GameSpawner.GetComponent<SpawnManager_Script>().currentWave == 0)
            {
            GameSpawner.GetComponent<SpawnManager_Script>().currentWave = 1;
            }

        }
        if (ScreenActive == true && Input.GetButton("Jump"))
        {
            ScreenActive = false;
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(false);
            dragon.GetComponent<MainCharacter_Script>().enabled = true;
            if (GameSpawner.GetComponent<SpawnManager_Script>().currentWave == 0)
            {
                GameSpawner.GetComponent<SpawnManager_Script>().currentWave = 1;
            }
        }

        if (GameSpawner.GetComponent<SpawnManager_Script>().currentWave == 12)
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

    public void Level1Click()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void TutorialClick()
    {
        SceneManager.LoadScene("Tutorialscene");
    }
    public void MainMenuClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }


}
