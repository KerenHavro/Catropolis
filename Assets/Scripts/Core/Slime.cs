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

    public bool isNear;

    public Animator animator;

    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private const int PatrolState = 0;
    private const int ChaseState = 1;
    private const int AttackState = 2;
    private int currentState;
    private Vector2 initialPosition;
    private Vector2 patrolDestination;

    private Transform playerTransform; // Reference to the player's Transform

    void Start()
    {
        initialPosition = transform.position;
        SetNewPatrolDestination();
        animator.SetTrigger("Hit");
        animator.SetTrigger("Die");
        healthBar.SetMaxHealth(maxHealth);
        currentState = PatrolState;
        currentHealth = maxHealth;

        // Find the player GameObject with the "Player" tag and get its Transform component
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found!");
        }
    }

    void Update()
    {
        if (playerTransform == null) // If player Transform is not found, return
            return;

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
        if (Vector2.Distance(transform.position, patrolDestination) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolDestination, patrolSpeed * Time.deltaTime);
        }
        else
        {
            SetNewPatrolDestination();
        }

        if (Vector2.Distance(transform.position, playerTransform.position) < chaseRange)
        {
            currentState = ChaseState;
        }
    }

    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, chaseSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            currentState = AttackState;
        }
        else if (Vector2.Distance(transform.position, playerTransform.position) > chaseRange)
        {
            currentState = PatrolState;
        }
    }

    void Attack()
    {
        Debug.Log("Attacking player!");

        if (Vector2.Distance(transform.position, playerTransform.position) > attackRange)
        {
            currentState = ChaseState;
        }
    }

    void SetNewPatrolDestination()
    {
        patrolDestination = initialPosition + Random.insideUnitCircle * 5f;
    }

    public void TakeDamage(int damageAmount)
    {
        animator.SetTrigger("Hit");
        Vector2 damageSourcePosition = playerTransform.position;
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
        StartCoroutine(Death());
    }

    public void ApplyKnockback(Vector2 damageSourcePosition)
    {
        StartCoroutine(KnockbackCoroutine(damageSourcePosition));
    }

    IEnumerator KnockbackCoroutine(Vector2 damageSourcePosition)
    {
        Vector2 knockbackDirection = ((Vector2)transform.position - damageSourcePosition).normalized;
        Vector2 endPosition = (Vector2)transform.position + knockbackDirection * knockbackDistance;
        float elapsedKnockbackTime = 0f;

        while (elapsedKnockbackTime < 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition, knockbackSpeed * Time.deltaTime);
            elapsedKnockbackTime += Time.deltaTime;
            yield return null;
        }

        animator.SetBool("Hit", false);
        currentState = ChaseState;
    }

    public void InflictDamage(IDamagable player)
    {
        if (player != null)
        {
            player.TakeDamage(10);
            player.ApplyKnockback(gameObject.transform.position);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamagable currentPlayer = collision.gameObject.GetComponent<IDamagable>();
            if (currentPlayer != null)
            {
                isNear = true;
                StartCoroutine(DealDamageRepeatedly(currentPlayer));
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isNear = false;
        }
    }

    IEnumerator DealDamageRepeatedly(IDamagable player)
    {
        while (isNear)
        {
            player.TakeDamage(10);
            player.ApplyKnockback(gameObject.transform.position);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Death()
    {
        isNear = false;
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);

    }


}
