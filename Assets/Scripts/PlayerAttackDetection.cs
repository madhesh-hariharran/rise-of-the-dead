using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackDetection : MonoBehaviour
{
    public PlayerMovement playermov;        

    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (playermov.Attacking() == true && collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<NormalZombieBehaviour>().DamageTaken();
        }
    }
   
    
}
