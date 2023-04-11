using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float health;
    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        Destroy(this.gameObject);
    }
}
