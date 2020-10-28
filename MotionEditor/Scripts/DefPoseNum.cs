using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefPoseNum : MonoBehaviour
{
    public int num;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(0).GetComponent<Text>().text = (num+ 1).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
