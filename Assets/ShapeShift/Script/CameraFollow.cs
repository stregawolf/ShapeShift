using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public Transform m_target;
    public float m_offset;
    public float m_followSpeed = 3.0f;

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = m_target.position.y + m_offset;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * m_followSpeed);
    }
}
