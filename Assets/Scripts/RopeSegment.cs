using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    public GameObject connectedabove, connectedbelow;
    void Start()
    {
        connectedabove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        RopeSegment abovesegment = connectedabove.GetComponent<RopeSegment>();
        if(abovesegment != null)
        {
            abovesegment.connectedbelow = gameObject;
            float spriteBottom = connectedabove.GetComponent<SpriteRenderer>().bounds.size.y;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBottom * -1);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
