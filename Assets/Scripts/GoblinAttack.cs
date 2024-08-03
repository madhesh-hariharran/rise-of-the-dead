using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    CircleCollider2D circlcoll;
    BoxCollider2D boxcoll;
    float speed = 2.2f;
    float distx;
    float distance;
    bool facingleft = false;
    bool canattack = true;
    bool attacking = false;
    bool visiblerange = false;
    bool hit = false;
    bool playerposinstance = false;
    bool waiting = false;

    Vector2 playerpos;
    Vector2 playerposfrattack;
    GameObject player;
    public Animator animator;
    PlayerMovement playerscript;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        playerscript = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(player.transform.position, gameObject.transform.position);

        if (distance <= 6f)
        {
            visiblerange = true;
        }
        else
        {
            visiblerange = false; ;
        }
        distx = gameObject.transform.position.x - player.transform.position.x;

        if (distx <= 1.8f && canattack == true)
        {
            attacking = true;
            canattack = false;
            if (hit == false)
            {
                animator.SetInteger("FZ", 1);
            }

        }
    }
}
