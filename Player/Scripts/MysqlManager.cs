using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;

public class MysqlManager : MonoBehaviour
{
    string SERVER = "127.0.0.1";
    string DATABASE = "signlangwiki";
    string MOTION_DATABASE = "signlanguage";
    string USERID = "root";
    string PORT = "3306";
    string PASSWORD = "root";
    string PHRASE_TABLE = "phrases";
    string POSE_TABLE = "poses";

    //string SERVER = "set1.ie.aitech.ac.jp";
    //string DATABASE = "signlangwiki";
    //string MOTION_DATABASE = "signlanguage";
    //string USERID = "hoge";
    //string PORT = "43306";
    //string PASSWORD = "Hogehage01";
    //string PHRASE_TABLE = "phrases";
    //string POSE_TABLE = "poses";

    [SerializeField]
    UIManager ui;
    [SerializeField]
    MotionData motion;
    public int columnCnt;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DummyStartCoroutine(string method)
    {
        StartCoroutine(method);
    }
    public void DummyStartCoroutine(string method, int id)
    {
        StartCoroutine(method, id);
    }


    public IEnumerator GetPhraseList()
    {
        MySqlConnection con = CreateConnection(DATABASE);

        string selCmd = "SELECT * FROM " + PHRASE_TABLE + " LIMIT 0, 1200;";
        MySqlCommand cmd = new MySqlCommand(selCmd, con);

        IAsyncResult iAsync = cmd.BeginExecuteReader();

        while (!iAsync.IsCompleted)
        {
            yield return 0;
        }

        MySqlDataReader rdr = cmd.EndExecuteReader(iAsync);

        List<string> temp = new List<string>();

        if (rdr.HasRows)
        {
            while (rdr.Read())
            {
                string phrase = rdr.GetString(1);
                //Debug.Log(phrase);
                temp.Add(phrase);
            }
        }

        ui.phrases = temp;

        rdr.Close();
        con.Close();
    }

    public IEnumerator GetMotion(int id)
    {
        MySqlConnection con = CreateConnection(MOTION_DATABASE);

        string selCmd = "SELECT * FROM " + PHRASE_TABLE + " WHERE id = " + id + ";";
        MySqlCommand cmd = new MySqlCommand(selCmd, con);

        IAsyncResult iAsync = cmd.BeginExecuteReader();

        while (!iAsync.IsCompleted)
        {
            yield return 0;
        }

        MySqlDataReader rdr = cmd.EndExecuteReader(iAsync);

        List<string> temp = new List<string>();

        if (rdr.HasRows)
        {
            while (rdr.Read())
            {
                int i = 2;
                while (true)
                {
                    try
                    {
                        int value = rdr.GetInt16(i);
                        StartCoroutine("GetMuscleValues", value);
                    }
                    catch
                    {
                        break;
                    }
                    i++;
                }
            }
        }
        Debug.Log("Load Syuwa id:" + id);
        rdr.Close();
        con.Close();
    }

    //public IEnumerator RegistPose()
    //{
    //    string cmdStr = "insert into " + POSE_TABLE + " (id,pose_name,";

    //    StartCoroutine("GetColumn");

    //    for (int i = 0; i < HumanTrait.MuscleName.Length; i++)
    //    {
    //        string temp = HumanTrait.MuscleName[i].Replace(" ", "_");
    //        temp = temp.Replace("-", "_");
    //        cmdStr += temp;
    //        if (i != HumanTrait.MuscleName.Length - 1)
    //        {
    //            cmdStr += ", ";
    //        }
    //    }

    //    cmdStr += ") values (" + currentPoseID + ", 'Old',  ";

    //    for (int i = 0; i < HumanTrait.MuscleName.Length; i++)
    //    {
    //        float temp = (float)actor.GetComponent<GetMusle>().humanPose.muscles[i];
    //        //if (Mathf.Abs(temp) < zero)
    //        //{
    //        //    cmdStr += "0";
    //        //}
    //        //else
    //        //{
    //        //temp = 0;
    //        cmdStr += temp.ToString();
    //        //}
    //        if (i != HumanTrait.MuscleName.Length - 1)
    //        {
    //            cmdStr += ", ";
    //        }
    //    }

    //    cmdStr += ");";

    //    keyFlameID.Add(currentPoseID);
    //    currentPoseID++;

    //    Debug.Log(cmdStr);

    //    MySqlCommand cmd = new MySqlCommand(cmdStr, connection);

    //    IAsyncResult iAsync = cmd.BeginExecuteNonQuery();
    //    while (!iAsync.IsCompleted)
    //    {
    //        yield return 0;
    //    }
    //}

    public  IEnumerator GetColumn()
    {
        MySqlConnection con = CreateConnection(MOTION_DATABASE);
        MySqlCommand cmd = new MySqlCommand("select count(*) from poses", con);
        var cnt = cmd.ExecuteScalar();

        columnCnt = (int)cnt;

        IAsyncResult iAsync = cmd.BeginExecuteNonQuery();
        while (!iAsync.IsCompleted)
        {
            yield return 0;
        }
    }

    public IEnumerator GetMuscleValues(int id)
    {
        MySqlConnection con = CreateConnection(MOTION_DATABASE);

        string selCmd = "SELECT * FROM " + POSE_TABLE + " WHERE id = " + id + ";";
        MySqlCommand cmd = new MySqlCommand(selCmd, con);

        IAsyncResult iAsync = cmd.BeginExecuteReader();

        while (!iAsync.IsCompleted)
        {
            yield return 0;
        }

        MySqlDataReader rdr = cmd.EndExecuteReader(iAsync);

        if (rdr.HasRows)
        {
            while (rdr.Read())
            {
                int i = 2;
                List<float> muscles = new List<float>();
                while (true)
                {
                    try
                    {
                        float value = rdr.GetFloat(i);
                        muscles.Add(value);
                        //motion.currentMotion.Poses
                    }
                    catch
                    {
                        break;
                    }
                    i++;
                }
                MotionData.Pose pose = new MotionData.Pose();
                pose.muscleValue = muscles;
                motion.currentMotion.Poses.Add(pose);
            }
        }
        rdr.Close();
        con.Close();
    }

    public MySqlConnection CreateConnection(string database)
    {
        string conCmd = "Server=" + SERVER + ";Port=" + PORT + ";Database=" + database + ";Uid=" + USERID + ";Pwd=" + PASSWORD;
        MySqlConnection connection = null;
        try
        {
            connection = new MySqlConnection(conCmd);
            connection.Open();
        }
        catch (MySqlException ex)
        {
            Debug.Log(ex.ToString());
        }
        return connection;
    }
}
