using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    int ipattacknum;
    int randindex;
    int randval;

    int aniidle =0;
    int anirun = 1;
    int anijump = 2;
    int anihurt = 4;
    int anideath = 41;
    int aniblocktry = 5;
    int aniblock = 51;
    int anifall = 6;
    int aniroll = 7;
    int aniledgegrab = 8;
    int aniwallslide = 9;


    int[] attackarray = { 31, 32, 33 };
    int[] attackarraydummy;

    float iphorizontal;
    float jumptimecounter;
    float coyotetime = 0.2f;
    float coyotetimecounter;
    float jumpbuffertime = 0.3f;
    float jumpbuffercounter;
    public float orginalgravity;
    float mass;
    [SerializeField] float attackrange = 4f;
    [SerializeField] float dashingpower = 24f;
    [SerializeField] float dashingtime = 0.2f;
    [SerializeField] float dashingcooldown = 15f;
    [SerializeField] float pushforce = 10f;
    [SerializeField] float jumptime;
    [SerializeField] float maxdistance;
    [SerializeField] float Enemydetectmaxdistance;
    [SerializeField] float speedhorizontal;
    [SerializeField] float speedsprint;
    [SerializeField] float speedvertical;
    [SerializeField] float impulsevertical;
    [SerializeField] float addvarbox;

    Vector2 horizontalmov;
    Vector2 verticalmov;
    Vector3 ropeherodiff;
    Vector2 checkpoint1 = new Vector2(5f, 3.6f);
    Vector2 checkpoint2 = new Vector2(37f, 3.6f);
    Vector2 checkpoint3 = new Vector2(81, 3.6f);
    Vector2 respawnpoint = new Vector2(-19f,0f);
    [SerializeField] Vector3 boxsize;
    [SerializeField] Vector3 Enemyboxsize;
    

    public bool isAlive = true;
    public bool canmove = true;
    bool canDash = true;
    bool onair = false;    
    bool idle;
    bool damaged;
    bool hurt;    
    bool ipattack;
    bool ipblock;
    bool iproll;    
    bool ipvertical;
    public bool grabattempt;
    bool isjumping;    
    public bool isgrabbing;
    public bool attacking;
    bool rolling;
    bool isdashing;
    bool facingright = true;
    bool A1, A2, A3;    
    bool fpressed;
    public bool pause = false;

    [SerializeField] GameObject pausemenu;
    public GameObject lampmover;
    public GameObject zipmover;
    GameObject player;   
    GameObject mover;    
    GameObject chainpart;
    
    Animator animator;
    HingeJoint2D hinge;
    GameObject chandlier;
    GameObject hook;
    HingeJoint2D chainhinge;
    Rigidbody2D playerrb;
    BoxCollider2D boxcoll;
    CircleCollider2D circlcoll;
    CapsuleCollider2D capscoll;    
    [SerializeField] LayerMask platformlayermask;
    [SerializeField] LayerMask EnemiesLM;
    [SerializeField] TrailRenderer tr;    
    [SerializeField] Transform attachedto;
    [SerializeField] Gauge gaugescript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerrb = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponent<Animator>();
        capscoll = GetComponent<CapsuleCollider2D>();
        circlcoll = GetComponent<CircleCollider2D>();
        boxcoll = GetComponent<BoxCollider2D>();
        hinge = GetComponent<HingeJoint2D>();        

        tr.emitting = false;
        attackarraydummy = attackarray;

        horizontalmov = Vector2.zero;
        verticalmov = new Vector2(0f, 1f);

        speedhorizontal = 250f;
        speedsprint = 5.0f;
        speedvertical = 3.5f;
        impulsevertical = 10f;
        jumptime = 0.25f;
        maxdistance = -0.11f;

        if(LogicManager.currcp == 0)
        {
            transform.position = respawnpoint;
        }
        else if(LogicManager.currcp == 1)
        {
            transform.position = checkpoint1;
        }
        else if(LogicManager.currcp == 2)
        {
            transform.position = checkpoint2;
        }
        else if(LogicManager.currcp == 3)
        {
            transform.position = checkpoint3;
        }

    }
    private void FixedUpdate()
    {        
        if (attacking == false && ipblock == false && isdashing == false && canmove == true)
        {
            playerrb.velocity = new Vector2(horizontalmov.x * speedhorizontal * Time.deltaTime, playerrb.velocity.y);
        }
        else if (attacking == true && playerrb.velocity.y != 0 && isdashing == false)
        {
            playerrb.velocity = new Vector2(horizontalmov.x * speedhorizontal * Time.deltaTime, playerrb.velocity.y);
        }

        if (playerrb.velocity.x != 0 && IsGrounded() && attacking == false && ipblock == false && rolling == false && hurt == false && isgrabbing == false && isAlive)
        {
            animator.SetInteger("Anim", anirun);
        }
        if (playerrb.velocity.y > 0.1f && !IsGrounded() && attacking == false && hurt == false && isgrabbing == false && rolling == false && isAlive)
        {
            animator.SetInteger("Anim", anijump);
        }
        if (playerrb.velocity.y < 0 && !IsGrounded() && attacking == false && hurt == false && idle == false && isgrabbing == false && rolling == false && isAlive)
        {
            animator.SetInteger("Anim", anifall);
        }
        
    }
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Escape) && !attacking)
        {
            pause = true;
            Time.timeScale = 0f;
            pausemenu.SetActive(true);
        }


        //Attack
        ipattack = Input.GetMouseButtonDown(0);
        if (ipattack)
        {
            if (attacking == false && ipblock == false && rolling == false && hurt == false && isgrabbing == false && isAlive)
            {
                randindex = Random.Range(0, attackarraydummy.Length);
                randval = attackarray[randindex];
                animator.SetInteger("Anim", randval);
                playerrb.velocity = new Vector2(0f, playerrb.velocity.y);
                attacking = true;
            }
        }
        RaycastHit2D hit = Physics2D.BoxCast(new Vector3(transform.position.x, transform.position.y + addvarbox, transform.position.z), Enemyboxsize, 0, transform.right, Enemydetectmaxdistance, EnemiesLM);
        if (hit.collider != null)
        {
            
            if (Vector2.Distance(transform.position, hit.collider.transform.position) <= attackrange && attacking )
            {
                // Perform the hit on the enemy.
                
                hit.collider.gameObject.GetComponent<NormalZombieBehaviour>().DamageTaken();                
            }
        }
        //Block
        /*
        if (IsGrounded())                
        {
            ipblock = Input.GetMouseButton(1);
            if (ipblock && attacking == false && rolling == false && hurt == false && isAlive)
            {
                animator.SetInteger("Anim", aniblocktry);
                playerrb.velocity = new Vector2(0f, 0f);
            }
        }
        */
        //Roll
        if (attacking == false && IsGrounded() && isAlive)         
        {
            iproll = Input.GetKeyDown(KeyCode.LeftControl);
            if (iproll && hurt == false)
            {
                animator.SetInteger("Anim", aniroll);
                rolling = true;
                capscoll.enabled = false;
                circlcoll.enabled = true;
            }
        }         
        //Movement Dir.
        iphorizontal = Input.GetAxisRaw("Horizontal");
        horizontalmov.x = iphorizontal;
        //Swing
        if (isgrabbing)
        {            
            playerrb.AddRelativeForce(horizontalmov * pushforce);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                detach();
            }
        }
        //Idle_Anim
        if (iphorizontal == 0 && IsGrounded() && attacking == false && ipblock == false && rolling == false && hurt == false && isAlive)
        {
            idle = true;
            animator.SetInteger("Anim", aniidle);
        }
        else
        {
            idle = false;
        }
        //Direction Change
        if (iphorizontal > 0 && facingright == false && attacking == false && !isgrabbing)
        {
            flip();
            facingright = true;
        }
        else if (iphorizontal < 0 && facingright == true && attacking == false && !isgrabbing)
        {
            flip();
            facingright = false;
        }        
        //Dashing
        if (onair == true && Input.GetKeyDown(KeyCode.LeftShift) == true)
        {
            StartCoroutine(Dash());
        }
        //Jumping
        ipvertical = Input.GetKeyDown("space");
        if (IsGrounded())
        {
            coyotetimecounter = coyotetime;
        }
        else
        {
            coyotetimecounter -= Time.deltaTime;
        }
        if (ipvertical)
        {
            jumpbuffercounter = jumpbuffertime;
        }
        else
        {
            jumpbuffercounter -= Time.deltaTime;
        }
        if (coyotetimecounter > 0f && jumpbuffercounter > 0f)
        {
            isjumping = true;
            rolling = false;
            jumptimecounter = jumptime;
            jumpbuffercounter = 0f;
            playerrb.AddForce(Vector2.up * (impulsevertical + (-playerrb.velocity.y)), ForceMode2D.Impulse);
            capscoll.enabled = true;
            circlcoll.enabled = false;
            if (attacking == false && hurt == false && isAlive)
            {
                animator.SetInteger("Anim", anijump);
            }
        }
        if ((Input.GetKey("space") || Input.GetKey("w")) && isjumping == true)
        {
            if (jumptimecounter > 0)
            {
                playerrb.AddForce(Vector2.up * speedvertical, ForceMode2D.Force);
                if (attacking == false && hurt == false && isAlive)
                {
                    animator.SetInteger("Anim", anijump);
                }
                jumptimecounter -= Time.deltaTime;
            }
            else
            {
                isjumping = false;
            }
        }
        if (Input.GetKeyUp("space") || Input.GetKeyUp("w"))
        {
            isjumping = false;
            coyotetimecounter = 0f;
        }       

        //Ground_Check
        if (IsGrounded())
        {
            onair = false;
        }
        else
        {
            onair = true;
        }
        //Chain_Grab
        if (Input.GetKeyDown("f") && !fpressed)
        {
            grabattempt = true;
            StartCoroutine(pressg());
        }
    }
    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    IEnumerator Dash()
    {
        if (canDash)
        {
            canDash = false;
            isdashing = true;
            orginalgravity = playerrb.gravityScale;
            playerrb.gravityScale = 0f;
            if (facingright == true)
            {
                playerrb.velocity = new Vector2(dashingpower, 0f);
            }
            else
            {
                playerrb.velocity = new Vector2(-dashingpower, 0f);
            }
            tr.emitting = true;
            yield return new WaitForSeconds(dashingtime);
            tr.emitting = false;
            playerrb.gravityScale = orginalgravity;
            isdashing = false;
            yield return new WaitForSeconds(dashingcooldown);
            canDash = true;
        }
        
    }

    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isgrabbing)
        {
            if (collision.gameObject.tag == "Chain")
            {                
                if (grabattempt && isAlive)
                {
                    print("Instantiated");
                    Instantiate(lampmover, collision.gameObject.transform.position, Quaternion.identity);
                    mover = GameObject.FindGameObjectWithTag ("LampMover" );
                    mover.GetComponent<HingeJoint2D>().connectedBody = playerrb;
                    transform.SetParent(mover.transform);
                    grabattempt = false;
                    isgrabbing = true;
                    fpressed = true;
                    animator.SetInteger("Anim", aniledgegrab);  
                }
            }
        }
    }
    IEnumerator pressg() {         
        yield return new WaitForSeconds(0.25f);
        grabattempt = false;        
    }
    
    public void attach(Rigidbody2D rope)
    {
        chainpart = rope.gameObject;
        chainpart.GetComponent<DistanceJoint2D>().enabled = true;
        chainpart.GetComponent<DistanceJoint2D>().connectedBody = playerrb;
        chandlier = chainpart.GetComponent<Hingereference>().Lantern;
        hook = chainpart.GetComponent<Hingereference>().hinge;
        hinge.connectedBody = rope;
        hinge.enabled = true;
        transform.SetParent(rope.transform);
    }
    void detach()
    {
        chainpart.GetComponent<DistanceJoint2D>().connectedBody = null;
        chainpart.GetComponent<DistanceJoint2D>().enabled = false;         
        isgrabbing = false;
        hinge.connectedBody = null;        
        hinge.enabled = false;
        transform.SetParent(null);
        orginalgravity = playerrb.gravityScale;        
        playerrb.gravityScale = 0.2f;
        playerrb.AddRelativeForce(new Vector2(transform.position.x *100, transform.position.y *25));    
        rolling = true;
        animator.SetInteger("Anim", aniroll);        
        StartCoroutine(playerjumpinvi());        
    }

    IEnumerator playerjumpinvi()
    {
        capscoll.enabled = false;        
        hook.GetComponent<DistanceJoint2D>().enabled = false;
        chandlier.GetComponent<BoxCollider2D>().enabled = true;
        chainpart.GetComponent<HingeJoint2D>().enabled = false;
        chandlier.GetComponent<Rigidbody2D>().gravityScale = 5;
        gaugescript.gaugegame();
        yield return new WaitForSeconds(2f);
        if (isgrabbing == true)
        {
            playerrb.gravityScale = orginalgravity;
            print("Breaked");
            yield break;
        }
        capscoll.enabled = true;
        playerrb.gravityScale = orginalgravity;
        print("Gravity set to orginal");
        rolling = false;        
        animator.SetInteger("Anim", anifall);
    }

    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public bool Attacking()
    {
        A1 = this.animator.GetCurrentAnimatorStateInfo(0).IsName("Hero_Attack1");
        A2 = this.animator.GetCurrentAnimatorStateInfo(0).IsName("Hero_Attack2");
        A3 = this.animator.GetCurrentAnimatorStateInfo(0).IsName("Hero_Attack3");

        if (A1 || A2 || A3)
        {
            attacking = true;
            return true;
        }
        else
        {
            attacking = false;
            return false;
        }
    }
    public void animationfinishedtrigger()
    {
        if(attacking == true)
        {
            attacking = false;
        }
        else if(rolling == true)
        {
            capscoll.enabled = true;
            circlcoll.enabled = false;
            rolling = false;
        }
        else if(hurt == true)
        {
            hurt = false;
        }        
    }     
    /*
    public void playerhitanim()
    {
        if (isAlive && attacking)
        {
            attacking = false;
            capscoll.enabled = true;
            circlcoll.enabled = false;
            rolling = false;
            hurt = true;
            animator.SetInteger("Anim", anihurt);
        }
        
    }
    */
    public void playerdead()
    {
        isAlive = false;
        attacking = false;
        canmove = false;
        rolling = false;
        hurt = false;
        idle = false;
        animator.SetInteger("Anim", anideath);
    }

    public void gameover()
    {
        SceneManager.LoadScene("GameOverLose");
    }


    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }
    
    private bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position,boxsize,0, - transform.up , maxdistance, platformlayermask) && !isgrabbing)
        {            
            return true;
        }
        else
        {
            return false;
        }
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawCube(transform.position - transform.up * maxdistance, boxsize);
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y + addvarbox, transform.position.z) + transform.right * Enemydetectmaxdistance, Enemyboxsize);

    }
    */
}




 


















