using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampMover : MonoBehaviour
{
    [SerializeField]PlayerMovement playerscript;
    [SerializeField] HingeJoint2D hinge;
    [SerializeField] Rigidbody2D currope;

    private void Start()
    {
        playerscript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if(hinge != null)
        {
            if (hinge.connectedBody.gameObject.tag == "Chain")
            {
                transform.position = Vector2.MoveTowards(transform.position, hinge.connectedBody.gameObject.transform.position, 4f * Time.deltaTime);
            }
            else if (hinge.connectedBody.gameObject.tag != "Chain")
            {
                playerscript.attach(currope);
                Destroy(gameObject);
            }
        }        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Chain")
        {
            currope = collision.GetComponent<Rigidbody2D>();
            hinge = collision.GetComponent<HingeJoint2D>();            
        }
    }


}
