using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueTM ;
    public float textspeed = 0.07f;
    public string dialogue = "Bruh. Are you Good ?";
    public GameObject dialoguebox;

    PlayerMovement playerscript;

    private void Start()
    {
        playerscript = FindObjectOfType<PlayerMovement>();
    }

    public void dialoguecall(string dial)
    {
        playerscript.canmove = false;
        dialogue = dial;        
        dialoguebox.SetActive(true);       
        StartCoroutine(typedialogue());       
    }

    IEnumerator typedialogue()
    {
        foreach(char c in dialogue.ToCharArray())
        {            
            dialogueTM.text += c;
            yield return new WaitForSeconds(textspeed);
        }
        yield return new WaitForSeconds(1);
        dialogue = null;
        dialogueTM.text = null;
        playerscript.canmove = true;
        dialoguebox.SetActive(false);
    }
}


