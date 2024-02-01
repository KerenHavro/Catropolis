using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private bool isAttacking = false;
    public float attackRange = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Input
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Update Animator parameters
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);
        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    IDamagable target = hit.collider.GetComponent<IDamagable>();
                    print("hya!");
                    if (target != null && IsWithinAttackRange(hit.collider.transform.position))
                    {
                        InflictDamage(target);  // Pass the target variable to the InflictDamage method
                    }
                }
            }
            isAttacking = false;
        }
    }

    void InflictDamage(IDamagable target)
    {
        target.TakeDamage(10);
    }

    bool IsWithinAttackRange(Vector2 targetPosition)
    {
        // Check if the distance between the player and the target is within the attack range
        float distance = Vector2.Distance(this.gameObject.transform.position, targetPosition);
        if (distance <= attackRange)
        {
            return true;
        }
        else return false;
    }


    void FixedUpdate()
    {
        // Movement
        Vector2 moveVelocity = moveInput.normalized * speed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
