using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionData : MonoBehaviour
{
    [SerializeField]
    //MysqlManager mysql;
    MysqlClient mysql;
    [SerializeField]
    bool network;
    public struct Pose
    {
        public List<float> muscleValue;
    }

    public struct Motion
    {
        public List<Pose> Poses;
    }

    public Motion currentMotion;

    public List<int> loadedMotionSentenceCnt;

    // Start is called before the first frame update
    void Start()
    {
        loadedMotionSentenceCnt = new List<int>();
        if (network == true)
        {
            mysql.DummyStartCoroutine("GetAnimation", 1);
            currentMotion.Poses = new List<Pose>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
