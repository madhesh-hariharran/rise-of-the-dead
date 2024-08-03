using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    public GameObject player;
    public bool cammov = false;
    public int startpos;
    [SerializeField]bool reachedend = false;
    [SerializeField] bool reachedstart = false;
    float movespeed = 10f;
    Vector3 platfrm1 = new Vector3(37f,2f,-10f);
    Vector3 platfrm2 = new Vector3(81f,2f,-10f);
    Transform post;
    Vector3 pos;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * ymax = 3.4f
         * ymin = 1.65f
         * xmin = -13.5f
         * xmax = 71.3f
         */

        if(cammov == false)
        {
            post = player.GetComponent<Transform>();
            pos = post.position;
            transform.position = new Vector3(Mathf.Clamp(pos.x + 5f, -13.5f, 101.35f), Mathf.Clamp(pos.y, 1.65f, 3.4f), pos.z = -10);
        }
        else if(cammov == true)
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;            
            if (startpos == 1 && !reachedend)
            {                
                transform.position = Vector3.MoveTowards(transform.position, platfrm2, movespeed*Time.deltaTime);
                if(transform.position == platfrm2)
                {
                    reachedend = true;                    
                }
            }
            if (reachedend)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movespeed * Time.deltaTime);
                if (transform.position == player.transform.position)
                {
                    reachedend = false;
                    player.GetComponent<PlayerMovement>().canmove = true;
                    player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;                    
                    cammov = false;
                }
            }
            else if (startpos == 2 && !reachedstart)
            {
                transform.position = Vector3.MoveTowards(transform.position, platfrm1, movespeed * Time.deltaTime);
                if (transform.position == platfrm1)
                {
                    reachedstart = true;
                }
            }
            if(reachedstart)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movespeed * Time.deltaTime);
                if (transform.position == player.transform.position)
                {
                    reachedstart = false;
                    player.GetComponent<PlayerMovement>().canmove = true;
                    player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;                    
                    cammov = false;
                }

            }
                    
                
            
        }
        
        
        

    }
}
