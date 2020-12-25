using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button playGameButton;
    public Button creditsButton;
    public Button quitGameButton;
    public Button quitCreditsButton;

    public GameObject creditsPanel;

    void Awake()
    {
        playGameButton.onClick.AddListener(delegate () { PlayGameButton(); });
        creditsButton.onClick.AddListener(delegate () { CreditsButton(); });
        quitGameButton.onClick.AddListener(delegate () { QuitGameButton(); });
        quitCreditsButton.onClick.AddListener(delegate () { QuitCreditsButton(); }) ;

        QuitCreditsButton();
    }

    public void PlayGameButton()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void CreditsButton()
    {
        creditsPanel.SetActive(true);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    public void QuitCreditsButton()
    {
        creditsPanel.SetActive(false);
    }
}
