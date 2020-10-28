using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    [SerializeField]
    GameObject leftCamera, rightCamera;
    [SerializeField]
    GameObject leftHand, rightHand;
    [SerializeField]
    GameObject actorLeft, actorRight;
    [SerializeField]
    GameObject manager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (manager.GetComponent<EditorFlow>().editorSequences[manager.GetComponent<EditorFlow>().currentPoseIndex] != EditorFlow.FlowElm.Confirm)
        {
            leftCamera.transform.position = new Vector3(leftHand.transform.position.x, leftHand.transform.position.y, leftHand.transform.position.z + 1.0f);
            rightCamera.transform.position = new Vector3(rightHand.transform.position.x, rightHand.transform.position.y, rightHand.transform.position.z + 1.0f);
        }
        else
        {
            leftCamera.transform.position = new Vector3(actorLeft.transform.position.x, actorLeft.transform.position.y, actorLeft.transform.position.z + 1.0f);
            rightCamera.transform.position = new Vector3(actorRight.transform.position.x, actorRight.transform.position.y, actorRight.transform.position.z + 1.0f);
        }
    }
}
