using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class XRJumping : MonoBehaviour
{
    int castDist = 2;
    int jumpForce = 200;
    public XRNode controllerHand = XRNode.RightHand;
    public LayerMask groundLayer;
    public Transform body;
    Rigidbody rb;
    bool grounded = false;
    bool primaryBtnState = false;

    Transform platformTrans;
    Rigidbody platformRB;

    [Header("Floating HUD Text")]
    public TextMeshPro outText;

    private int jumpsleft = 10;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        InputDevices.GetDeviceAtXRNode(controllerHand).TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryBtn);
        if (primaryBtn && !primaryBtnState && grounded && jumpsleft > 0)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            jumpsleft--;
            outText.text = "Jumps Left: " + jumpsleft.ToString();
        }
        primaryBtnState = primaryBtn;
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(body.position, -Vector3.up, out RaycastHit hit, castDist, groundLayer))
        {
            if (hit.distance < 1f)
            {
                grounded = true;
                if (hit.transform != platformTrans)
                {
                    platformTrans = hit.transform;
                    platformTrans.TryGetComponent(out platformRB);
                }
            } else
            {
                grounded = false;
            }

            if (platformRB != null)
            {
                rb.linearVelocity += platformRB.linearVelocity;
            }
        }
        else
        {
            platformTrans = null;
            platformRB = null;
            grounded = false;
        }
    }
}