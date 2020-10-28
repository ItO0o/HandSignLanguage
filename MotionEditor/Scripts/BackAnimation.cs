using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAnimation : MonoBehaviour
{
    [SerializeField]
    GameObject manager;

    public bool loop;

    private void Start()
    {
        loop = true;
    }
    // Start is called before the first frame update
    void Update()
    {
        //if (manager.GetComponent<LocalMotion>().localMotion.Poses.Count > 1)
        //{
        //    manager.GetComponent<AnimationManager>().currentElm = AnimationManager.ModeElm.Play;
        //    manager.GetComponent<AnimationManager>().currentState = AnimationManager.StateElm.Playing;
        //}

        //if (loop && manager.GetComponent<LocalMotion>().localMotion.Poses.Count > 1 && manager.GetComponent<AnimationManager>().sequence == manager.GetComponent<AnimationManager>().motion.currentMotion.Poses.Count - 1)
        //{
        //    manager.GetComponent<AnimationManager>().currentElm = AnimationManager.ModeElm.Loop;
        //}
    }
}
