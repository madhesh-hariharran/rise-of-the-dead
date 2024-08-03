using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piperotation : MonoBehaviour
{

    public PuzzleManager puzzlescript;
    float[] rotats = {0,90,180,270};
    public float[] correctrotation;    

    [SerializeField]bool isplaced = false;

    public int possrots;
    

    private void Start()
    {
        possrots = correctrotation.Length;
        int rand = Random.Range(0, rotats.Length);
        transform.eulerAngles = new Vector3(0, 0, rotats[rand]);

        if (possrots == 2)
        {
            if (Mathf.Approximately(Mathf.Round(transform.eulerAngles.z), correctrotation[0]) || Mathf.Approximately(Mathf.Round(transform.eulerAngles.z), correctrotation[1]))
            {
                isplaced = true;
                puzzlescript.correctmove();
            }
        }
        else if(possrots == 1)
        {            
            if (Mathf.Approximately(Mathf.Round(transform.eulerAngles.z), correctrotation[0]))
            {
                isplaced = true;
                puzzlescript.correctmove();
            }
        }
    }      
    public void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        if (possrots == 2)
        {            
            if (Mathf.Approximately(Mathf.Round(transform.eulerAngles.z),correctrotation[0]) || Mathf.Approximately(Mathf.Round(transform.eulerAngles.z), correctrotation[1]) && isplaced == false)
            {
                isplaced = true;
                puzzlescript.correctmove();
            }
            else if (isplaced == true)
            {
                isplaced = false;
                puzzlescript.wrongmove();
            }
        }
        else if(possrots == 1)
        {
            if (Mathf.Approximately(Mathf.Round(transform.eulerAngles.z), correctrotation[0]) && isplaced == false)
            {
                isplaced = true;
                puzzlescript.correctmove();
            }
            else if (isplaced == true)
            {
                isplaced = false;
                puzzlescript.wrongmove();
            }
        }
    }

   
}
