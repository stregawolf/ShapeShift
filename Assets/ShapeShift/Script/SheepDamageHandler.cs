using UnityEngine;
using System.Collections;

public class SheepDamageHandler : MonoBehaviour {
    public Sheep m_sheep;

    public void OnCollisionEnter2D(Collision2D c)
    {
        Obstacle obstacle = c.collider.GetComponentInParent<Obstacle>();
        if (obstacle != null && !obstacle.m_passed && Vector2.Dot(Vector2.up, c.contacts[0].normal) > 0.75f)
        {
            m_sheep.TakeDamage();
        }
    }
}
