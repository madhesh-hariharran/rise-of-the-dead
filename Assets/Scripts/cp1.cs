using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cp1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            LogicManager.currcp = 1;
        }
    }
}
