using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public enum ModeElm
    {
        Edit,
        Play,
        Loop,
    }

    [SerializeField]
    public MotionData motion;


    [SerializeField]
    public Animator animator;
    HumanPose humanPose;
    HumanPoseHandler handler;

    [SerializeField]
    public ModeElm currentElm;

    public enum StateElm
    {
        Playing,
        Stop
    }

    public StateElm currentState;
    // Start is called before the first frame update
    void Start()
    {
        currentState = StateElm.Stop;
        //humanPose = new HumanPose();
        handler = new HumanPoseHandler(animator.avatar, animator.transform);
        //handler.GetHumanPose(ref humanPose);
        //ChangeAnimator(animator);
        //handler.SetHumanPose(ref humanPose);
    }

    // Update is called once per frame
    //void LateUpdate()
    //{
    //    if (currentElm == ModeElm.Play && currentState == StateElm.Playing && motion.currentMotion.Poses.Count > 0)
    //    {
    //        MuscleAnimation();
    //    }
    //}

    private void FixedUpdate()
    {
        if (currentElm == ModeElm.Play && currentState == StateElm.Playing && motion.currentMotion.Poses.Count > 1)
        {
            MuscleAnimation();
        }
        if (currentElm == ModeElm.Loop)
        {
            ResetAnime();
        }
    }

    [SerializeField]
    bool autoSeq;
    public float interpolation = 0;
    public int sequence = 0;
    [SerializeField]
    public float speed = 1;

    public void ResetAnime()
    {
        sequence = 0;
        interpolation = 0;
        currentElm = ModeElm.Play;
    }

    /// <summary>
    /// Muscleの値を計算して反映
    /// </summary>
    public void MuscleAnimation()
    {
        //次のポーズへ遷移
        if (interpolation >= 1 && autoSeq)
        {
            interpolation = 0;
            sequence++;
        }
        //最後までアニメーションさせたら実行しない
        if (sequence + 1 >= motion.currentMotion.Poses.Count || motion.currentMotion.Poses.Count == 0)
        {
            currentState = StateElm.Stop;
            return;
        }
        //全筋肉分計算
        for (int i = 0; i < humanPose.muscles.Length; i++)
        {
            float current = motion.currentMotion.Poses[sequence].muscleValue[i];
            float next = motion.currentMotion.Poses[sequence + 1].muscleValue[i];
            //一応躍度の公式をぶち込む
            humanPose.muscles[i] = current + (current - next) * (15 * (Mathf.Pow(interpolation, 4)) + (-6) * (Mathf.Pow(interpolation, 5)) + (-10) * (Mathf.Pow(interpolation, 3)));
        }
        interpolation += speed;
        //モデルに反映
        handler.SetHumanPose(ref humanPose);
    }

    public void LastPose()
    {
        for (int i = 0; i < humanPose.muscles.Length; i++)
        {
            //一応躍度の公式をぶち込む
            humanPose.muscles[i] = motion.currentMotion.Poses[motion.currentMotion.Poses.Count - 1].muscleValue[i];
        }
        handler.SetHumanPose(ref humanPose);
    }

    /// <summary>
    /// 作動モデルのアバターを更新
    /// </summary>
    /// <param name="animator"></param>
    public void ChangeAnimator(Animator animator)
    {
        humanPose = new HumanPose();
        handler = new HumanPoseHandler(animator.avatar, animator.transform);
        handler.GetHumanPose(ref humanPose);
    }
}
