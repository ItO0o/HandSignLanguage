using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseSelector : MonoBehaviour
{
    public List<GameObject> poseActor;
    [SerializeField]
    GameObject mainActor;

    [SerializeField]
    List<Material> materials;
    List<Color> buttonColor;
    // Start is called before the first frame update
    void Start()
    {
        buttonColor = new List<Color>();
        for (int i = 0; i < materials.Count; i++)
        {
            buttonColor.Add(new Color(materials[i].color.r, materials[i].color.g, materials[i].color.b, 1));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonColor(int poseNum, GameObject button)
    {
        ColorBlock color = button.GetComponent<Button>().colors;

        color.normalColor = buttonColor[poseNum - 1];

        button.GetComponent<Button>().colors = color;
    }

    public void SetPose(int poseNum)
    {
        poseActor[poseNum].SetActive(true);

        HumanPose pose2 = new HumanPose();

        Animator animator2 = poseActor[poseNum].GetComponent<Animator>();
        HumanPoseHandler handler2 = new HumanPoseHandler(animator2.avatar, animator2.transform);
        handler2.GetHumanPose(ref pose2);
        for (int i = 0; i < HumanTrait.MuscleCount; i++)
        {
            pose2.muscles[i] = this.GetComponent<AnimationManager>().motion.currentMotion.Poses[poseNum].muscleValue[i];
        }
        handler2.SetHumanPose(ref pose2);
    }
}
