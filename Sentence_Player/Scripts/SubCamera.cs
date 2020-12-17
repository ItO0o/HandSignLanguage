using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.rotation = new Quaternion(0,180,0,0);
        Transform parent = this.transform.parent;
        this.transform.position = new Vector3(parent.transform.position.x,parent.transform.position.y,parent.transform.position.z + 0.5f);
    }
}
