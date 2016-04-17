using UnityEngine;
using System.Collections;

public class Wool : MonoBehaviour {
    public const float kBreakOffForce = 10.0f;

    public SpringJoint2D m_joint;
    public Rigidbody2D m_rigidbody;
    public Collider2D m_collider;

    private void Awake()
    {
        if (m_joint == null)
        {
            m_joint = GetComponent<SpringJoint2D>();
        }

        if (m_rigidbody == null)
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        if (m_collider == null)
        {
            m_collider = GetComponent<Collider2D>();
        }
    }

    public void BreakOff()
    {
        transform.parent = null;
        Destroy(m_joint);
        m_collider.enabled = true;
        m_rigidbody.mass = 1.0f;
        m_rigidbody.AddForce(Vector3.up * kBreakOffForce, ForceMode2D.Impulse);
        Destroy(gameObject, 3.0f);
    }
}
