using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pauseMenu : MonoBehaviour
{
    private bool pauseMenuActive = false;
    [SerializeField] private GameObject pausedMenu;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        pauseMenuActive = !pauseMenuActive;
        if (pauseMenuActive)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        pausedMenu.SetActive(pauseMenuActive);
    }
}
