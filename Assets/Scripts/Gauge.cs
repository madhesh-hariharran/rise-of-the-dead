using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    [SerializeField] GaugeGreen script;
    [SerializeField]PlayerMovement playerscript;
    public GameObject redr;
    public GameObject greenr;
    bool greenset = false;
    bool result;

   
    void Start()
    {     
        /*
        redr = GameObject.FindGameObjectWithTag("GaugeRed");
        float deg = redr.GetComponent<RectTransform>().eulerAngles.z;
        greenr = GameObject.FindGameObjectWithTag("GaugeGreen");
        script = greenr.GetComponent<GaugeGreen>();
        */
    }

    public void gaugegame()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0.25f;
        script.gaugegreen();
        greenset = true;
        
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float degr = redr.GetComponent<RectTransform>().eulerAngles.z;
            float degg = greenr.GetComponent<RectTransform>().eulerAngles.z;

            if (degg <= degr && degg + 40f >= degr )
            {
                playerscript.grabattempt = true;
            }
            else
            {
                playerscript.grabattempt = false;
            }
            greenset = false;
            Time.timeScale = 1f;
            gameObject.SetActive(false);           
            
        }
        
    }
}
