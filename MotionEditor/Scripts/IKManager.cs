using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IKManager : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject currentObj;
    [SerializeField]
    public List<GameObject> target;

    [SerializeField]
    GameObject hipBone;

    [SerializeField]
    GameObject rightHandBone, leftHandBone;
    [SerializeField]
    GameObject midPoint;
    [SerializeField]
    GameObject head;
    [SerializeField]
    GameObject rightTipTarget, leftTipTarget;
    [SerializeField]
    GameObject manager;

    Vector3[] lastest = new Vector3[2];
    [SerializeField]
    GameObject rightBallPointer, leftBallPointer;
    [SerializeField]
    GameObject handPointersRight, handPointersLeft;
    [SerializeField]
    GameObject kneeHintRight, kneeHintLeft; 

    EditorFlow.FlowElm preFlow;
    bool preIsEasy;
    [SerializeField]
    Toggle easyToggle;
    [SerializeField]
    GameObject easyToggleObj;
    // Start is called before the first frame update
    void Start()
    {
        //animator = manager.GetComponent<UIManager>().currentObj.GetComponent<Animator>();
        //target = new List<GameObject>();
        animator = currentObj.GetComponent<Animator>();
        lastest[0] = rightTipTarget.transform.position;
        lastest[1] = leftTipTarget.transform.position;
    }

    private void Update()
    {
        EditorFlow.FlowElm curFlow = manager.GetComponent<EditorFlow>().editorSequences[manager.GetComponent<EditorFlow>().editorSequences.Count - 1];
        manager.GetComponent<EditorFlow>().easyEditor = easyToggle.isOn;
        bool curIsEasy = manager.GetComponent<EditorFlow>().easyEditor;
        if (curFlow == EditorFlow.FlowElm.RightArm && preFlow != EditorFlow.FlowElm.RightArm && curIsEasy == true)
        {
            handPointersRight.SetActive(true);
        }
        if (curFlow != EditorFlow.FlowElm.RightArm && preFlow == EditorFlow.FlowElm.RightArm)
        {
            handPointersRight.SetActive(false);
        }
        if (curFlow == EditorFlow.FlowElm.LeftArm && preFlow != EditorFlow.FlowElm.LeftArm && curIsEasy == true)
        {
            handPointersLeft.SetActive(true);
        }
        if (curFlow != EditorFlow.FlowElm.LeftArm && preFlow == EditorFlow.FlowElm.LeftArm)
        {
            handPointersLeft.SetActive(false);
        }
        if (curFlow != EditorFlow.FlowElm.LeftArm || curFlow != EditorFlow.FlowElm.RightArm)
        {
            if (curIsEasy == true && preIsEasy == false)
            {
                manager.GetComponent<EditorFlow>().InitTargetUI();
                if (curFlow == EditorFlow.FlowElm.LeftArm)
                {
                    handPointersLeft.SetActive(true);
                    handPointersRight.SetActive(false);
                }else if (curFlow == EditorFlow.FlowElm.RightArm)
                {
                    handPointersLeft.SetActive(false);
                    handPointersRight.SetActive(true);
                }
            }
            if (curIsEasy == false && preIsEasy == true)
            {
                handPointersLeft.SetActive(false);
                handPointersRight.SetActive(false);
                manager.GetComponent<EditorFlow>().ChangeTarget(curFlow);
                if (curFlow == EditorFlow.FlowElm.LeftArm)
                {
                    target[0] = leftBallPointer;
                    target[1] = kneeHintLeft; ;
                }
                else if (curFlow == EditorFlow.FlowElm.RightArm)
                {
                    target[2] = rightBallPointer;
                    target[3] = kneeHintRight;
                }
            }
        }
        preFlow = manager.GetComponent<EditorFlow>().editorSequences[manager.GetComponent<EditorFlow>().editorSequences.Count - 1];
        preIsEasy = manager.GetComponent<EditorFlow>().easyEditor;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.CompareTag("Hand_Pointer"))
                {
                    EditorFlow.FlowElm current = manager.GetComponent<EditorFlow>().editorSequences[manager.GetComponent<EditorFlow>().editorSequences.Count - 1];
                    if (current == EditorFlow.FlowElm.RightArm)
                    {
                        target[2] = hit.collider.gameObject;
                        target[3] = hit.collider.gameObject.transform.GetChild(0).gameObject;
                    }else if(current == EditorFlow.FlowElm.LeftArm)
                    {
                        target[0] = hit.collider.gameObject;
                        target[1] = hit.collider.gameObject.transform.GetChild(0).gameObject;
                    }
                }
                else
                {

                }
            }
        }

        if (manager.GetComponent<InputUIManager>().currentMode != InputUIManager.ModeElm.Play)
        {

            if (manager.GetComponent<HumanPoseCheck>().right || manager.GetComponent<HumanPoseCheck>().left) {
                //rightHandBone.transform.LookAt(rightTipTarget.transform.position);
                //rightHandBone.transform.Rotate(rightHandBone.transform.up, -90);
                //手のひらが下
                //rightHandBone.transform.rotation = new Quaternion(0, 90, 0, 0);

                leftHandBone.transform.LookAt(leftTipTarget.transform.position);
                leftHandBone.transform.Rotate(leftHandBone.transform.up, 90);

                lastest[0] = rightTipTarget.transform.position;
                lastest[1] = leftTipTarget.transform.position;
            } else {
                rightHandBone.transform.LookAt(lastest[0]);
                rightHandBone.transform.Rotate(rightHandBone.transform.up, -90);
                leftHandBone.transform.LookAt(lastest[1]);
                leftHandBone.transform.Rotate(leftHandBone.transform.up, 90);
            }

            //腰をいい感じに回す奴
            //SetHandMid();
            //hipBone.transform.LookAt(new Vector3(midPoint.transform.position.x, midPoint.transform.position.y, midPoint.transform.position.y));
        }
    }

    public void SetHandMid()
    {
        Vector3 point = (rightHandBone.transform.position + leftHandBone.transform.position) / 2;
        midPoint.transform.position = point;
    }

    public void SetIK()
    {
        //HumanPose pose = new HumanPose();
        //HumanPoseHandler handler;

        //handler = new HumanPoseHandler(animator.avatar,animator.transform);
        //handler.GetHumanPose(ref pose);
        //manager.GetComponent<HumanPoseCheck>().Check(ref pose);
        //左て追従
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.5f);
        animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 0.5f);
        animator.SetIKHintPosition(AvatarIKHint.LeftElbow, target[1].transform.position);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, target[0].transform.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, target[0].transform.rotation);

        //右手追従
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);
        animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0.5f);
        animator.SetIKHintPosition(AvatarIKHint.RightElbow, target[3].transform.position);
        animator.SetIKPosition(AvatarIKGoal.RightHand, target[2].transform.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, target[2].transform.rotation);
    }

}
