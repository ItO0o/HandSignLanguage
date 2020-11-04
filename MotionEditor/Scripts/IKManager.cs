using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        //animator = manager.GetComponent<UIManager>().currentObj.GetComponent<Animator>();
        //target = new List<GameObject>();
        animator = currentObj.GetComponent<Animator>();
        lastest[0] = rightTipTarget.transform.position;
        lastest[1] = leftTipTarget.transform.position;
    }


    private void LateUpdate()
    {
        //if (manager.GetComponent<UIManager>().currentObj.GetComponent<Animator>() != null && animator == null)
        //{
        //    animator = manager.GetComponent<UIManager>().currentObj.GetComponent<Animator>();
        //}
        //if (target != null)
        //{
        //    Debug.Log(animator);
        //    //animator.SetLookAtWeight(0.5f);
        //    //animator.SetLookAtPosition(target[0].transform.position);
        //    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        //    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        //    animator.SetIKPosition(AvatarIKGoal.LeftHand, target[0].transform.position);
        //    animator.SetIKRotation(AvatarIKGoal.LeftHand, target[0].transform.rotation);
        //}
        //SetIK();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.CompareTag("Hand_Pointer"))
                {
                    int currentSeq = manager.GetComponent<EditorFlow>().flowSeq;
                    EditorFlow.FlowElm current = manager.GetComponent<EditorFlow>().editorSequences[currentSeq];
                    if (current == EditorFlow.FlowElm.RightArm)
                    {
                        target[2] = hit.collider.gameObject;
                        target[3] = hit.collider.gameObject.transform.GetChild(0).gameObject;
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

            SetHandMid();
            hipBone.transform.LookAt(new Vector3(midPoint.transform.position.x, midPoint.transform.position.y, midPoint.transform.position.y));
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
