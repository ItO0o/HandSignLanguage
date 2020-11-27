using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMotion : MonoBehaviour
{
    public MotionData.Motion localMotion;

    [SerializeField]
    Animator animator;
    [SerializeField]
    Animator actor;
    HumanPose humanPose;
    HumanPoseHandler handler;

    [SerializeField]
    GameObject manager;
    [SerializeField]
    public MotionData motion;

    InputUIManager.ModeElm preMode;

    [SerializeField]
    List<GameObject> targets;

    // Start is called before the first frame update
    void Start()
    {
        localMotion = new MotionData.Motion();
        localMotion.Poses = new List<MotionData.Pose>();

        manager.GetComponent<AnimationManager>().motion = new MotionData();
        manager.GetComponent<AnimationManager>().motion.currentMotion = new MotionData.Motion();
        manager.GetComponent<AnimationManager>().motion.currentMotion.Poses = new List<MotionData.Pose>();

        manager.GetComponent<AnimationManager>().ChangeAnimator(actor);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(manager.GetComponent<InputUIManager>().currentMode);
        if (manager.GetComponent<InputUIManager>().currentMode == InputUIManager.ModeElm.Play && preMode != InputUIManager.ModeElm.Play)
        {
            PlayMode();
        }
        //if (manager.GetComponent<InputUIManager>().currentMode != InputUIManager.ModeElm.Play && preMode == InputUIManager.ModeElm.Play)
        //{
        //    EditMode();
        //}

        preMode = manager.GetComponent<InputUIManager>().currentMode;
    }

    public void PlayMode()
    {
        animator.gameObject.SetActive(false);
        foreach (GameObject temp in targets)
        {
            temp.SetActive(false);
        }
    }

    public void EditMode()
    {
        animator.gameObject.SetActive(true);
        actor.gameObject.SetActive(false);
        foreach (GameObject temp in targets)
        {
            temp.SetActive(true);
        }
    }

    public void PlayMotion()
    {
        actor.gameObject.SetActive(true);
        manager.GetComponent<InputUIManager>().currentMode = InputUIManager.ModeElm.Play;
        manager.GetComponent<AnimationManager>().motion = motion;
        manager.GetComponent<AnimationManager>().motion.currentMotion = localMotion;
        manager.GetComponent<AnimationManager>().currentState = AnimationManager.StateElm.Playing;
        manager.GetComponent<AnimationManager>().ChangeAnimator(actor);
        manager.GetComponent<AnimationManager>().ResetAnime();

        manager.GetComponent<BackAnimation>().loop = false;
    }

    public void AddKeyFlame()
    {
        manager.GetComponent<InputUIManager>().ResetFingerJoint();
        handler = new HumanPoseHandler(animator.avatar, animator.transform);
        humanPose = new HumanPose();
        handler.GetHumanPose(ref humanPose);
        MotionData.Pose pose = new MotionData.Pose();
        pose.muscleValue = new List<float>();
        for (int i = 0; i < HumanTrait.MuscleCount; i++)
        {
            pose.muscleValue.Add(humanPose.muscles[i]);
        }
        localMotion.Poses.Add(pose);

        manager.GetComponent<AnimationManager>().motion.currentMotion = localMotion;
        manager.GetComponent<LocalMotion>().motion.currentMotion = localMotion;
    }
}
