using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        motion.currentMotion.Poses = new List<MotionData.Pose>();
        string temp = textInputField.text.Replace(' ',',');
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
