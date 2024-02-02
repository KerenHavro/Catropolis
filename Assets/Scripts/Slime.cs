using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IDamagable
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 2f;
    public float attackRange = 1f;
    public float knockbackSpeed = 5f;
    public float knockbackDistance = 5f;
    public float chaseRange = 5f;


    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int currentHealth;
    public Transform player;

    private const int PatrolState = 0;
    private const int ChaseState = 1;
    private const int AttackState = 2;
 

    private int currentState;

    void Start()
    {
       
        currentState = PatrolState;
        currentHealth = maxHealth;
        
    }

    void Update()
    {
        switch (currentState)
        {
            case PatrolState:
                Patrol();
                break;
            case ChaseState:
                Chase();
                break;
            case AttackState:
                Attack();
                break;
        
        }
    }

    void Patrol()
    {
        // Implement patrolling behavior here

        // Example: Move left and right
        transform.Translate(Vector2.left * patrolSpeed * Time.deltaTime);

        // Check if the player is within chase range
        if (Vector2.Distance(transform.position, player.position) < chaseRange)
        {
            currentState = ChaseState;
        }
    }

    void Chase()
    {

        // Implement chasing behavior here
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

        // Check if the player is within attack range
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            currentState = AttackState;
        }
        // Check if the player is out of chase range
        else if (Vector2.Distance(transform.position, player.position) > chaseRange)
        {
            currentState = PatrolState;
        }
    }

    void Attack()
    {
        // Implement attacking behavior here

        // Example: Deal damage to the player
        Debug.Log("Attacking player!");

        // Check if the player is out of attack range
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            currentState = ChaseState;
        }
    }



    public void TakeDamage(int damageAmount)
    {
        Vector2 damageSourcePosition = player.position;
        currentHealth -= damageAmount;
        ApplyKnockback(damageSourcePosition);


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

    void ApplyKnockback(Vector2 damageSourcePosition)
    {
       
            StartCoroutine(KnockbackCoroutine(damageSourcePosition));
        
    }
    IEnumerator KnockbackCoroutine(Vector2 damageSourcePosition)
    {
        // Calculate knockback direction based on the damage source position
        Vector2 knockbackDirection = ((Vector2)transform.position - damageSourcePosition).normalized;

        // Calculate the end position after knockback
        Vector2 endPosition = (Vector2)transform.position + knockbackDirection * knockbackDistance;

        // Time elapsed during knockback
        float elapsedKnockbackTime = 1f;

        while (elapsedKnockbackTime < 2f) // Knockback duration is 2 seconds
        {
            // Move towards the end position
            transform.position = Vector2.MoveTowards(transform.position, endPosition, knockbackSpeed * Time.deltaTime);

            // Update elapsed time
            elapsedKnockbackTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

      
        currentState = ChaseState;
    }
}
