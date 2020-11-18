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
    Quaternion currentRightHand,currentLeftHand;
    [SerializeField]
    List<Slider> fingerSliders;
    [SerializeField]
    GameObject buttons;
    EditorFlow.FlowElm tempFlow;
    [SerializeField]
    GameObject rightHandUI, leftHandUI, rightFingerUI, leftFingerUI;
     // Start is called before the first frame update
    void Start()
    {
        test = true;
    }

    private void Update() {
        EditorFlow.FlowElm curFlow = this.GetComponent<EditorFlow>().editorSequences[this.GetComponent<EditorFlow>().editorSequences.Count - 1];
        if (curFlow == EditorFlow.FlowElm.RightFinger && tempFlow != EditorFlow.FlowElm.RightFinger) {
            rightFingerUI.SetActive(true);
        }
        if (curFlow != EditorFlow.FlowElm.RightFinger && tempFlow == EditorFlow.FlowElm.RightFinger) {
            rightFingerUI.SetActive(false);
        }
        if (curFlow == EditorFlow.FlowElm.RightHand && tempFlow != EditorFlow.FlowElm.RightHand) {
            rightHandUI.SetActive(true);
        }
        if (curFlow != EditorFlow.FlowElm.RightHand && tempFlow == EditorFlow.FlowElm.RightHand) {
            rightHandUI.SetActive(false);
        }
        if (curFlow == EditorFlow.FlowElm.LeftFinger && tempFlow != EditorFlow.FlowElm.LeftFinger) {
            leftFingerUI.SetActive(true);
        }
        if (curFlow != EditorFlow.FlowElm.LeftFinger && tempFlow == EditorFlow.FlowElm.LeftFinger) {
            leftFingerUI.SetActive(false);
        }
        if (curFlow == EditorFlow.FlowElm.LeftHand && tempFlow != EditorFlow.FlowElm.LeftHand) {
            leftHandUI.SetActive(true);
        }
        if (curFlow != EditorFlow.FlowElm.LeftHand && tempFlow == EditorFlow.FlowElm.LeftHand) {
            leftHandUI.SetActive(false);
        }
        tempFlow = this.GetComponent<EditorFlow>().editorSequences[this.GetComponent<EditorFlow>().editorSequences.Count - 1];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (test) {
            leftHand.transform.rotation = currentLeftHand;
            rightHand.transform.rotation = currentRightHand;
        }
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
    //
    public void RightFinger2() {
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
    public void RightFinger5() {
        fingerSliders[0].value = -0.5f;
        fingerSliders[1].value = -1;
        fingerSliders[2].value = 1;
        fingerSliders[3].value = 1;
        fingerSliders[4].value = 1;
    }
    //
    public void RightFinger6() {
        fingerSliders[0].value = -0.5f;
        fingerSliders[1].value = -1;
        fingerSliders[2].value = 1;
        fingerSliders[3].value = 1;
        fingerSliders[4].value = 1;
    }
    //
    public void RightFinger7() {
        fingerSliders[0].value = 0.5f;
        fingerSliders[1].value = 0.5f;
        fingerSliders[2].value = 0.5f;
        fingerSliders[3].value = 0.5f;
        fingerSliders[4].value = 0.5f;
    }
    
    public void RightFinger8() {
        fingerSliders[0].value = -0.25f;
        fingerSliders[1].value = -0.25f;
        fingerSliders[2].value = -0.25f;
        fingerSliders[3].value = -0.25f;
        fingerSliders[4].value = -0.25f;
    }
    public void RightFinger9() {
        fingerSliders[0].value = 1f;
        fingerSliders[1].value = -1f;
        fingerSliders[2].value = -1f;
        fingerSliders[3].value = -1f;
        fingerSliders[4].value = -1f;
    }
    public void RightFinger10() {
        fingerSliders[0].value = -1f;
        fingerSliders[1].value = 1f;
        fingerSliders[2].value = 1f;
        fingerSliders[3].value = -1f;
        fingerSliders[4].value = -1f;
    }

    //内＝体のセンターへ
    //掌をカメラに、指を上に
    public void LeftHand1() {
        currentLeftHand = new Quaternion(-0.3f, 0.5f, -0.5f, 0.6f);
    }
    //掌を下に、指をカメラに
    public void LeftHand2() {
        currentLeftHand = new Quaternion(-0.1f, 0.7f, -0.1f, 0.6f);
    }
    //掌を内に、指を上に
    public void LeftHand3() {
        currentLeftHand = new Quaternion(-0.8f, 0.6f, 0.1f, 0.2f);
    }
    //掌を下に、指を内に
    public void LeftHand4() {
        currentLeftHand = new Quaternion(0.0f, 1.0f, 0.0f, 0.0f);
    }
    //掌を内に、指をカメラに
    public void LeftHand5() {
        currentLeftHand = new Quaternion(-0.4f, 0.5f, 0.5f, 0.5f);
    }
    //掌をカメラ反対に、指を下に
    public void LeftHand6() {
        currentLeftHand = new Quaternion(0.5f, 0.5f, 0.6f, 0.4f);
    }

    public void LeftFinger1() {
        fingerSliders[5].value = 1;
        fingerSliders[6].value = 1;
        fingerSliders[7].value = 1;
        fingerSliders[8].value = 1;
        fingerSliders[9].value = 1;
    }
    //
    public void LeftFinger2() {
        fingerSliders[5].value = 1;
        fingerSliders[6].value = 1;
        fingerSliders[7].value = 1;
        fingerSliders[8].value = 1;
        fingerSliders[9].value = 1;
    }
    public void LeftFinger3() {
        fingerSliders[5].value = -1;
        fingerSliders[6].value = 1;
        fingerSliders[7].value = -1;
        fingerSliders[8].value = -1;
        fingerSliders[9].value = -1;
    }
    public void LeftFinger4() {
        fingerSliders[5].value = -1;
        fingerSliders[6].value = -1;
        fingerSliders[7].value = -1;
        fingerSliders[8].value = -1;
        fingerSliders[9].value = -1;
    }
    public void LeftFinger5() {
        fingerSliders[5].value = -0.5f;
        fingerSliders[6].value = -1;
        fingerSliders[7].value = 1;
        fingerSliders[8].value = 1;
        fingerSliders[9].value = 1;
    }
    //
    public void LeftFinger6() {
        fingerSliders[5].value = -0.5f;
        fingerSliders[6].value = -1;
        fingerSliders[7].value = 1;
        fingerSliders[8].value = 1;
        fingerSliders[9].value = 1;
    }
    //
    public void LeftFinger7() {
        fingerSliders[5].value = 0.5f;
        fingerSliders[6].value = 0.5f;
        fingerSliders[7].value = 0.5f;
        fingerSliders[8].value = 0.5f;
        fingerSliders[9].value = 0.5f;
    }

    public void LeftFinger8() {
        fingerSliders[5].value = -0.25f;
        fingerSliders[6].value = -0.25f;
        fingerSliders[7].value = -0.25f;
        fingerSliders[8].value = -0.25f;
        fingerSliders[9].value = -0.25f;
    }
    public void LeftFinger9() {
        fingerSliders[5].value = 1f;
        fingerSliders[6].value = -1f;
        fingerSliders[7].value = -1f;
        fingerSliders[8].value = -1f;
        fingerSliders[9].value = -1f;
    }
    public void LeftFinger10() {
        fingerSliders[5].value = -1f;
        fingerSliders[6].value = 1f;
        fingerSliders[7].value = 1f;
        fingerSliders[8].value = -1f;
        fingerSliders[9].value = -1f;
    }

}
