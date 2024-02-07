using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Player : MonoBehaviour, IDamagable
{
    
    public int maxHealth = 100;
    [SerializeField]
    public int currentHealth;


    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

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

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);
    }

    public void Die()
    {
        // Handle death actions, such as respawning or game over
        Debug.Log("Player has died!");
    }

    public void InflictDamage(IDamagable Enemy)
    {
        // Assuming target is also IDamagable
        if (Enemy != null)
        {
            Enemy.TakeDamage(10); // Inflict 10 damage
        }
    }

}