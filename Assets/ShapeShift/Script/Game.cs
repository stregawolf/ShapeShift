using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
    private static Game sInstance;
    public static Game Instance { get { return sInstance; } }

    public Sheep m_sheep;
    public CameraFollow m_camera;
    public float m_lookAheadScaler = 1.0f;
    public Transform m_topBounds;
    public float m_cycleDistance = 100.0f;

    public GameObject[] m_obstaclePrefabs;
    public float m_obstacleOffset = -16.0f;

    public int m_numObstaclesPassed = 0;
    public Text m_obstaclesPassedDisplayText;

    public List<Obstacle> m_obstacles = new List<Obstacle>();
    private List<Obstacle> m_newObstacles = new List<Obstacle>();
    private List<Obstacle> m_obstaclesToRemove = new List<Obstacle>();

    private void Awake()
    {
        sInstance = this;
        m_obstacles.Add(SpawnObstacle());
    }

    private void Update()
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

        m_newObstacles.Clear();
        m_obstaclesToRemove.Clear();
        for (int i = 0, n = m_obstacles.Count; i < n; ++i)
        {
            Obstacle obstacle = m_obstacles[i];
            
            if (obstacle.transform.position.y > m_topBounds.position.y)
            {
                m_obstaclesToRemove.Add(obstacle);
            }

            obstacle.Shift(shift);
            obstacle.UpdateElementPositions();

            if (!obstacle.m_passed && m_sheep.transform.position.y < obstacle.transform.position.y)
            {
                obstacle.m_passed = true;
                m_numObstaclesPassed++;
                m_obstaclesPassedDisplayText.text = m_numObstaclesPassed.ToString();

                m_newObstacles.Add(SpawnObstacle());
            }
        }

        if (m_newObstacles.Count > 0)
        {
            m_obstacles.AddRange(m_newObstacles);
        }

        if (m_obstaclesToRemove.Count > 0)
        {
            for (int i = 0, n = m_obstaclesToRemove.Count; i < n; ++i)
            {
                Obstacle obstacle = m_obstaclesToRemove[i];
                m_obstacles.Remove(obstacle);
                Destroy(obstacle.gameObject);
            }
        }
    }

    public Obstacle SpawnObstacle()
    {
        GameObject obstacleObj = Instantiate(m_obstaclePrefabs[Random.Range(0, m_obstaclePrefabs.Length)], new Vector3(0, m_sheep.transform.position.y + m_obstacleOffset, 0), Quaternion.identity) as GameObject;
        Obstacle obstacle = obstacleObj.GetComponent<Obstacle>();
        obstacle.m_offsetIndex = Random.Range(-obstacle.m_elements.Length, obstacle.m_elements.Length);
        obstacle.UpdateElementPositions(true);
        return obstacle;
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

            for (int i = 0, n = m_obstacles.Count; i < n; ++i)
            {
                Obstacle obstacle = m_obstacles[i];
                obstacle.transform.position += offset;
            }
        }
    }
}
