using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandShape : MonoBehaviour
{
    [SerializeField]
    GameObject rightHand, leftHand;
    [Range(-1, 1)]
    public float x, y, z, w;
    [SerializeField]
    bool test = true;
    [SerializeField]
    GameObject obj;
    Quaternion currentRightHand;
    [SerializeField]
    List<Slider> fingerSliders;
     // Start is called before the first frame update
    void Start()
    {
        test = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (test) {
            //rightHand.transform.LookAt(obj.transform.position);
            //rightHand.transform.rotation = new Quaternion(x, y, z, w);
            rightHand.transform.rotation = currentRightHand;
        }
        Debug.Log(rightHand.transform.rotation);
    }
    //内＝体のセンターへ
    //掌をカメラに、指を上に
    public void RightHand1() {
        currentRightHand = new Quaternion(-0.4f, -0.6f, 0.4f, 0.5f);
    }
    //掌を下に、指をカメラに
    public void RightHand2() {
        currentRightHand = new Quaternion(-0.2f, -0.7f, 0.0f, 0.7f);
    }
    //掌を内に、指を上に
    public void RightHand3() {
        currentRightHand = new Quaternion(0.7f, 0.6f, 0.1f, -0.2f);
    }
    //掌を下に、指を内に
    public void RightHand4() {
        currentRightHand = new Quaternion(0.0f, -0.9f, 0.0f, 0.4f);
    }
    //掌を内に、指をカメラに
    public void RightHand5() {
        currentRightHand = new Quaternion(0.6f, 0.5f, 0.5f, -0.3f);
    }
    //掌をカメラ反対に、指を下に
    public void RightHand6() {
        currentRightHand = new Quaternion(-0.4f, 0.6f, 0.6f, -0.4f);
    }
    public void RightFinger1() {
        fingerSliders[0].value = 1;
        fingerSliders[1].value = 1;
        fingerSliders[2].value = 1;
        fingerSliders[3].value = 1;
        fingerSliders[4].value = 1;
    }
    public void RightFinger3() {
        fingerSliders[0].value = -1;
        fingerSliders[1].value = 1;
        fingerSliders[2].value = -1;
        fingerSliders[3].value = -1;
        fingerSliders[4].value = -1;
    }
    public void RightFinger4() {
        fingerSliders[0].value = -1;
        fingerSliders[1].value = -1;
        fingerSliders[2].value = -1;
        fingerSliders[3].value = -1;
        fingerSliders[4].value = -1;
    }
}
