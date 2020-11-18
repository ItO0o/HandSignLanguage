using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanPoseCheck : MonoBehaviour
{
    [SerializeField]
    public Animator animator;
    HumanPose humanPose;
    HumanPoseHandler handler;

    [SerializeField]
    GameObject rightHandBone, leftHandBone;
    [SerializeField]
    GameObject rightTipTarget, leftTipTarget;

    public bool right,left;
    public bool check;

    [SerializeField]
    GameObject poseWarningTxt;
    // Start is called before the first frame update
    void Start()
    {
        handler = new HumanPoseHandler(animator.avatar, animator.transform);
        handler.GetHumanPose(ref humanPose);
    }

    // Update is called once per frame
    void Update()
    {
        check = true;
        //for (int i = 0; i < HumanTrait.MuscleCount; i++) {
        //    if(humanPose.muscles[i] > 1 || humanPose.muscles[i] < -1) {
        //        check = false;
        //    }
        //}
        if (humanPose.muscles[53] < 0f) {
            check = false;
        }
        if (humanPose.muscles[44] < 0f) {
            check = false;
        }
        if (check == false) {
            poseWarningTxt.GetComponent<Text>().text = "<color=#ff0000>無理なポーズ</color>";
        } else {
            poseWarningTxt.GetComponent<Text>().text = "";
        }
        //rightHandBone.transform.LookAt(rightTipTarget.transform.position);
        //rightHandBone.transform.Rotate(rightHandBone.transform.up, -90);
        //leftHandBone.transform.LookAt(leftTipTarget.transform.position);
        //leftHandBone.transform.Rotate(leftHandBone.transform.up, 90);
        //if (humanPose.muscles[53] > 1 || humanPose.muscles[53] < 0) {
        //    right = false;
        //} else {
        //    right = true;
        //}
        //if (humanPose.muscles[44] > 1 || humanPose.muscles[44] < 0) {
        //    left = false;
        //} else {
        //    left = true;
        //}
        handler.GetHumanPose(ref humanPose);
    }
}
