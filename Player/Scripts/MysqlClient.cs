using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MysqlClient : MonoBehaviour
{
    //string serverAddress = "http://172.16.5.193/handsignlanguage_mysql_server.php";
    //string serverAddress = "http://set1.ie.aitech.ac.jp:18888/handsignlanguage_mysql_server.php";
    //public static string serverAddress = "http://192.168.1.15:80/handsignlanguage_mysql_server.php";
    //public static string serverAddress = "http://10.4.141.113:80/handsignlanguage_mysql_server.php";
    public static string serverAddress = "http://sawanolab.aitech.ac.jp/HandSignLanguage_Mysql_Server/handsignlanguage_mysql_server.php";
    public static int id;
    public static string motionName;
    public bool inRequestAnimation;
    [SerializeField]
    GameObject startButton, confirmButton;

    public void Start()
    {

    }

    [SerializeField]
    UIManager ui;
    [SerializeField]
    MotionData motion;

    List<int> motionsID = new List<int>();

    [SerializeField]
    GameObject dropdown,dropdwonsParent,lastDropdown;
    public List<GameObject> dropdowns;

    public int poseCount = 0;

    public void DummyStartCoroutine(string method, int id)
    {
        StartCoroutine(method, id);
    }

    public IEnumerator SearchText(string text) {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        //複数phpに送信したいデータがある場合は今回の場合dic.Add("hoge", value)のように足していけばよい
        dic.Add("text_search", text);
        yield return StartCoroutine(Post(serverAddress, dic));  // POST
    }

    public IEnumerator SearchPhrase(string text)
    {
        inRequestAnimation = true;
        Dictionary<string, string> dic = new Dictionary<string, string>();
        //複数phpに送信したいデータがある場合は今回の場合dic.Add("hoge", value)のように足していけばよい
        dic.Add("phrase_search", text);
        yield return StartCoroutine(Post(serverAddress, dic));  // POST
        try {
            this.GetComponent<TextManager>().loadingCanvas.SetActive(false);
            this.GetComponent<TextManager>().searchButton.GetComponent<Button>().interactable = true;
        } catch { }
        //yield return 0;
    }

    public IEnumerator GetPhraseList()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        //複数phpに送信したいデータがある場合は今回の場合dic.Add("hoge", value)のように足していけばよい
        dic.Add("list", 0.ToString());
        StartCoroutine(Post(serverAddress, dic));  // POST

        yield return 0;
    }

    public IEnumerator GetAnimation(int id)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        motionsID = new List<int>();
        dic.Add("motion", id.ToString());
        yield return StartCoroutine(Post(serverAddress, dic));  // POST
        yield return 0;
    }

    private void ByeLoading() {
        this.GetComponent<TextManager>().loadingCanvas.SetActive(false);
        this.GetComponent<TextManager>().searchButton.GetComponent<Button>().interactable = true;
    }

    private IEnumerator GetPose(int id)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        //dic.Add("id", InputText_.GetComponent<Text>().text);  //インプットフィールドからidの取得);
        //複数phpに送信したいデータがある場合は今回の場合dic.Add("hoge", value)のように足していけばよい
        dic.Add("pose", id.ToString());
        yield return StartCoroutine(Post(serverAddress, dic));  // POST

        yield return 0;
    }

    public IEnumerator GetPoseCount()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("get_pose_count", 0.ToString());

        StartCoroutine(Post(serverAddress, dic));
        yield return 0;
    }

    public IEnumerator SetPoses(MotionData motion, string name)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        string temp = name + "#";
        for (int j = 0; j < motion.currentMotion.Poses.Count; j++)
        {
            for (int i = 0; i < HumanTrait.MuscleCount; i++)
            {
                temp += motion.currentMotion.Poses[j].muscleValue[i].ToString();
                if (i >= HumanTrait.MuscleCount - 1)
                {
                    break;
                }
                temp += ",";
            }
            if (j >= motion.currentMotion.Poses.Count - 1)
            {
                break;
            }
            temp += "#";
        }
        dic.Add("set_pose", temp);

        StartCoroutine(Post(serverAddress, dic));
        yield return 0;
    }

    private IEnumerator Post(string url, Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }
        WWW www = new WWW(url, form);

        yield return StartCoroutine(CheckTimeOut(www, 10f)); //TimeOutSecond = 3s;

        if (www.error != null)
        {
            Debug.Log("HttpPost NG: " + www.error);
            //そもそも接続ができていないとき

        }
        else if (www.isDone)
        {
            //送られてきたデータをテキストに反映
            //ResultText_.GetComponent<Text>().text = www.text;
            if (post.ContainsKey("list"))
            {
                Debug.Log(www.text);
                string[] tempArray = www.text.Split(',');
                List<string> tempList = new List<string>();
                for (int i = 0; i < tempArray.Length; i++)
                {
                    tempList.Add(tempArray[i]);
                }
                ui.phrases = tempList;
            }
            if (post.ContainsKey("pose"))
            {
                List<float> muscles = new List<float>();

                string[] tempArray = www.text.Split(',');
                List<string> tempList = new List<string>();
                for (int i = 2; i < tempArray.Length; i++)
                {
                    //Debug.Log(tempArray[i]);
                    try
                    {
                        muscles.Add(float.Parse(tempArray[i]));
                    }
                    catch { }
                }
                MotionData.Pose pose = new MotionData.Pose();
                pose.muscleValue = muscles;
                motion.currentMotion.Poses.Add(pose);
                if(inRequestAnimation == true)
                {
                    inRequestAnimation = false;
                    startButton.SetActive(true);
                }
            }
            if (post.ContainsKey("motion"))
            {
                string[] tempArray = www.text.Split(',');
                int cnt = 0;
                for (int i = 0; i < tempArray.Length; i++)
                {
                    if (tempArray[i].Length > 0) {
                        cnt++;
                        yield return StartCoroutine("GetPose",int.Parse(tempArray[i]));
                    }
                }
                this.GetComponent<MotionData>().loadedMotionSentenceCnt.Add(cnt);
            }
            if (post.ContainsKey("set_pose"))
            {
                Debug.Log("regist:" + www.text);
                SetStatic(www.text);
                SceneManager.LoadScene("Confirmation");
            }
            if (post.ContainsKey("get_pose_count"))
            {
                Debug.Log(www.text);
                poseCount = int.Parse(www.text);
            }
            if (post.ContainsKey("phrase_search"))
            {
                if (www.text.Length > 0) {
                    Debug.Log(www.text);
                    string[] temp = www.text.Split(',');
                    //string[] temp = {"4","6"};
                    string value = "";
                    for (int i = 0; i < temp.Length; i++) {
                        int id = int.Parse(temp[i]);
                        yield return StartCoroutine("GetAnimation", id);
                    }
                    List<string> currentTxt = new List<string>();
                    for (int i = 0; i < dropdowns.Count; i++) {
                        if (dropdowns[i].GetComponent<Dropdown>().options.Count > 0) {
                            currentTxt.Add(dropdowns[i].GetComponent<Dropdown>().options[dropdowns[i].GetComponent<Dropdown>().value].text);
                        } else {
                            continue;
                        }
                        //value += "<color=#00ff00>" + currentTxt + "　　　</color>";
                    }
                    //this.GetComponent<TextManager>().result.text = value;
                    try {
                        this.GetComponent<MotionTextSync>().currentTexts = currentTxt;
                        this.GetComponent<MotionTextSync>().SetText();
                    } catch { }
                }
            }
            if (post.ContainsKey("text_search")) {
                Debug.Log(www.text);
                string[] temp = www.text.Split('*');
                dropdowns = new List<GameObject>();
                for (int i = 0; i < dropdowns.Count;i++) {
                    Destroy(dropdowns[i].gameObject);
                }
                for (int i = 0; i < temp.Length; i++) {
                    string[] cand = temp[i].Split('+');
                    GameObject tempObj;
                    if (i == temp.Length - 1) {
                        tempObj = Instantiate<GameObject>(lastDropdown);
                    } else {
                        tempObj = Instantiate<GameObject>(dropdown);
                    }
                    dropdowns.Add(tempObj);
                    tempObj.transform.parent = dropdwonsParent.transform;
                    tempObj.GetComponent<RectTransform>().position = new Vector3(120,650 - 67 * i, 0);
                    confirmButton.GetComponent<RectTransform>().position = new Vector3(120, 670 - 67 * (i + 1), 0);
                    for (int j = 0; j < cand.Length; j++) {
                        string str = cand[j];
                        if (str.Equals("-1")) {
                            continue;
                        } else {
                            tempObj.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData { text = str });
                        }
                    }
                }
                this.GetComponent<TextManager>().loadingCanvas.SetActive(false);
                this.GetComponent<TextManager>().confirmButton.SetActive(true);
            }
        }
        yield return 0;
    }


    private void SetStatic(string str)
    {
        MysqlClient.id = int.Parse(str.Split('(')[2].Split(',')[0]);
        MysqlClient.motionName = str.Split('(')[2].Split(',')[1].Split('\'')[1];
    }

    private IEnumerator CheckTimeOut(WWW www, float timeout)
    {
        float requestTime = Time.time;

        while (!www.isDone)
        {
            if (Time.time - requestTime < timeout)
                yield return null;
            else
            {
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
