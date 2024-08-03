using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger1 : MonoBehaviour
{
    public GameObject ball;
    public GameObject colliderprevent;
    public Dialogue script;
    bool dialoguecalled = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!dialoguecalled)
            {
                script.dialoguecall("This Way is Blocked, There must be another way through");
                dialoguecalled = true;
            }
            
            ball.SetActive(true);
            colliderprevent.SetActive(false);            
        }
    }

}
