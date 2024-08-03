using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject pipeHolder;
    public GameObject[] pipes;

    [SerializeField]int totalpipes = 0;
    int correctedpipes = 0;

    public bool win = false;

    private void Start()
    {
        totalpipes = pipeHolder.transform.childCount;

        pipes = new GameObject[totalpipes];

        for (int i = 0; i < pipes.Length;i++)
        {
            pipes[i] = pipeHolder.transform.GetChild(i).gameObject;
        }
        
    }

    public void correctmove()
    {
        correctedpipes += 1;

        if (correctedpipes == totalpipes)
        {
            print("You Win");
            win = true;
        }
    }

    public void wrongmove()
    {
        correctedpipes -= 1;
    }

}
