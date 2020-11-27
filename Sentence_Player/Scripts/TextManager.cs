using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class TextManager : MonoBehaviour
{
    [SerializeField]
    InputField textInputField;
    [SerializeField]
    GameObject manager;
    [SerializeField]
    MotionData motion;
    [SerializeField]
    public GameObject playButton;
    List<string> phrases = new List<string>();
    [SerializeField]
    public GameObject loadingCanvas;
    [SerializeField]
    public GameObject searchButton;
    public List<string> valueResult;
    [SerializeField]
    public Text result;
    [SerializeField]
    public GameObject confirmButton;
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
    }

    public void ClearMotion()
    {
        motion.currentMotion.Poses = new List<MotionData.Pose>();
        playButton.SetActive(false);
    }

    public void StartSearch()
    {
        ClearMotion();
        confirmButton.SetActive(false);
        loadingCanvas.SetActive(true);
        searchButton.GetComponent<Button>().interactable = false;
        motion.currentMotion.Poses = new List<MotionData.Pose>();
        string temp = textInputField.text;
        temp = Regex.Replace(temp, @"\s+", " ");
        temp = temp.TrimEnd().TrimStart().Replace(' ', ',');
        valueResult = new List<string>();
        for (int i = 0; i < temp.Split(',').Length; i++) {
            valueResult.Add(temp.Split(',')[i]);
        }
        if (valueResult.Count == 0 || textInputField.text.TrimEnd().Length == 0) {
            loadingCanvas.SetActive(false);
            searchButton.GetComponent<Button>().interactable = true;
            return;
        }
        StartCoroutine(manager.GetComponent<MysqlClient>().SearchText(temp));
    }

    void Cut()
    {
        string[] temp = textInputField.text.Split(' ');
        for (int i = 0; i < temp.Length; i ++)
        {
            phrases.Add(temp[i]);
        }
    }

    public void SearchText()
    {
        StartCoroutine(GetText());
    }
    public IEnumerator GetText() {
        ClearMotion();
        this.GetComponent<MotionData>().loadedMotionSentenceCnt = new List<int>();
        string str = "";
        for (int i = 0; i < manager.GetComponent<MysqlClient>().dropdowns.Count; i++) {
            Dropdown temp = manager.GetComponent<MysqlClient>().dropdowns[i].GetComponent<Dropdown>();
            if (temp.options.Count == 0) {
                continue;
            }
            str += temp.options[temp.value].text;
            if (i == manager.GetComponent<MysqlClient>().dropdowns.Count - 1) {
                break;
            }
            str += ",";
        }
        yield return StartCoroutine(manager.GetComponent<MysqlClient>().SearchPhrase(str));
        motion.currentMotion.Poses.Insert(0, defaultMotion.Poses[0]);
        motion.currentMotion.Poses.Add(defaultMotion.Poses[0]);
    }
}
