using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Player : MonoBehaviour, IDamagable
{
    public HealthBar healthBar;
    public int maxHealth = 100;
    [SerializeField]
    public int currentHealth;
    [SerializeField]
    public int currentMiningPower;
    [SerializeField]
    public int currentChoppingPower;
    [SerializeField]
    public int currentHunger;
    public HealthBar hungerBar;
    public int maxHunger = 100;



    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    public int MaxHunger => maxHunger;
    public int CurrentHunger => currentHunger;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentHunger = maxHunger;
        hungerBar.SetMaxHealth(maxHunger);
 

    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);
        healthBar.SetHealth(currentHealth);
  
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

    public void Feed(int feedAmount)
    {
        currentHunger = Mathf.Min(maxHunger, currentHunger + feedAmount);
        hungerBar.SetHealth(CurrentHunger);
        StopCoroutine(TakeDamageOverTime());
    }
    public void GetHungry(int hungerAmount)
    {
 
        currentHunger -= hungerAmount;
        hungerBar.SetHealth(currentHunger);
        if (currentHunger <= 0)
        {
            currentHunger = 0;
        
                StartCoroutine(TakeDamageOverTime());
            
        }
        else
        {
            StopCoroutine(TakeDamageOverTime());
        }
    }
    IEnumerator TakeDamageOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            // Take damage
            TakeDamage(5);
            if (currentHunger > 0)
            {
                StopAllCoroutines();
            }
            
        }
       
        }

}