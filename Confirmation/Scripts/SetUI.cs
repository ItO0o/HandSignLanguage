using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        this.GetComponent<Text>().text = "手話単語" + "\""+ MysqlClient.motionName +"\"の動作が記録されました(ID:" + MysqlClient.id +")"; 
            }
    // Update is called once per frame
    void Update()
    {
        
    }
}
