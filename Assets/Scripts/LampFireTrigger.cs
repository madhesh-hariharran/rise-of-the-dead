using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LampFireTrigger : MonoBehaviour
{
    bool fireon;
    GameObject entity;
    [SerializeField]GameObject F1;
    [SerializeField] GameObject F2;
    [SerializeField] GameObject F3;
    [SerializeField] GameObject F4;
    [SerializeField] GameObject F5;
    [SerializeField] GameObject F6;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Lamp")
        {
            fireon = true;
            F1.SetActive(true);
            F2.SetActive(true);
            F3.SetActive(true);
            F4.SetActive(true);
            F5.SetActive(true);
            F6.SetActive(true);
            print("SetFire");
        }
        /*
        if(collision.tag == "Enemy" && fireon == true)
        {            
            entity = collision.gameObject;
            StartCoroutine(death());
        }
        */

        if(collision.tag == "Player" && fireon == true)
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("GameOverLose");
        }
    }

    /*
    IEnumerator death()
    {
        yield return new WaitForSeconds(4);
        entity.SetActive(false);
    }
    */
}
