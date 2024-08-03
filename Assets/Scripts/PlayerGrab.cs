using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{/*
    Animator animator;
    public PlayerMovement script;
    HingeJoint2D hinge;
    
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        hinge = gameObject.GetComponent<HingeJoint2D>();
        hinge.enabled = false;
        
    }

    private void OnTriggerStay2D(Collider2D  collision)
    {
        print("collided");
        if (collision.gameObject.tag == "Chain")
        {
            print("Chain Detected");
            hinge.enabled = true;
            script.isgrabbing = true;
            if (Input.GetKeyDown("g"))
            {
                script.isgrabbing = true;
                print("Grabbed");
                animator.SetInteger("Anim", 8);
            }
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("collided");
        if (collision.gameObject.tag == "Chain")
        {            
            print("Chain Detected");
            if (Input.GetKeyDown("g"))
            {
                script.isgrabbing = true;
                print("Grabbed");
                animator.SetInteger("Anim", 8);
            }
        }
    }
    */
    
}
