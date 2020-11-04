using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject manager;

    public List<string> phrases;
    [SerializeField]
    //MysqlManager mysql;
    MysqlClient mysql;

    [SerializeField]
    Dropdown phrasesDropdown;
    int tempPhraseCnt;

    [SerializeField]
    Dropdown modelsDropdown;
    [SerializeField]
    Button chengeModelButton;

    [SerializeField]
    Dropdown posesDropdown;

    [SerializeField]
    AnimationManager anime;

    private enum Models
    {
        M3DFemale,
        //UnityChan
    }

    private GameObject[] modelsObj;
    private Models currentModel;
    [SerializeField]
    private Models defaultModel;
    public GameObject currentObj;

    const float speedWeight = 0.05f;
    [SerializeField]
    private Slider speedSlider;

    public bool poseRequest = true;

    [SerializeField]
    public Text seqText;

    // Start is called before the first frame update
    void Start()
    {
        //最初のポーズ一覧を反映させるため
        poseRequest = true;

        //フレーズリスト初期化
        phrases = new List<string>();
        //mysql.DummyStartCoroutine("GetPhraseList");

        mysql.DummyStartCoroutine("GetPhraseList", 0);

        //モデル変更ドロップダウンと変数の初期化
        int modelCnt = System.Enum.GetNames(typeof(Models)).Length;
        modelsObj = new GameObject[modelCnt];

        for (int i = 0; i < modelCnt; i++)
        {
            modelsObj[i] = GameObject.Find(System.Enum.GetNames(typeof(Models))[i]);
            modelsObj[i].SetActive(false);
            modelsDropdown.options.Insert(0, new Dropdown.OptionData { text = System.Enum.GetNames(typeof(Models))[i] });
        }

        //初期モデルをセット
        currentObj = modelsObj[(int)defaultModel];
        ModelSelector(defaultModel);

        manager.GetComponent<AnimationManager>().ChangeAnimator(currentObj.GetComponent<Animator>());

        SetAnimationSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        //フレーズリストが増えたら増えた分ドロップダウンに追加
        if (tempPhraseCnt < phrases.Count && null != phrasesDropdown)
        {
            int newCnt = phrases.Count - tempPhraseCnt;

            for (int i = 0; i < newCnt; i++)
            {
                int index = phrases.Count - 1 - i;
                //phrasesDropdown.options.Add(new Dropdown.OptionData { text = phrases[index] });
                phrasesDropdown.options.Insert(0, new Dropdown.OptionData { text = phrases[index] });
            }
            phrasesDropdown.RefreshShownValue();
        }

        if (poseRequest && manager.GetComponent<MotionData>().currentMotion.Poses.Count > 0)
        {
            SetPoseDropdown();
            poseRequest = false;
        }

        PrintSeq();

        //前フレーム保持場所
        tempPhraseCnt = phrases.Count;
    }

    /// <summary>
    /// ドロップダウンの値からモデルを変更
    /// </summary>
    public void ChangeModel()
    {
        if (TryParse(modelsDropdown.options[modelsDropdown.value].text, out currentModel))
        {
            ModelSelector(currentModel);
        }
    }

    /// <summary>
    /// 文字列から列挙型に変換
    /// </summary>
    /// <param name="s"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    bool TryParse(string s, out Models model)
    {
        return System.Enum.TryParse(s, out model) && System.Enum.IsDefined(typeof(Models), model);
    }

    /// <summary>
    /// モデル変更に伴う変更処理
    /// </summary>
    /// <param name="model"></param>
    void ModelSelector(Models model)
    {
        currentModel = model;
        currentObj.SetActive(false);
        currentObj = modelsObj[(int)model];
        currentObj.SetActive(true);
        //manager.GetComponent<AnimationManager>().animator = currentObj.GetComponent<Animator>();
        manager.GetComponent<AnimationManager>().ChangeAnimator(currentObj.GetComponent<Animator>());
    }

    /// <summary>
    /// アニメーション速度を入力
    /// </summary>
    public void SetAnimationSpeed()
    {
        manager.GetComponent<AnimationManager>().speed = speedSlider.value * speedWeight;
    }

    public void PoseRequestOn()
    {
        poseRequest = true;
    }

    public void SetPoseDropdown()
    {
        posesDropdown.options.Clear();
        for (int i = 0; i < manager.GetComponent<MotionData>().currentMotion.Poses.Count; i++)
        {
            posesDropdown.options.Add(new Dropdown.OptionData { text = i.ToString() });
        }
    }

    public void SetPose()
    {
        anime.currentState = AnimationManager.StateElm.Stop;
        anime.sequence = posesDropdown.value;
        anime.interpolation = 0;
        if (posesDropdown.value + 1 >= manager.GetComponent<MotionData>().currentMotion.Poses.Count)
        {
            anime.LastPose();
        }
        else
        {
            anime.MuscleAnimation();
        }
    }

    public void PlayFromPose()
    {
        anime.currentState = AnimationManager.StateElm.Playing;
        anime.sequence = posesDropdown.value;
        anime.interpolation = 0;
    }

    public void PrintSeq()
    {
        if (anime.currentState == AnimationManager.StateElm.Playing)
        {
            if (anime.sequence == manager.GetComponent<MotionData>().currentMotion.Poses.Count - 2)
            {
                seqText.text = anime.sequence + " => " + (anime.sequence + 1) + "(Last)";
            }
            else if (anime.sequence == 0)
            {
                seqText.text = "0(Entry)" + " => " + (anime.sequence + 1);
            }
            else
            {
                seqText.text = anime.sequence + " => " + (anime.sequence + 1);
            }
        }
    }
}
