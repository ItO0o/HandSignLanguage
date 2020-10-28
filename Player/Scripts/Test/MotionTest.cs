using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionTest : MonoBehaviour
{
    [SerializeField]
    MotionData motion;
    [SerializeField]
    //MysqlManager mysql;
    MysqlClient mysql;
    [SerializeField]
    AnimationManager anime;
    // Start is called before the first frame update
    void Start()
    {

        string cmdStr = "";
        for (int i = 0; i < HumanTrait.MuscleName.Length; i++) {
            string temp = HumanTrait.MuscleName[i].Replace(" ", "_");
            temp = temp.Replace("-", "_");
            cmdStr += temp;
            if (i != HumanTrait.MuscleName.Length - 1) {
                cmdStr += ", ";
            }
        }
        Debug.Log(cmdStr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test() {
        //motion.currentMotion.Poses = new List<MotionData.Pose>();
        //mysql.DummyStartCoroutine("GetMotion", 1);
        //anime.currentState = AnimationManager.StateElm.Playing;
        //anime.sequence = 0;
        //anime.interpolation = 0;
    }
}
