using UnityEngine;
using System.Collections;

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
        animator.SetBool("IsWalking", true);
    }

    void Update()
    {
        // Input
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Determine the primary movement direction
        if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
        {
            // Moving horizontally
            moveInput = new Vector2(horizontalInput, 0f);
        }
        else
        {
            // Moving vertically
            moveInput = new Vector2(0f, verticalInput);
        }

        // Update Animator parameters
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);
        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        // Check for an attack input
        if (Input.GetMouseButton(0) && !isAttacking)
        {
            animator.SetBool("IsWalking", false);
            StartCoroutine(AttackAnimation());
        }
    }

    IEnumerator AttackAnimation()
    {
        speed = 0;
        isAttacking = true;

        // Stop walking animation
        animator.SetBool("IsWalking", false);

        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        // Trigger different attack animations based on the direction
        if (direction.x > 0.5f)
            animator.SetTrigger("AttackRight");
        else if (direction.x < -0.5f)
            animator.SetTrigger("AttackLeft");
        else if (direction.y > 0.5f)
            animator.SetTrigger("AttackUp");
        else if (direction.y < -0.5f)
            animator.SetTrigger("AttackDown");

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy") && IsInDistance(hit.collider.transform.position))
            {
                IDamagable target = hit.collider.GetComponent<IDamagable>();
                print("hya!");
                if (target != null)
                {
                    InflictDamage(target);

                    // Pass the target variable to the InflictDamage method
                }
            }
            if (hit.collider.CompareTag("Object") && IsInDistance(hit.collider.transform.position))
            {
                MineableObject mineableObject = hit.collider.GetComponent<MineableObject>();

                if (mineableObject != null)
                {
                    // Mine the object
                    mineableObject.Mine();
                }
            }
        }

        // Add delay based on your attack animation duration
        yield return new WaitForSeconds(0.4f); // Adjust this value based on your animation length

        animator.ResetTrigger("AttackDown");
        animator.ResetTrigger("AttackUp");
        animator.ResetTrigger("AttackRight");
        animator.ResetTrigger("AttackLeft");
        animator.SetBool("IsWalking", true);
        isAttacking = false;
        speed = 5;
   
    }

    void InflictDamage(IDamagable target)
    {
        target.TakeDamage(10);
      



    }

 
    bool IsInDistance(Vector2 TargetLocation)
    {
        float distance = Vector2.Distance(this.gameObject.transform.position, TargetLocation);
        return distance <= attackRange;
    }
  


        void FixedUpdate()
    {
        // Movement
        Vector2 moveVelocity = moveInput.normalized * speed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
