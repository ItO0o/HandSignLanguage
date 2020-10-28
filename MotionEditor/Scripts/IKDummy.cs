using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKDummy : MonoBehaviour
{
    [SerializeField]
    IKManager manager;
    Animator animator;
    HumanPose humanPose;
    HumanPoseHandler handler;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        handler = new HumanPoseHandler(animator.avatar, animator.transform);
        humanPose = new HumanPose();
        handler.GetHumanPose(ref humanPose);
    }

   public void SetHandValue(int index,float value)
    {
        humanPose.muscles[index] = value;
    }

    public void GetPose()
    {
        handler.GetHumanPose(ref humanPose);
    }
    public void SetPose()
    {
        handler.SetHumanPose(ref humanPose);
    }
    // Update is called once per frame
    void Update()
    {
        //this.SetStickPose();
        //handler.GetHumanPose(ref humanPose);
    }

    private void LateUpdate()
    {
        //handler.SetHumanPose(ref humanPose);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (manager.GetComponent<InputUIManager>().currentMode != InputUIManager.ModeElm.Play)
        {
            manager.GetComponent<IKManager>().SetIK();
        }
    }

}
