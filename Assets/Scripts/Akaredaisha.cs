using UnityEngine;

public class Akaredaisha : MonoBehaviour
{

    private Rigidbody rb;

    public AudioSource aud;
    public AudioClip explosion1;
    public AudioClip explosion2;

    public ParticleSystem explode;

    // Create reference to self
    public static Akaredaisha reference;

    void Awake()
    {
        reference = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        // Move forward continuously at a constant speed
        rb.linearVelocity = transform.forward * 10f;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            Random.Range(0, 2);
            if (Random.Range(0, 2) == 0)
            {
                aud.PlayOneShot(explosion1);
            }
            else
            {
                aud.PlayOneShot(explosion2);
            }

            ContactPoint contact = collision.GetContact(0);
            Instantiate(explode, contact.point, Quaternion.identity);
        }
    }
}
