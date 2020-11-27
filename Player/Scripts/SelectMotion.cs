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

    MotionData.Motion defaultMotion;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(SetDefault());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator SetDefault() {
       yield return StartCoroutine(this.GetComponent<MysqlClient>().SearchPhrase("Default"));
       defaultMotion = this.GetComponent<MotionData>().currentMotion;
       //Debug.Log(defaultMotion.Poses.Count);
    }

    public void LoadAnimation()
    {
        StartCoroutine(GetAnimation());
    }

    public IEnumerator GetAnimation() {
        motion.currentMotion.Poses = new List<MotionData.Pose>();
        string temp = label.GetComponent<Text>().text.Split('_')[0];
        yield return StartCoroutine(this.GetComponent<MysqlClient>().GetAnimation(int.Parse(temp)));
        motion.currentMotion.Poses.Insert(0, defaultMotion.Poses[0]);
        motion.currentMotion.Poses.Add(defaultMotion.Poses[0]);
    }

    public void StartMotion()
    {
        anime.currentState = AnimationManager.StateElm.Playing;
        anime.sequence = 0;
        anime.interpolation = 0;
    }
}
