using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyHandPosition : MonoBehaviour
{

    [SerializeField]
    GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        manager.GetComponent<EasyUI>().SetIK();
    }
}
