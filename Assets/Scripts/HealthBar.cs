using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    bool cantakedamage = true;
    PlayerMovement playerscript;
    Animator animator;

    private void Awake()
    {
        playerscript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(slider.value <= 0)
        {
            playerscript.playerdead();
            gameObject.SetActive(false);
        }
    }

    public void setmaxhealth(int maxhealthval)
    {
        slider.maxValue = maxhealthval;
        slider.value = maxhealthval;
    }
    public void sethealth(int damage)
    {
        if (cantakedamage)
        {
            cantakedamage = false;
            slider.value -= damage;
            //playerscript.playerhitanim();
            StartCoroutine(damagecooldown());
        }         
                 
    }

    IEnumerator damagecooldown()
    {
        yield return new WaitForSeconds(0.5f);
        cantakedamage = true;
    }
    

}
