using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camdisp1 : MonoBehaviour
{
    [SerializeField]camerafollow camerscript;    
    bool disp1 = false;
    public Dialogue scriptdial;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(disp1 == false)
            {
                disp1 = true;
                scriptdial.dialoguecall("Hmm... seems like I must get the hang of  \"hanging\" !!" );
                camerscript.cammov = true;
                camerscript.startpos = 1;
            }            
        }
    }


}
