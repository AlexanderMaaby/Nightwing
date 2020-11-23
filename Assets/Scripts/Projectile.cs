using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Projectile : MonoBehaviour
{

    public float m_Speed = 500f;   // this is the projectile's speed
    public float m_Lifespan = 2f; // this is the projectile's lifespan (in seconds)

    public string projectileTag;

    private Rigidbody m_Rigidbody;

    void Awake()
    {
        m_Rigidbody = this.GetComponent<Rigidbody>();
    }

    void Start()
    {
        projectileTag = this.tag;
        m_Rigidbody.AddForce(transform.forward * m_Speed);
        //m_Rigidbody.AddForce(m_Rigidbody.transform.forward * m_Speed);
        Destroy(gameObject, m_Lifespan);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (projectileTag == "Fire")
        {
            if (other.tag == "Enemy")
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(-20);
                Destroy(this);
            }
        }
        if (projectileTag == "Curse")
        {
            if (other.tag == "Enemy")
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(20);
                Destroy(this);
            }
        }
    }
}