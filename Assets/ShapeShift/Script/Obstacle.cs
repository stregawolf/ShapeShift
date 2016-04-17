using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
    public const float kOffsetBounds = 10;
    public const float kShiftStep = 2.0f;
    public const float kShiftSpeed = 10.0f;

    public Transform[] m_elements;
    public int m_offsetIndex = 0;

    public bool m_passed = false;

    public void Shift(int dir)
    {
        if (dir < 0)
        {
            int indexOfFirst = GetAdjustedIndex(-m_offsetIndex);
            Transform element = m_elements[indexOfFirst];
            Vector3 pos = element.localPosition;
            pos.x = kOffsetBounds;
            element.localPosition = pos;
        }
        else if (dir > 0)
        {
            int indexOfLast = GetAdjustedIndex(-m_offsetIndex-1);
            Transform element = m_elements[indexOfLast];
            Vector3 pos = element.localPosition;
            pos.x = -kOffsetBounds;
            element.localPosition = pos;
        }

        m_offsetIndex += dir;
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Shift(-1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Shift(1);
        }
        UpdateElementPositions();
    }
     */

    public int GetAdjustedIndex(int i)
    {
        if (i < 0)
        {
            i = m_elements.Length + (i % m_elements.Length);
        }

        return i % m_elements.Length;
    }

    public void UpdateElementPositions(bool optInstant = false)
    {
        float startX = -kOffsetBounds;
        for (int i = 0, n = m_elements.Length; i < n; ++i)
        {
            Transform element = m_elements[i];
            float x = startX + GetAdjustedIndex(m_offsetIndex + i) * kShiftStep;

            Vector3 pos = element.localPosition;
            pos.x = x;
            if (optInstant)
            {
                element.localPosition = pos;
            }
            else
            {
                element.localPosition = Vector3.Lerp(element.localPosition, pos, Time.deltaTime * kShiftSpeed);
            }
        }
    }
}
