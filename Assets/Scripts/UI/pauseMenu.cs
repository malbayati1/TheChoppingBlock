using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pauseMenu : MonoBehaviour
{
    private bool isPauseMenueActive = false;
    [SerializeField] private GameObject pausedMenu;
    private void Awake()
    {
        isPauseMenueActive = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        isPauseMenueActive = !isPauseMenueActive;
        if (isPauseMenueActive)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        pausedMenu.SetActive(isPauseMenueActive);
    }
}
