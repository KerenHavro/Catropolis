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
    public int playerDmg;

    [SerializeField]
    public GameObject deathBoard;

    public PlayerStats playerStats;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    public int MaxHunger => maxHunger;
    public int CurrentHunger => currentHunger;

    public int CurrentMiningPower => currentMiningPower;
    public int CurrentChoppingPower => currentChoppingPower;

    public int PlayerDmg => playerDmg;


    void Start()
    {
        GameObject playerStatsObject = GameObject.FindGameObjectWithTag("StatManager");
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentHunger = maxHunger;
        hungerBar.SetMaxHealth(maxHunger);
        playerStats = playerStatsObject.GetComponent<PlayerStats>();
        currentMiningPower = playerStats.miningStrength;
        currentChoppingPower = playerStats.choppingStrenght;
        playerDmg = playerStats.attack;
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
        this.gameObject.SetActive(false);
        deathBoard.SetActive(true);
        Debug.Log("Player has died!");
    }

    public void InflictDamage(IDamagable Enemy)
    {
        // Assuming target is also IDamagable
        if (Enemy != null)
        {
            Enemy.TakeDamage(playerDmg); // Inflict 10 damage
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
    public void ApplyKnockback(Vector2 damageSourcePosition)
    {
        Debug.Log("knocked");
        StartCoroutine(KnockbackCoroutine(damageSourcePosition));
    }
    IEnumerator KnockbackCoroutine(Vector2 damageSourcePosition)
    {
        Vector2 knockbackDirection = ((Vector2)transform.position - damageSourcePosition).normalized;
        Vector2 endPosition = (Vector2)transform.position + knockbackDirection * 2;
        float elapsedKnockbackTime = 0f;

        while (elapsedKnockbackTime < 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition, 5 * Time.deltaTime);
            elapsedKnockbackTime += Time.deltaTime;
            yield return null;
        }


    }


}