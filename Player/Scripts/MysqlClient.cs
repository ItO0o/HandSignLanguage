using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysqlClient : MonoBehaviour {
    //string serverAddress = "http://172.16.5.193/handsignlanguage_mysql_server.php";
    //string serverAddress = "http://set1.ie.aitech.ac.jp:18888/handsignlanguage_mysql_server.php";
    //string serverAddress = "http://172.16.0.73:80/handsignlanguage_mysql_server.php";
    public static string serverAddress = "http://sawanolab.aitech.ac.jp/HandSignLanguage_Mysql_Server/handsignlanguage_mysql_server.php";

    [SerializeField]

    public void Start() {
        
    }

    [SerializeField]
    UIManager ui;
    [SerializeField]
    MotionData motion;

    List<int> motionsID = new List<int>();
    public int poseCount = 0;

    public void DummyStartCoroutine(string method, int id) {
        StartCoroutine(method, id);
    }

    public IEnumerator GetPhraseList () {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        //複数phpに送信したいデータがある場合は今回の場合dic.Add("hoge", value)のように足していけばよい
        dic.Add("list", 0.ToString());
        StartCoroutine(Post(serverAddress, dic));  // POST

        yield return 0;
    }

    private IEnumerator GetAnimation(int id) {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        motionsID = new List<int>();
        dic.Add("motion", id.ToString());
        StartCoroutine(Post(serverAddress, dic));  // POST
        yield return 0;
    }

    private IEnumerator GetPose(int id) {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        //dic.Add("id", InputText_.GetComponent<Text>().text);  //インプットフィールドからidの取得);
        //複数phpに送信したいデータがある場合は今回の場合dic.Add("hoge", value)のように足していけばよい
        dic.Add("pose", id.ToString());
        StartCoroutine(Post(serverAddress, dic));  // POST

        yield return 0;
    }

    public IEnumerator GetPoseCount() {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("get_pose_count", 0.ToString());

        StartCoroutine(Post(serverAddress, dic));
        yield return 0;
    }

    public IEnumerator SetPoses(MotionData motion,string name) {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        string temp = name + "#";
        for (int j = 0; j < motion.currentMotion.Poses.Count; j++) {
            for (int i = 0; i < HumanTrait.MuscleCount; i++) {
                temp += motion.currentMotion.Poses[j].muscleValue[i].ToString();
                if (i >= HumanTrait.MuscleCount - 1) {
                    break;
                }
                temp += ",";
            }
            if (j >= motion.currentMotion.Poses.Count - 1) {
                break;
            }
            temp += "#";
        }
        dic.Add("set_pose", temp);

        StartCoroutine(Post(serverAddress, dic));
        yield return 0;
    }

    private IEnumerator Post(string url, Dictionary<string, string> post) {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, string> post_arg in post) {
            form.AddField(post_arg.Key, post_arg.Value);
        }
        WWW www = new WWW(url, form);

        yield return StartCoroutine(CheckTimeOut(www, 10f)); //TimeOutSecond = 3s;

        if (www.error != null) {
            Debug.Log("HttpPost NG: " + www.error);
            //そもそも接続ができていないとき

        } else if (www.isDone) {
            //送られてきたデータをテキストに反映
            //ResultText_.GetComponent<Text>().text = www.text;
            if (post.ContainsKey("list")) {
                Debug.Log(www.text);
                string[] tempArray = www.text.Split(',');
                List<string> tempList= new List<string>();
                for(int i = 0; i < tempArray.Length; i++) {
                    tempList.Add(tempArray[i]);
                }
                ui.phrases = tempList;
            }
            if (post.ContainsKey("pose")) {
                List<float> muscles = new List<float>();

                string[] tempArray = www.text.Split(',');
                List<string> tempList = new List<string>();
                for (int i = 2; i < tempArray.Length; i++) {
                    Debug.Log(tempArray[i]);
                    try {
                        muscles.Add(float.Parse(tempArray[i]));
                    } catch { }
                }
                MotionData.Pose pose = new MotionData.Pose();
                pose.muscleValue = muscles;
                motion.currentMotion.Poses.Add(pose);
            }
            if (post.ContainsKey("motion")) {
                string[] tempArray = www.text.Split(',');
                for (int i = 0; i < tempArray.Length; i++) {
                    try {
                        //motionsID.Add(int.Parse(tempArray[i]));
                        StartCoroutine(GetPose(int.Parse(tempArray[i])));
                    } catch { }
                }
            }
            if (post.ContainsKey("set_pose")) {
                Debug.Log("regist:" + www.text);
            }
            if (post.ContainsKey("get_pose_count")) {
                Debug.Log(www.text);
                poseCount = int.Parse(www.text);
            }
        }
    }

    private IEnumerator CheckTimeOut(WWW www, float timeout) {
        float requestTime = Time.time;

        while (!www.isDone) {
            if (Time.time - requestTime < timeout)
                yield return null;
            else {
                Debug.Log("TimeOut");  //タイムアウト
                //タイムアウト処理
                //
                //
                break;
            }
        }
        yield return null;
    }
}
