using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressureplate : MonoBehaviour
{
    public JumppadMovementScript scriptJP;
    public Dialogue scriptdial;
    bool dialoguecalled = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Ball")
        {            
            scriptJP.istriggered = true;
            if (collision.tag == "Player")
            {
                if (!dialoguecalled)
                {
                    scriptdial.dialoguecall("Hmm... Need some weights.");
                    dialoguecalled = true;
                }
            }
        }
        
    }




}
