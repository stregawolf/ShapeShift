using UnityEngine;
using System.Collections.Generic;

public class Sheep : MonoBehaviour {
    public Rigidbody2D m_rigidbody;

    public float m_woolLostPerDamage = 1;

    private List<Wool> m_wools;

    private void Awake()
    {
        m_wools = new List<Wool>(GetComponentsInChildren<Wool>());
    }

    public void TakeDamage()
    {
        for (int i = 0; i < m_woolLostPerDamage; ++i)
        {
            LoseRandomWool();
        }
    }

    public void LoseRandomWool()
    {
        if (m_wools.Count <= 0)
        {
            return;
        }

        Wool wool = m_wools[Random.Range(0, m_wools.Count)];
        m_wools.Remove(wool);
        wool.BreakOff();
    }
}
