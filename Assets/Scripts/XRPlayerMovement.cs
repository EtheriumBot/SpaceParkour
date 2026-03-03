using System;
using UnityEngine;
using UnityEngine.XR;

public class XRPlayerMovement : MonoBehaviour
{
    int speed = 5;
    public XRNode moveStick = XRNode.LeftHand;
    public XRNode rotStick = XRNode.RightHand;

    public Transform body;
    private Rigidbody rb;
    Transform camTrans;
    bool justClicked = false;
    int rot = 0;

    private Akaredaisha akar;

    public static XRPlayerMovement reference;

    void Awake()
    {
        reference = this;
        akar = Akaredaisha.reference;
    }

    void Start()
    {
        camTrans = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        body.position = new Vector3(camTrans.position.x, body.position.y, camTrans.position.z);
    }

    void Update()
    {
        InputDevices.GetDeviceAtXRNode(rotStick).TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 joyStick);
        if (!justClicked)
        {
            if (joyStick.x > .9f)
            {
                rot += 90;
                rb.MoveRotation(Quaternion.Euler(0, rot, 0));
                justClicked = true;
            }
            else if (joyStick.x < -.9f)
            {
                rot -= 90;
                rb.MoveRotation(Quaternion.Euler(0, rot, 0));
                justClicked = true;
            }
        }
        else if (Math.Abs(joyStick.x) < .1f)
        {
            justClicked = false;
        }
    }

    void FixedUpdate()
    {
        InputDevices.GetDeviceAtXRNode(moveStick).TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 direction);
        Vector3 moveDir = camTrans.forward * direction.y + camTrans.right * direction.x;
        moveDir = moveDir.normalized * speed;
        moveDir.y = rb.linearVelocity.y;
        rb.linearVelocity = moveDir;
    }


}