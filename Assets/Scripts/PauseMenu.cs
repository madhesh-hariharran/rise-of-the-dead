using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject startmenu;
    [SerializeField] GameObject ctrlscreen;
    [SerializeField] GameObject menus;    

    GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");          
    }
    public void resumefunc()
    {
        Time.timeScale = 1f;
        menus.SetActive(false);
        player.GetComponent<PlayerMovement>().pause = false;
    }
    public void startfunc()
    {
        Time.timeScale = 1f;
        LogicManager.currcp = 0;
        SceneManager.LoadScene("Swing");
    }

    public void loadcp()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Swing");
    }
    public void ctrlfunc()
    {        
        startmenu.SetActive(false);
        ctrlscreen.SetActive(true);
    }
    public void quitfunc()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
    public void backfunc()
    {        
        ctrlscreen.SetActive(false);
        startmenu.SetActive(true);
    }
}
