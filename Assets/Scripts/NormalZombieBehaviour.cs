using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NormalZombieBehaviour : MonoBehaviour
{
    GameObject _player;
    [SerializeField]HealthBar hp;
    public Animator animator;

    //Vector2 offset;
    Vector2 _pos;

    int _idleAct = 0;
    int _walkAct = 1;
    int _attackAct = 2;
    int _deadAct = 5;
    int _takehitAct = 4;
    int _aggattackAct = 3;

    public float zombieHealth = 100f;
    float zombieSpeed = 2f;
    float _xdist;
    //float rdist;
    float zombieattackrange = 2.6f;
    [SerializeField] float playermaxdist;
    [SerializeField] float addvarzombbox;
    [SerializeField] Vector3 playerboxsize;
    [SerializeField] LayerMask playerLm;

    bool attackanimplay = false;
    bool dead = false;
    bool facingright = false;
    [SerializeField] bool _isattacking=false;
    bool idle;
    bool walk;
    [SerializeField] bool hitbool;
    [SerializeField] bool _canAttack = true;
    [SerializeField] bool _takeDamage = true;

    [SerializeField] private Rigidbody2D _rb;
    public float _force = 5;
    public float _kbcounter;
    public float _kbTotalTime = 0.5f;
    public bool _kbright;

    void Start()
    {        
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if(_player.transform.position.x < transform.position.x && !facingright)
        {
            flip();
            facingright = true;
        }
        else if (_player.transform.position.x > transform.position.x && facingright)
        {
            flip();
            facingright = false;
        }
        if(!_isattacking && !hitbool && !dead)
        {
            _canAttack = true;
        }
        if (!_player.GetComponent<PlayerMovement>().isAlive)
        {
            animator.SetInteger("act", _idleAct);
        }
        //MoveZombie();
        //offset = _player.transform.position - transform.position;
        //rdist = offset.magnitude;
        if (zombieHealth <= 0 && !dead )
        {
            dead = true;
            animator.SetInteger("act", _deadAct);            
        }
        RaycastHit2D hit = Physics2D.BoxCast(new Vector3(transform.position.x, transform.position.y + addvarzombbox, transform.position.z), playerboxsize, 0, transform.right, playermaxdist, playerLm);
        if (hit.collider != null)
        {            
            if (Vector2.Distance(transform.position, hit.collider.transform.position - Vector3.left)<= zombieattackrange && _canAttack && !dead && !attackanimplay &&!hitbool && _player.GetComponent<PlayerMovement>().isAlive )
            {                
                _canAttack = false;
                _isattacking = true;                
                animator.SetInteger("act", _attackAct);                
            }
            else
            {
                /*
                print(Vector2.Distance(transform.position, hit.collider.transform.position) <= zombieattackrange);
                print(Vector2.Distance(transform.position, hit.collider.transform.position));
                print(_canAttack);
                */

            }
        }       

        if (_player.transform.position.x < transform.position.x)
        {
            _kbright = false;
        }
        else
        {
            _kbright = true;
        }

        if (_kbcounter <= 0)
        {
            MoveZombie();                                            
        }
        else
        {
            if (_kbright == true)
            {
                _rb.velocity = new Vector2(-_force, 2.5f);
            }
            if (_kbright == false)
            {
                _rb.velocity = new Vector2(_force, 2.5f);
            }
            _kbcounter -= Time.deltaTime;
        }
    }    
    void MoveZombie()  
    {
        if ( _takeDamage && !dead && !_isattacking && !hitbool && _player.GetComponent<PlayerMovement>().isAlive)
        {
            _pos = _player.transform.position;
            _pos.y = -2.2f;
            _xdist = transform.position.x - _pos.x;
            if (Mathf.Abs(_xdist) < 8 && Mathf.Abs(_xdist) >= 1.0f )
            {
                animator.SetInteger("act", _walkAct);
                transform.position = Vector2.MoveTowards(transform.position, _pos, zombieSpeed * Time.deltaTime);
            }
            else if(!_isattacking)
            {
                animator.SetInteger("act", _idleAct);
            }
        }                
    }

    public void DamageTaken()
    {
        _kbcounter = _kbTotalTime;
        if (_takeDamage && !dead)
        {
            hitbool = true;
            _isattacking = false;           
            zombieHealth -= 40f;
            _takeDamage = false;
            animator.SetInteger("act", _takehitAct);
            StartCoroutine(damagecooldown());
        }        
     }
    IEnumerator damagecooldown()
    {
        

        yield return new WaitForSeconds(1);
        animator.SetInteger("act", _idleAct);
        hitbool = false;
        _takeDamage = true;        
    }   
        
    public void zombiefiredie()
    {
        zombieHealth = 0;
        dead = true;
        animator.SetInteger("act", _deadAct);
    }
    public void ZombieDeath()
    {
            Destroy(gameObject);
     }
    public void Cooldown()
    {
        hp.sethealth(10);        
        _canAttack = true;
        _isattacking = false;
        attackanimplay = false;
    }
    void flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawCube(transform.position - transform.up * maxdistance, boxsize);
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y + addvarzombbox, transform.position.z) + transform.right * playermaxdist, playerboxsize);

    }
    */
    
}

