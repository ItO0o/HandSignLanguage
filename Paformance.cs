using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paformance : MonoBehaviour
{
    public float elapsedTime;
    public bool counter_flag = false;

    // Update is called once per frame
    void Update()
    {
        if (counter_flag == true) {
            elapsedTime += Time.deltaTime;
            //Debug.Log("計測中： " + (elapsedTime).ToString());
        }
    }

}
