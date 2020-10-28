using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMotion : MonoBehaviour
{
    [SerializeField]
    MotionData motion;
    [SerializeField]
    //MysqlManager mysql;
    MysqlClient mysql;
    [SerializeField]
    Dropdown dropdown;
    [SerializeField]
    GameObject label;
    [SerializeField]
    AnimationManager anime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadAnimation()
    {
        motion.currentMotion.Poses = new List<MotionData.Pose>();
        string temp = label.GetComponent<Text>().text.Split('_')[0];
        mysql.DummyStartCoroutine("GetAnimation", int.Parse(temp));
    }

    public void StartMotion()
    {
        anime.currentState = AnimationManager.StateElm.Playing;
        anime.sequence = 0;
        anime.interpolation = 0;
    }
}
