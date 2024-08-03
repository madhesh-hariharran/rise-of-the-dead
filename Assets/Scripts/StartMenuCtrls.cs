using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuCtrls : MonoBehaviour
{
    [SerializeField] GameObject startmenu;
    [SerializeField] GameObject ctrlscreen;
    public void startfunc()
    {
        LogicManager.currcp = 0;
        SceneManager.LoadScene("Swing");
    }
    public void ctrlfunc()
    {
        startmenu.SetActive(false);
        ctrlscreen.SetActive(true);
    }
    public void quitfunc()
    {
        Application.Quit();
    }
    public void backfunc()
    {
        ctrlscreen.SetActive(false);
        startmenu.SetActive(true);        
    }
}
