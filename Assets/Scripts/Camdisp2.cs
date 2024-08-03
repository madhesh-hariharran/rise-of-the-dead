using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camdisp2 : MonoBehaviour
{
    [SerializeField] GameObject dummy;
    [SerializeField] camerafollow camerscript;    
    bool disp2 = false;

    [SerializeField] GameObject z1;
    [SerializeField] GameObject z2;
    [SerializeField] GameObject z3;
    [SerializeField] GameObject z4;
    [SerializeField] GameObject z5;
    [SerializeField] GameObject z6;
    [SerializeField] GameObject z7;
    [SerializeField] GameObject z8;
    [SerializeField] GameObject z9;
    [SerializeField] GameObject z10;
    [SerializeField] GameObject z11;
    [SerializeField] GameObject z12;    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dummy.SetActive(false);
            if (disp2 == false)
            {
                disp2 = true;
                camerscript.cammov = true;
                camerscript.startpos = 2;
            }
            StartCoroutine(zomdeath());
        }
    }
    IEnumerator zomdeath()
    {
        yield return new WaitForSeconds(1);
        z1.GetComponent<NormalZombieBehaviour>().zombiefiredie();
        z2.GetComponent<NormalZombieBehaviour>().zombiefiredie();
        z3.GetComponent<NormalZombieBehaviour>().zombiefiredie();
        z4.GetComponent<NormalZombieBehaviour>().zombiefiredie();
        z5.GetComponent<NormalZombieBehaviour>().zombiefiredie();
        yield return new WaitForSeconds(1);
        z6.GetComponent<NormalZombieBehaviour>().zombiefiredie();
        z7.GetComponent<NormalZombieBehaviour>().zombiefiredie();
        yield return new WaitForSeconds(1);
        z8.GetComponent<NormalZombieBehaviour>().zombiefiredie();
        z9.GetComponent<NormalZombieBehaviour>().zombiefiredie();
        yield return new WaitForSeconds(1);
        z10.GetComponent<NormalZombieBehaviour>().zombiefiredie();
        z11.GetComponent<NormalZombieBehaviour>().zombiefiredie();
        z12.GetComponent<NormalZombieBehaviour>().zombiefiredie();
       
    }
}
