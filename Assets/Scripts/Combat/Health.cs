using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;

    private int health;

    private void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int damage)
    {
        if (health == 0) { return; }
        
        // Set health to be larger of the 2
        health = Mathf.Max(health - damage, 0);

        Debug.Log(health);
    }
}