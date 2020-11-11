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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearMotion()
    {
        motion.currentMotion.Poses = new List<MotionData.Pose>();
        playButton.SetActive(false);
    }

    public void StartSearch()
    {
        ClearMotion();
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
        StartCoroutine(manager.GetComponent<MysqlClient>().SearchPhrase(temp));
    }

    void Cut()
    {
        string[] temp = textInputField.text.Split(' ');
        for (int i = 0; i < temp.Length; i ++)
        {
            phrases.Add(temp[i]);
        }
    }

    void PrintTexts()
    {

    }
}
