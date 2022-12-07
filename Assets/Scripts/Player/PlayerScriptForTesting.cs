using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptForTesting : MonoBehaviour
{
    private float health = 100f;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("Health: " + health);
        }
    }

    void Die()
    {
        Debug.Log("Yuo is died");
    }

    public void Damage(float damage)
    {
        if (health <= 0)
            return;
        
        health -= damage;
    }
}
