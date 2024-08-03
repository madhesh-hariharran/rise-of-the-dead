using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiplineMover : MonoBehaviour
{
    HingeJoint2D hinge;
    private void Update()
    {
        transform.position=  Vector2.MoveTowards(transform.position, hinge.gameObject.transform.position, 1f*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Chain")
        {
            hinge = collision.GetComponent<HingeJoint2D>();
            print(hinge.attachedRigidbody);
            //print(hinge.connectedAnchor);
        }
    }
}
