using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BeatMenuManager : MonoBehaviour
{
    [SerializeField] BeatGameManager gameManager;
    public KeyCode pauseKey = KeyCode.Escape;
    public KeyCode playKey = KeyCode.Space ;

    public Button enterButton;
    public GameObject mainMenu, inGameMenu, pauseMenu;
    //public TextMeshProUGUI songTitleText, playerNameText, playerScoreText;
    //public TextMeshProUGUI currentScoreMultiplierText, currentComboText, hitCountText, missedCountText;
    public bool gamePaused;

    void Start()
    {
        gamePaused = false;
        mainMenu.SetActive(true);
        inGameMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseGame();
        }

        else if (Input.GetKeyDown(playKey))
        {
            StartGame();
        }

        else if (gameManager.gameStart)
        { StartGame(); }
    }

    public void StartGame()
    {
        Debug.Log("start clicked");
        gameManager.gameStart = true;
        mainMenu.SetActive(false);
        inGameMenu.SetActive(true);
    }

    public void PauseGame()
    {
        if (gameManager.gameStart)
        {
            Debug.Log("pause clicked");
            if (!gamePaused)
            {
                //stop the time flow
                Time.timeScale = 0;

                //rearrange the menu
                gameManager.gameStart = false;
                pauseMenu.SetActive(true);
            }
            else
            {
                //continue the time flow
                Time.timeScale = 1;

                //rearrange the menu
                gameManager.gameStart = true;
                pauseMenu.SetActive(false);
            }
        }
    }
}
