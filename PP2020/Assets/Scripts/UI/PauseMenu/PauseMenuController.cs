using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public GameObject player;

    public Button resumeButton;
    public Button quitButton;
    public GameObject pauseMenuPanel;

    void Awake()
    {
        pauseMenuPanel.SetActive(false);
        resumeButton.onClick.AddListener(delegate () { pauseMenuPanel.SetActive(false); });
        quitButton.onClick.AddListener(delegate () { Application.Quit(); });
    }



    void Update()
    {
        if (player.GetComponent<CharacterController2D>().gameEnded)
            return;

        Time.timeScale = pauseMenuPanel.activeSelf ? 0 : 1;

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);
        }
    }
}
