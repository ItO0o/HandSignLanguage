using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueUI : MonoBehaviour
{
    [SerializeField]
    Text explanationText;
    [SerializeField]
    GameObject manager;
    [SerializeField]
    List<Slider> fingerSliders;
    [SerializeField]
    List<GameObject> fingerKeys;

    EditorFlow.FlowElm preFlow;
    EditorFlow.FlowElm currentFlow;

    [SerializeField]
    GameObject rightHandImage, leftHandImage;

    [SerializeField]
    Text errorText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentFlow = manager.GetComponent<EditorFlow>().editorSequences[manager.GetComponent<EditorFlow>().editorSequences.Count - 1];
        PrintExplanationText();

        //for (int i = 0; i < 10; i++)
        //{
        //    if (fingerSliders[i].value > 0)
        //    {
        //        fingerKeys[i].gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        fingerKeys[i].gameObject.SetActive(true);
        //    }
        //}

        if (currentFlow == EditorFlow.FlowElm.LeftFinger)
        {
            LeftHandKey();
        }
        if (currentFlow == EditorFlow.FlowElm.RightFinger)
        {
            RightHandKey();
        }

        if (currentFlow == EditorFlow.FlowElm.LeftFinger && preFlow != EditorFlow.FlowElm.LeftFinger)
        {
            ValueLeftHand();
        }
        else if (currentFlow != EditorFlow.FlowElm.LeftFinger && preFlow == EditorFlow.FlowElm.LeftFinger)
        {
            HideLeftHand();
        }
        if (currentFlow == EditorFlow.FlowElm.RightFinger && preFlow != EditorFlow.FlowElm.RightFinger)
        {
            ValueRightHand();
        }
        else if (currentFlow != EditorFlow.FlowElm.RightFinger && preFlow == EditorFlow.FlowElm.RightFinger)
        {
            HideRightHand();
        }

        preFlow = currentFlow;
    }

    void PrintExplanationText()
    {
        if (currentFlow != EditorFlow.FlowElm.Confirm)
        {
            explanationText.text = "ポーズ:" + manager.GetComponent<EditorFlow>().editorSequences.Count + " 設定箇所:";
            if (manager.GetComponent<EditorFlow>().editorSequences[manager.GetComponent<EditorFlow>().editorSequences.Count - 1] == EditorFlow.FlowElm.LeftArm) {
                explanationText.text += "左手の位置"; 
            }
            if (manager.GetComponent<EditorFlow>().editorSequences[manager.GetComponent<EditorFlow>().editorSequences.Count - 1] == EditorFlow.FlowElm.LeftHand) {
                explanationText.text += "左手の向き";
            }
            if (manager.GetComponent<EditorFlow>().editorSequences[manager.GetComponent<EditorFlow>().editorSequences.Count - 1] == EditorFlow.FlowElm.LeftFinger) {
                explanationText.text += "左手の指の形";
            }
            if (manager.GetComponent<EditorFlow>().editorSequences[manager.GetComponent<EditorFlow>().editorSequences.Count - 1] == EditorFlow.FlowElm.RightArm) {
                explanationText.text += "右手の位置";
            }
            if (manager.GetComponent<EditorFlow>().editorSequences[manager.GetComponent<EditorFlow>().editorSequences.Count - 1] == EditorFlow.FlowElm.RightHand) {
                explanationText.text += "右手の向き";
            }
            if (manager.GetComponent<EditorFlow>().editorSequences[manager.GetComponent<EditorFlow>().editorSequences.Count - 1] == EditorFlow.FlowElm.RightFinger) {
                explanationText.text += "右手の指の形";
            }
        }
        else if (manager.GetComponent<EditorFlow>().editorSequences.Count > 1 && manager.GetComponent<EditorFlow>().editorSequences.Count < 6)
        {
            explanationText.text = "この動きでいいですか？\n";
        }else if(manager.GetComponent<EditorFlow>().editorSequences.Count >= 6) {
            explanationText.text = "登録できるキーフレームは6つまでです。この動きでいいですか？";
        }
        else
        {
            explanationText.text = "このポーズでいいですか？";
        }
        if (currentFlow == EditorFlow.FlowElm.LeftArm || currentFlow == EditorFlow.FlowElm.RightArm)
        {
            if (manager.GetComponent<EditorFlow>().easyEditor) {
                explanationText.text += "\n手の位置を赤いボックスから選んでください";
            } else {
                explanationText.text += "\nマウスでボールの位置を設定してください(左クリック:追従切り替え)\n手の位置がボールに追従します";
            }
        }
        if (currentFlow == EditorFlow.FlowElm.LeftHand || currentFlow == EditorFlow.FlowElm.RightHand)
        {
            explanationText.text += " 手の向きを以下から選んでください";
        }
        if (currentFlow == EditorFlow.FlowElm.LeftFinger || currentFlow == EditorFlow.FlowElm.RightFinger)
        {
            explanationText.text += " 手形を以下から選んでください";
        }
    }

    private void HideRightHand()
    {
        rightHandImage.SetActive(false);
    }

    private void HideLeftHand()
    {
        leftHandImage.SetActive(false);
    }

    private void ValueRightHand()
    {
        rightHandImage.SetActive(true);
    }

    private void ValueLeftHand()
    {
        leftHandImage.SetActive(true);
    }

    private void RightHandKey()
    {
        for (int i = 5; i < 10; i++)
        {
            fingerKeys[i].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            if (fingerSliders[i].value > 0)
            {
                fingerKeys[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
        }
    }

    private void LeftHandKey()
    {
        for (int i = 0; i < 5; i++)
        {
            fingerKeys[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            if (fingerSliders[i].value > 0)
            {
                fingerKeys[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
        }
    }

    public void NoneInputWordError() {
        errorText.text = "単語名を入力してください";
        Invoke("DeleteErrorTxt",5f);
    }
    public void DeleteErrorTxt() {
        errorText.text = "\n";
        //string temp = "";
        //for (int i = 1;i < errorText.text.Length - 1; i++) {
        //    temp += errorText.text[i];
        //}
        //errorText.text = temp;
        //if(temp.Length > 0) {
        //    Invoke("DeleteErrorTxt", 0.5f);
        //}
    }
}
