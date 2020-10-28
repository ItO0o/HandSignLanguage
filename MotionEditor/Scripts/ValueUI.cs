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
            explanationText.text = "ポーズ:" + manager.GetComponent<EditorFlow>().editorSequences.Count + " 部位:" + manager.GetComponent<EditorFlow>().editorSequences[manager.GetComponent<EditorFlow>().editorSequences.Count - 1].ToString();
        }
        else if (manager.GetComponent<EditorFlow>().editorSequences.Count > 1)
        {
            explanationText.text = "この動きでいいですか？";
        }
        else
        {
            explanationText.text = "このポーズでいいですか？";
        }
        if (currentFlow == EditorFlow.FlowElm.LeftArm || currentFlow == EditorFlow.FlowElm.RightArm)
        {
            explanationText.text += "\nマウスでボールの位置を設定してください(左クリック:追従切り替え)\n手の位置がボールに追従します";
        }
        if (currentFlow == EditorFlow.FlowElm.LeftHand || currentFlow == EditorFlow.FlowElm.RightHand)
        {
            explanationText.text += "\nマウスでボールの位置を設定してください(左クリック:追従切り替え)\n手がボールの方向を向きます";
        }
        if (currentFlow == EditorFlow.FlowElm.LeftFinger || currentFlow == EditorFlow.FlowElm.RightFinger)
        {
            explanationText.text += "\nキーボードの指に割り当てられたキーで指の曲げ伸ばしを設定してください";
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
}
