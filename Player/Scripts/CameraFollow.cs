using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform actor;

    Vector3 initPosition;
    Quaternion initRotation;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(actor.transform.position.x, actor.transform.position.y + 0.2f, actor.transform.position.z + 1.5f);
        initPosition = this.transform.position;
        initRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void Right() {
        this.transform.RotateAround(actor.transform.position,-this.transform.up,10);
    }
    public void Left() {
        this.transform.RotateAround(actor.transform.position, this.transform.up, 10);
    }
    public void FromRight() {
        Reset();
        this.transform.RotateAround(actor.transform.position, this.transform.up, 90);
    }
    public void FromLeft() {
        Reset();
        this.transform.RotateAround(actor.transform.position, -this.transform.up, 90);
    }
    public void Reset() {
        this.transform.rotation = initRotation;
        this.transform.position = initPosition;
    }
}
