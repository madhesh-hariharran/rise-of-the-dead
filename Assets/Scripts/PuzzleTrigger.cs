using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    [SerializeField] GameObject puzzle;
    [SerializeField] GameObject Timer;
    PlayerMovement playerscript;
    [SerializeField] GameObject cameravid;
    [SerializeField] AudioClip audio;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            puzzle.SetActive(true);
            cameravid.GetComponent<AudioSource>().clip = audio;
            cameravid.GetComponent<AudioSource>().Play();
            playerscript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            playerscript.canmove = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            puzzle.SetActive(true);
            Timer.SetActive(true);
        }
    }
}
