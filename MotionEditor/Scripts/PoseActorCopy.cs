using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseActorCopy : MonoBehaviour
{
    [SerializeField]
    GameObject poseActorSrc;
    [SerializeField]
    int actorCnt;
    [SerializeField]
    List<Material> materials;
    [SerializeField]
    GameObject manager;
    // Start is called before the first frame update
    private void Awake() {
        manager.GetComponent<PoseSelector>().poseActor.Add(poseActorSrc);
        for (int i = 0; i < actorCnt; i++) {
            GameObject temp = Instantiate<GameObject>(poseActorSrc);
            temp.transform.parent = poseActorSrc.transform.parent;
            temp.transform.GetChild(0).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().materials[2].color = materials[i].color;
            manager.GetComponent<PoseSelector>().poseActor.Add(temp);
        }
    }
}
