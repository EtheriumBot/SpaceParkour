using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class XRController : MonoBehaviour
{
    public TextMeshPro outText;
    public XRNode handRole;

    private Akaredaisha akaredaisha;
    private Vector3 defaultAkarTrans;

    private XRPlayerMovement playerMovement;

    Animator anim;

    void Start()
    {
        akaredaisha = Akaredaisha.reference;
        playerMovement = XRPlayerMovement.reference;
        defaultAkarTrans = new Vector3(173.1f, -49.2f, -551f);

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        InputDevice controller = InputDevices.GetDeviceAtXRNode(handRole);
        outText.text = "none";

        controller.TryGetFeatureValue(CommonUsages.triggerButton, out bool trigger);
        anim.SetBool("Trigger", trigger);
        if (trigger)
        {
            outText.text = "trigger";
        }

        controller.TryGetFeatureValue(CommonUsages.gripButton, out bool grip);
        anim.SetBool("Grip", grip);
        if (grip)
        {
            outText.text = "grip";
        }

        controller.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryBtn);
        if (primaryBtn)
        {
            outText.text = "primary Button";
        }

        controller.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryBtn);
        if (secondaryBtn)
        {
            outText.text = "secondary Button";
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        controller.TryGetFeatureValue(CommonUsages.menuButton, out bool menuBtn);
        if (menuBtn)
        {
            outText.text = "menu Button";
        }
    }
}