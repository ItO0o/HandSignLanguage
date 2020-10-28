using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    List<int> poseIds = new List<int>();
    string POSE_TABLE = "poses";
    [SerializeField]
    GameObject manager;

    MotionData motionBuffer;

    [SerializeField]
    GameObject wordInput;
    [SerializeField]
    GameObject loadingCanvas;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator RecordPhrase()
    {
        MysqlManager mysql = new MysqlManager();
        MySqlConnection con = mysql.CreateConnection("signlangwiki");

         string cmdStr = "insert into " + "phrases" + " (name, move1) values ('";

        cmdStr += wordInput.GetComponent<InputField>().text + "',1)";
        Debug.Log(cmdStr);
        MySqlCommand cmd = new MySqlCommand(cmdStr, con);

        IAsyncResult iAsync = cmd.BeginExecuteNonQuery();
        while (!iAsync.IsCompleted)
        {
            yield return 0;
        }

    }

    public IEnumerator RecordMotion()
    {
        MysqlManager mysql = new MysqlManager();
        MySqlConnection con = mysql.CreateConnection("signlanguage");

        string cmdStr = "insert into " + "phrases" + "(id, ";

        for (int i = 0; i < motionBuffer.currentMotion.Poses.Count; i++)
        {
            cmdStr += "key_flames" + (i + 1);
            if (i != motionBuffer.currentMotion.Poses.Count - 1)
            {
                cmdStr += ", ";
            }
        }

        cmdStr += ") values (" + GetPhraseCount() + ", ";

        for (int i = 0; i < poseIds.Count; i++)
        {
            cmdStr += poseIds[i];
            if (i != poseIds.Count - 1)
            {
                cmdStr += ", ";
            }
        }
        cmdStr += ");";

        MySqlCommand cmd = new MySqlCommand(cmdStr, con);

        IAsyncResult iAsync = cmd.BeginExecuteNonQuery();
        while (!iAsync.IsCompleted)
        {
            yield return 0;
        }
    }

    public void ResistHandSingLang()
    {
        if (wordInput.GetComponent<InputField>().text.Equals("")) {
            manager.GetComponent<ValueUI>().NoneInputWordError();
        } else {
            loadingCanvas.SetActive(true);
            StartCoroutine(manager.GetComponent<MysqlClient>().SetPoses(manager.GetComponent<LocalMotion>().motion, wordInput.GetComponent<InputField>().text));
        }
    }

    public IEnumerator RecordPose(int index)
    {
        MysqlManager mysql = new MysqlManager();
        MySqlConnection con = mysql.CreateConnection("signlanguage");

        string cmdStr = "insert into " + POSE_TABLE + " (id,pose_name,";

        for (int i = 0; i < HumanTrait.MuscleName.Length; i++)
        {
            string temp = HumanTrait.MuscleName[i].Replace(" ", "_");
            temp = temp.Replace("-", "_");
            cmdStr += temp;
            if (i != HumanTrait.MuscleName.Length - 1)
            {
                cmdStr += ", ";
            }
        }
        int poseId = GetPoseCount();
        poseIds.Add(poseId);
        cmdStr += ") values (" + poseId + ", '',  ";

        //MotionData.Motion poseBuffer = position
        for (int i = 0; i < HumanTrait.MuscleName.Length; i++)
        {
            float temp = motionBuffer.currentMotion.Poses[index].muscleValue[i];
            //float temp = (float)actor.GetComponent<GetMusle>().humanPose.muscles[i];
            //if (Mathf.Abs(temp) < zero)
            //{
            //    cmdStr += "0";
            //}
            //else
            //{
            //temp = 0;
            cmdStr += temp.ToString();
            //}
            if (i != HumanTrait.MuscleName.Length - 1)
            {
                cmdStr += ", ";
            }
        }

        cmdStr += ");";

        MySqlCommand cmd = new MySqlCommand(cmdStr, con);

        IAsyncResult iAsync = cmd.BeginExecuteNonQuery();
        while (!iAsync.IsCompleted)
        {
            yield return 0;
        }
    }

    int GetPoseCount()
    {
        MysqlManager sql = new MysqlManager();
        MySqlConnection con = sql.CreateConnection("signlanguage");
        MySqlCommand cmd = new MySqlCommand("select count(*) from poses", con);
        var cnt = cmd.ExecuteScalar();
        return int.Parse(cnt.ToString());
    }

    int GetMotionCount()
    {
        MysqlManager sql = new MysqlManager();
        MySqlConnection con = sql.CreateConnection("signlanguage");
        MySqlCommand cmd = new MySqlCommand("select count(*) from phrases", con);
        var cnt = cmd.ExecuteScalar();
        return int.Parse(cnt.ToString());
    }

    int GetPhraseCount()
    {
        MysqlManager sql = new MysqlManager();
        MySqlConnection con = sql.CreateConnection("signlangwiki");
        MySqlCommand cmd = new MySqlCommand("select count(*) from phrases", con);
        var cnt = cmd.ExecuteScalar();
        return int.Parse(cnt.ToString());
    }
}
