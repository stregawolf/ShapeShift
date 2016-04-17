using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {
    public Sheep m_sheep;
    public CameraFollow m_camera;
    public float m_lookAheadScaler = 1.0f;
    public Transform m_topBounds;
    public float m_shiftStep = 2.0f;
    public float m_cycleDistance = 100.0f;

    public GameObject[] m_obsticles;
    public float m_obsticleOffset = -10.0f;
    public GameObject m_currentObsticle;
    public bool m_obsticleCounted = false;

    public int m_numObsticlesPassed = 0;
    public Text m_obsticlesPassedDisplayText;

    private Vector2 m_gravity;

    private void Awake()
    {
        m_gravity = Physics2D.gravity;
    }

    private void Update()
    {
        if (m_currentObsticle == null)
        {
            m_currentObsticle = Instantiate(m_obsticles[Random.Range(0, m_obsticles.Length)], new Vector3(0, m_sheep.transform.position.y + m_obsticleOffset, 0), Quaternion.identity) as GameObject;
            m_obsticleCounted = false;
        }

        if (m_currentObsticle != null)
        {
            int shift = 0;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                shift--;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                shift++;
            }

            m_currentObsticle.transform.position += Vector3.right * shift * m_shiftStep;

            if (m_currentObsticle.transform.position.y > m_topBounds.position.y)
            {
                Destroy(m_currentObsticle);
            }

            if (!m_obsticleCounted && m_sheep.transform.position.y < m_currentObsticle.transform.position.y)
            {
                m_obsticleCounted = true;
                m_numObsticlesPassed++;
                m_obsticlesPassedDisplayText.text = m_numObsticlesPassed.ToString();
            }
        }

    }

    private void FixedUpdate()
    {
        m_camera.m_offset = m_sheep.m_rigidbody.velocity.y * m_lookAheadScaler;
    }

    private void LateUpdate()
    {
        if (m_sheep.transform.position.y < -m_cycleDistance)
        {
            Vector3 offset = Vector3.up * m_cycleDistance;
            m_sheep.transform.position += offset;
            m_camera.transform.position += offset;
            if (m_currentObsticle != null)
            {
                m_currentObsticle.transform.position += offset;
            }
        }
    }
}
