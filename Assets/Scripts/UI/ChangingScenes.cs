using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangingScenes : MonoBehaviour
{
    private bool paused;
    public void ChangeScene(string s)
    {
        SceneManager.LoadScene(s);
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
