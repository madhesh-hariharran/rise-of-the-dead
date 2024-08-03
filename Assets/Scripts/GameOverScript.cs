using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScript : MonoBehaviour
{
    public bool GameOver = false;

    public void QuitGame()
    {        
        Application.Quit();
    }
    public void loadcp()
    {
        SceneManager.LoadScene("Swing");
    }

    public void RestartGame()
    {
        LogicManager.currcp = 0;
        SceneManager.LoadScene("Swing");
    }
    public void MainMenu()
    {        
        SceneManager.LoadScene("Start Screen");
    }
}
