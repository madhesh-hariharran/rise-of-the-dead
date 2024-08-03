using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeGreen : MonoBehaviour
{
    public void gaugegreen()
    {
        float randspawn = Random.Range(-118.5f, 77.4f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, randspawn);
    }
   

}
