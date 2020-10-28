using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyUI : MonoBehaviour
{
    [SerializeField]
    GameObject actor;

    Animator animator;

    enum HandSide
    {
        Right,
        Left
    }
    HandSide currentSide;
    Vector3 currentTarget;
    // Start is called before the first frame update
    void Start()
    {
        animator = actor.GetComponent<Animator>();
        currentSide = HandSide.Right;
    }

    // Update is called once per frame
    void Update()
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
                    currentTarget = hit.collider.gameObject.transform.position;
                }
                else
                {
                    
                }
            }
        }
    }

    public void SwitchSide()
    {
        if (currentSide == HandSide.Left)
        {
            currentSide = HandSide.Right;
        }
        else if (currentSide == HandSide.Right)
        {
            currentSide = HandSide.Left;
        }
    }

    public void SetIK()
    {
        if (currentSide == HandSide.Left)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.5f);
            animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 0.5f);
            //animator.SetIKHintPosition(AvatarIKHint.LeftElbow, target[1].transform.position);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, currentTarget);
            //animator.SetIKRotation(AvatarIKGoal.LeftHand, target[0].transform.rotation);
        }

        if (currentSide == HandSide.Right)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.5f);
            animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0.5f);
            //animator.SetIKHintPosition(AvatarIKHint.RightElbow, target[3].transform.position);
            animator.SetIKPosition(AvatarIKGoal.RightHand, currentTarget);
            //animator.SetIKRotation(AvatarIKGoal.RightHand, target[2].transform.rotation);
        }
    }
}
