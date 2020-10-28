using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorFlow : MonoBehaviour
{
    [SerializeField]
    GameObject manager;
    public enum FlowElm
    {
        RightArm,
        RightHand,
        RightFinger,
        LeftArm,
        LeftHand,
        LeftFinger,
        Confirm
    }

    public List<FlowElm> editorSequences;
    public int flowSeq = 0;

    [SerializeField]
    public FlowElm[] defFlow = new FlowElm[7];

    public FlowElm preFlow;
    public int currentPoseIndex = 0;

    [SerializeField]
    GameObject confirmButton,nextButton;
    [SerializeField]
    GameObject poseObj,actor;

    [SerializeField]
    GameObject txtInputField, saveButton;
    // Start is called before the first frame update
    void Start()
    {
        editorSequences = new List<FlowElm>();
        editorSequences.Add(defFlow[0]);
        ChangeTarget(editorSequences[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.GetComponent<LocalMotion>().localMotion.Poses.Count > 1)
        {
            manager.GetComponent<AnimationManager>().currentElm = AnimationManager.ModeElm.Play;
            manager.GetComponent<AnimationManager>().currentState = AnimationManager.StateElm.Playing;
        }

        if (manager.GetComponent<LocalMotion>().localMotion.Poses.Count > 1 && manager.GetComponent<AnimationManager>().sequence == manager.GetComponent<AnimationManager>().motion.currentMotion.Poses.Count - 1)
        {
            manager.GetComponent<AnimationManager>().currentElm = AnimationManager.ModeElm.Loop;
        }
        preFlow = editorSequences[editorSequences.Count - 1];
    }

    void InitTargetUI()
    {
        List<GameObject> target = manager.GetComponent<IKManager>().target;
        for (int i = 0; i < target.Count; i++)
        {
            if (target[i].CompareTag("Target")) {
                target[i].GetComponent<SphereCollider>().enabled = false;
                target[i].GetComponent<MeshRenderer>().enabled = false;

                target[i].transform.GetChild(0).GetComponent<SphereCollider>().enabled = false;
                target[i].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    public void Next()
    {
        flowSeq++;
        if (defFlow[flowSeq] != FlowElm.Confirm) {
            editorSequences[editorSequences.Count - 1] = defFlow[flowSeq];
            ChangeTarget(editorSequences[editorSequences.Count - 1]);
        }
        else
        {
            editorSequences[editorSequences.Count - 1] = defFlow[flowSeq];
            WaitConfilm();
        }
    }

    void WaitConfilm()
    {
        if (editorSequences.Count > 1) {
            //manager.GetComponent<AnimationManager>().currentElm = AnimationManager.ModeElm.Loop;
            manager.GetComponent<LocalMotion>().PlayMotion();
        }
        if (editorSequences.Count < 2) {
            txtInputField.SetActive(false);
            saveButton.SetActive(false);
        }else {
            txtInputField.SetActive(true);
            saveButton.SetActive(true);
        }
        if (editorSequences.Count >= 6) {

        } else {
            confirmButton.SetActive(true);
        }
        nextButton.SetActive(false);
        manager.GetComponent<LocalMotion>().AddKeyFlame();
    }

    public void Revert()
    {
        manager.GetComponent<InputUIManager>().RemoveKeyFlame();
        confirmButton.SetActive(false);
        nextButton.SetActive(true);
        txtInputField.SetActive(false);
        saveButton.SetActive(false);
        flowSeq = 0;
        editorSequences[editorSequences.Count - 1] = defFlow[0];
        ChangeTarget(editorSequences[editorSequences.Count - 1]);
        poseObj.SetActive(true);
        actor.SetActive(false);
    }

    public void ConfilmPose()
    {
        flowSeq = 0;
        editorSequences.Add(defFlow[flowSeq]);
        ChangeTarget(editorSequences[editorSequences.Count - 1]);
        confirmButton.SetActive(false);
        txtInputField.SetActive(false);
        saveButton.SetActive(false);
        nextButton.SetActive(true);
        poseObj.SetActive(true);
        actor.SetActive(false);
        manager.GetComponent<InputUIManager>().AddPoseButton(editorSequences.Count - 1);
        manager.GetComponent<PoseSelector>().SetPose(currentPoseIndex);
        currentPoseIndex++;
    }

    void ChangeTarget(FlowElm elm)
    {
        InitTargetUI();
        List<GameObject> target = manager.GetComponent<InputUIManager>().targets;
        manager.GetComponent<InputUIManager>().currentMode = InputUIManager.ModeElm.Hand;
        if (elm == FlowElm.LeftArm)
        {
            target[0].GetComponent<SphereCollider>().enabled = true;
            target[0].GetComponent<MeshRenderer>().enabled = true;
            manager.GetComponent<InputUIManager>().target = target[0];
            manager.GetComponent<InputUIManager>().follow = false;
        }
        else if (elm == FlowElm.LeftHand)
        {
            target[2].GetComponent<SphereCollider>().enabled = true;
            target[2].GetComponent<MeshRenderer>().enabled = true;
            manager.GetComponent<InputUIManager>().target = target[2];
            manager.GetComponent<InputUIManager>().follow = false;
        }
        else if (elm == FlowElm.RightArm)
        {
            target[1].GetComponent<SphereCollider>().enabled = true;
            target[1].GetComponent<MeshRenderer>().enabled = true;
            manager.GetComponent<InputUIManager>().target = target[1];
            manager.GetComponent<InputUIManager>().follow = false;
        }
        else if (elm == FlowElm.RightHand)
        {
            target[3].GetComponent<SphereCollider>().enabled = true;
            target[3].GetComponent<MeshRenderer>().enabled = true;
            manager.GetComponent<InputUIManager>().target = target[3];
            manager.GetComponent<InputUIManager>().follow = false;
        }
        else if (elm == FlowElm.LeftFinger || elm == FlowElm.RightFinger)
        {
            manager.GetComponent<InputUIManager>().currentMode = InputUIManager.ModeElm.Finger;
        }
    }
}
