using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slime : MonoBehaviour, IDamagable
{
    [SerializeField]
    public int maxHealth = 100;
    [SerializeField]
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Handle death actions for the slime enemy
        Debug.Log("Slime enemy has been defeated!");
        Destroy(gameObject); // Assuming you want to destroy the enemy object when it dies
    }
}
