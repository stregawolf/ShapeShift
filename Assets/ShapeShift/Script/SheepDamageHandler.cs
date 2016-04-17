using UnityEngine;
using System.Collections;

public class SheepDamageHandler : MonoBehaviour {
    public Sheep m_sheep;

    public void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.gameObject.layer == Utils.kBoundsLayer)
        {
            return;
        }

        m_sheep.TakeDamage();
    }
}
