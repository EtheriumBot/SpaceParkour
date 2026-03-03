using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public Material off;
    public Material on;

    private int switchCounter = 0;

    public AudioClip pushSnd;
    private AudioSource aud;

    private MeshRenderer rend;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aud = XRPlayerMovement.reference.GetComponent<AudioSource>();
        rend = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);

        // If the object that collided with the button is on the "Grabable" layer, toggle the door's active state and play the push sound
        if (collision.gameObject.layer == LayerMask.NameToLayer("Grabable"))
        {
            aud.PlayOneShot(pushSnd);

            // If the material is off, change it to on, and vice versa
            switchCounter++;
            if (switchCounter % 2 == 0)
            {
                rend.material = off;
            }
            else
            {
                rend.material = on;
            }

            StartCoroutine(DelayedAction(1f));
        }


    }

    IEnumerator DelayedAction(float delayTime)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(delayTime);

        // Load ending scene
        SceneManager.LoadScene("EndingScene");
    }
}
