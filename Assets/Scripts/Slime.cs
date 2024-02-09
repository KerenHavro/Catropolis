using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour, IDamagable
{
    public HealthBar healthBar;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 2f;
    public float attackRange = 1f;
    public float knockbackSpeed = 5f;
    public float knockbackDistance = 5f;
    public float chaseRange = 5f;

    public Animator animator;
    public Transform player;

    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private const int PatrolState = 0;
    private const int ChaseState = 1;
    private const int AttackState = 2;
    private int currentState;
    private Vector2 initialPosition;
    private Vector2 patrolDestination;

    void Start()
    {
        initialPosition = transform.position; // Set initial position
        SetNewPatrolDestination(); // Set initial patrol destination
        animator.SetBool("Hit", false);
        healthBar.SetMaxHealth(maxHealth);
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
        // Move towards the patrol destination
        transform.position = Vector2.MoveTowards(transform.position, patrolDestination, patrolSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, patrolDestination) < 0.1f)
        {
            // Set a new random destination within the patrol radius
            SetNewPatrolDestination();
        }
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

    void SetNewPatrolDestination()
    {
        // Generate a random patrol destination within a certain range around the initial position
        patrolDestination = initialPosition + Random.insideUnitCircle * 5f;
    }

    public void TakeDamage(int damageAmount)
    {
        animator.SetBool("Hit", true);
        Vector2 damageSourcePosition = player.position;
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
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

        animator.SetBool("Hit", false);
        currentState = ChaseState;
    }
}