using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject surface;
    [SerializeField]
    public GameObject target;

    [SerializeField]
    public List<GameObject> targets;

    public bool follow = false;

    [SerializeField]
    List<Slider> slider;

    [SerializeField]
    GameObject actor;
    [SerializeField]
    float fingerTime;
    float fingerCnt = 0;
    [SerializeField]
    Text fingerTimer;

    [SerializeField]
    List<GameObject> fingersJoint;

    public bool preFollow;

    [SerializeField]
    GameObject manager;
    [SerializeField]
    GameObject inputCanves;
    enum StageElm
    {
        HandPosition,
        FingerPose
    }

    public enum ModeElm
    {
        Hand,
        Finger,
        Play,
        None
    }

    public ModeElm currentMode;

    // Start is called before the first frame update
    void Start()
    {
        fingerKeys.Add(KeyCode.Y);
        fingerKeys.Add(KeyCode.U);
        fingerKeys.Add(KeyCode.I);
        fingerKeys.Add(KeyCode.O);
        fingerKeys.Add(KeyCode.P);
        fingerKeys.Add(KeyCode.T);
        fingerKeys.Add(KeyCode.R);
        fingerKeys.Add(KeyCode.E);
        fingerKeys.Add(KeyCode.W);
        fingerKeys.Add(KeyCode.Q);

        TargetPointerCollor(target);
        InitTargetPointerCollor();
    }

    List<KeyCode> fingerKeys = new List<KeyCode>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && manager.GetComponent<EditorFlow>().easyEditor == false)
        {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity) && currentMode == ModeElm.Hand)
            {
                if (hit.collider.gameObject.CompareTag("Target") || hit.collider.gameObject.CompareTag("Tip"))
                {
                    InitTargetPointerCollor();
                    target = hit.collider.gameObject;
                    TargetPointerCollor(target);
                }
                else if (hit.collider.gameObject.CompareTag("Hand_Pointer"))
                {
                    TargetToPointer(hit.collider.gameObject.transform.position);
                }
                else
                {
                    target = null;
                }
            }
        }

        if (follow && target != null && currentMode == ModeElm.Hand && manager.GetComponent<EditorFlow>().easyEditor == false)
        {
            FollowMouse(0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            follow = !follow;
        }

        if (preFollow == true && follow == false)
        {
            InitTargetPointerCollor();
        }

        if (currentMode == ModeElm.Finger)
        {
            if (manager.GetComponent<EditorFlow>().editorSequences[manager.GetComponent<EditorFlow>().editorSequences.Count - 1] == EditorFlow.FlowElm.LeftFinger)
            {
                LeftFingerInput();
            }
            else if (manager.GetComponent<EditorFlow>().editorSequences[manager.GetComponent<EditorFlow>().editorSequences.Count - 1] == EditorFlow.FlowElm.RightFinger)
            {
                RightFingerInput();
            }
        }

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    currentMode = ModeElm.Finger;
        //    Invoke("ReturnHandEdit", fingerTime);
        //}

        preFollow = follow;
    }

    private void TargetToPointer(Vector3 position)
    {
        target.transform.position = position;
    }

    private void RightFingerInput()
    {
        for (int i = 5; i < 10; i++)
        {
            if (Input.GetKey(fingerKeys[i]))
            {
                BendFinger(i);
            }
            else
            {
                StrethFinger(i);
            }
        }

    }

    private void LeftFingerInput()
    {
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKey(fingerKeys[i]))
            {
                BendFinger(i);
            }
            else
            {
                StrethFinger(i);
            }
        }

    }

    private void BendFinger(int index)
    {
        slider[index].value = -1;
    }
    private void StrethFinger(int index)
    {
        slider[index].value = 1;
    }
    private void LateUpdate()
    {
        actor.GetComponent<IKDummy>().GetPose();

        actor.GetComponent<IKDummy>().SetHandValue(56, -slider[0].value);
        actor.GetComponent<IKDummy>().SetHandValue(57, slider[0].value);
        actor.GetComponent<IKDummy>().SetHandValue(58, slider[0].value);

        actor.GetComponent<IKDummy>().SetHandValue(60, -slider[1].value * 2);
        actor.GetComponent<IKDummy>().SetHandValue(61, slider[1].value);
        actor.GetComponent<IKDummy>().SetHandValue(62, slider[1].value);

        actor.GetComponent<IKDummy>().SetHandValue(64, -slider[2].value * 4);
        actor.GetComponent<IKDummy>().SetHandValue(65, slider[2].value);
        actor.GetComponent<IKDummy>().SetHandValue(66, slider[2].value);

        actor.GetComponent<IKDummy>().SetHandValue(68, slider[3].value);
        actor.GetComponent<IKDummy>().SetHandValue(69, slider[3].value);
        actor.GetComponent<IKDummy>().SetHandValue(70, slider[3].value);

        actor.GetComponent<IKDummy>().SetHandValue(72, slider[4].value);
        actor.GetComponent<IKDummy>().SetHandValue(73, slider[4].value);
        actor.GetComponent<IKDummy>().SetHandValue(74, slider[4].value);

        actor.GetComponent<IKDummy>().SetHandValue(76, -slider[5].value);
        actor.GetComponent<IKDummy>().SetHandValue(77, slider[5].value);
        actor.GetComponent<IKDummy>().SetHandValue(78, slider[5].value);

        actor.GetComponent<IKDummy>().SetHandValue(80, -slider[6].value * 2);
        actor.GetComponent<IKDummy>().SetHandValue(81, slider[6].value);
        actor.GetComponent<IKDummy>().SetHandValue(82, slider[6].value);

        actor.GetComponent<IKDummy>().SetHandValue(84, -slider[7].value * 4);
        actor.GetComponent<IKDummy>().SetHandValue(85, slider[7].value);
        actor.GetComponent<IKDummy>().SetHandValue(86, slider[7].value);

        actor.GetComponent<IKDummy>().SetHandValue(88, slider[8].value);
        actor.GetComponent<IKDummy>().SetHandValue(89, slider[8].value);
        actor.GetComponent<IKDummy>().SetHandValue(90, slider[8].value);

        actor.GetComponent<IKDummy>().SetHandValue(92, slider[9].value);
        actor.GetComponent<IKDummy>().SetHandValue(93, slider[9].value);
        actor.GetComponent<IKDummy>().SetHandValue(94, slider[9].value);

        actor.GetComponent<IKDummy>().SetPose();

        //for (int i = 1; i < 5; i++)
        //{
        //    fingersJoint[i].transform.Rotate(-90, 0, 0);
        //}
        //for (int i = 6; i < 10; i++)
        //{
        //    fingersJoint[i].transform.Rotate(-90, 0, 0);
        //}

        FixFingerJoint(fingersJoint);
    }

    public void FixFingerJoint(List<GameObject> objs)
    {
        objs[0].transform.Rotate(45, 0, 0);
        //objs[5].transform.Rotate(-90, 90, 0);
        for (int i = 1; i < 5; i++)
        {
            objs[i].transform.Rotate(-90, 0, 0);
        }
        for (int i = 6; i < 10; i++)
        {
            objs[i].transform.Rotate(-90, 0, 0);
        }
    }

    public void ResetFingerJoint()
    {
        for (int i = 1; i < 5; i++)
        {
            fingersJoint[i].transform.Rotate(90, 0, 0);
        }
        for (int i = 6; i < 10; i++)
        {
            fingersJoint[i].transform.Rotate(90, 0, 0);
        }
    }

    void ReturnHandEdit()
    {
        currentMode = ModeElm.Hand;
        fingerCnt = 0;
        PrintFingerCount();
    }


    void FollowMouse(int index)
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = surface.transform.position.z;
        if (target.transform.CompareTag("Target"))
        {
            mousePos.z = surface.transform.position.z;
        }
        else if (target.transform.CompareTag("Tip"))
        {
            mousePos.z = surface.transform.position.z - 0.5f;
        }
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);
        //targetPos.z = surface.transform.position.z;
        //if (target.transform.CompareTag("Tip"))
        //{
        //    if ((target.transform.parent.transform.position - targetPos).magnitude < 0.5f)
        //    {
        //        target.transform.position = Vector3.Lerp(target.transform.position, targetPos, 0.1f);
        //    }
        //}
        //else
        //{
            target.transform.position = Vector3.Lerp(target.transform.position, targetPos, 0.1f);
        //}
        //target[0].transform.position = targetPos;
    }

    void PrintFingerCount()
    {
        fingerTimer.text = "Count Down:" + ((int)(fingerTime - fingerCnt)).ToString();
    }

    public void TargetPointerCollor(GameObject targetPointer)
    {
        try
        {
            Color temp = new Color(Color.red.r, Color.red.g, Color.red.b, targetPointer.GetComponent<MeshRenderer>().material.color.a);
            targetPointer.GetComponent<MeshRenderer>().material.color = temp;
        }
        catch
        {
        }
    }

    void InitTargetPointerCollor()
    {
        foreach (GameObject target in targets)
        {
            Color temp = new Color(Color.gray.r, Color.gray.g, Color.gray.b, target.GetComponent<MeshRenderer>().material.color.a);
            target.GetComponent<MeshRenderer>().material.color = temp;
        }
    }

    public void RemoveKeyFlame()
    {
        manager.GetComponent<LocalMotion>().localMotion.Poses.RemoveAt(manager.GetComponent<LocalMotion>().localMotion.Poses.Count - 1);
        manager.GetComponent<AnimationManager>().motion.currentMotion = manager.GetComponent<LocalMotion>().localMotion;
    }

    public void AddPoseButton(int poseNum)
    {
        GameObject temp = Resources.Load<GameObject>("SelectPose_Button");
        GameObject inst = Instantiate(temp);
        inst.GetComponent<RectTransform>().position = new Vector3(360 + (80 * poseNum - 1), 680, 0);
        inst.transform.parent = inputCanves.transform;
        temp.GetComponent<DefPoseNum>().num = poseNum;
        manager.GetComponent<PoseSelector>().ButtonColor(poseNum, inst);
    }
}
