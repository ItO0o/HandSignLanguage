using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotionTextSync : MonoBehaviour {
    public List<string> currentTexts;
    public List<GameObject> textObjs = new List<GameObject>();
    [SerializeField]
    public GameObject origText, canvas;
    [SerializeField]
    public GameObject playArrow, rightArrow;
    public int textSeq = 0;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        int currentPhrase = 0;
        bool transition = false;
        for (int i = 0; i < this.GetComponent<MotionData>().loadedMotionSentenceCnt.Count; i++) {
            int total = 0;
            for (int j = 0; j <= i; j++) {
                total += this.GetComponent<MotionData>().loadedMotionSentenceCnt[j];
            }
            if (total > this.GetComponent<AnimationManager>().sequence) {
                currentPhrase = i;
                break;
            } else if (total == this.GetComponent<AnimationManager>().sequence) {
                currentPhrase = i;
                transition = true;
                break;
            }
        }
        //try {
        //    Debug.Log("currentPhrase:" + currentPhrase);
        //} catch {

        //}
        if (textObjs != null && textObjs.Count > 0) {
            if (transition == false) {
                playArrow.GetComponent<RectTransform>().position = new Vector3(textObjs[currentPhrase].GetComponent<RectTransform>().position.x, textObjs[currentPhrase].GetComponent<RectTransform>().position.y - 30, 0);
                rightArrow.GetComponent<RectTransform>().position = new Vector3(10000, 10000, 0);
            } else {
                if (currentPhrase <= textObjs.Count - 1) {
                    if (textObjs.Count > currentPhrase + 1) {
                        rightArrow.GetComponent<RectTransform>().position = new Vector3((textObjs[currentPhrase].GetComponent<RectTransform>().position.x + textObjs[currentPhrase + 1].GetComponent<RectTransform>().position.x) / 2, textObjs[currentPhrase].GetComponent<RectTransform>().position.y, 0);
                        playArrow.GetComponent<RectTransform>().position = new Vector3(10000, 10000, 0);
                    } else {
                        rightArrow.GetComponent<RectTransform>().position = new Vector3(10000, 10000, 0);
                        playArrow.GetComponent<RectTransform>().position = new Vector3(textObjs[currentPhrase].GetComponent<RectTransform>().position.x, textObjs[currentPhrase].GetComponent<RectTransform>().position.y - 30, 0);
                    }
                } else {
                    playArrow.GetComponent<RectTransform>().position = new Vector3(10000, 10000, 0);
                    rightArrow.GetComponent<RectTransform>().position = new Vector3(10000, 10000, 0);
                }
            }
        }
    }


    public void SetText() {
        textObjs = new List<GameObject>();
        for (int i = 0; i < canvas.transform.childCount; i++) {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < currentTexts.Count; i++) {
            GameObject temp = Instantiate<GameObject>(origText);
            temp.GetComponent<RectTransform>().position = new Vector3(350 + i * 180, 100, 0);
            temp.GetComponent<Text>().text = currentTexts[i];
            temp.transform.parent = canvas.transform;
            textObjs.Add(temp);
        }
    }

}
