using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderdisableBall : MonoBehaviour
{
    [SerializeField] GameObject colls;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            colls.SetActive(false);
        }
    }
}
