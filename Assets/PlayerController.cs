
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

        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            speed = 0;
            StartCoroutine(WaitForThreeSeconds());
        }

        void InflictDamage(IDamagable target)
        {
            target.TakeDamage(10);
        }

        bool IsInDistance(Vector2 TargetLocation)
        {
            float distance = Vector2.Distance(this.gameObject.transform.position, TargetLocation);
            if (distance <= attackRange) return true;
            else return false;
        }


        IEnumerator WaitForThreeSeconds()
        {
            yield return new WaitForSeconds(1f);
            speed = 5;
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            // Trigger different attack animations based on the direction
            if (direction.x > 0.5f)
                animator.SetTrigger("AttackRight");
            else if (direction.x < -0.5f)
                animator.SetTrigger("AttackLeft");
            else if (direction.y > 0.5f)
                animator.SetTrigger("AttackUp");
            else if (direction.y < -0.5f)
                animator.SetTrigger("AttackDown");

            isAttacking = true;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {

                if (hit.collider.CompareTag("Enemy") && IsInDistance(hit.collider.transform.position) == true)
                {
                    IDamagable target = hit.collider.GetComponent<IDamagable>();
                    print("hya!");
                    if (target != null)
                    {

                        InflictDamage(target);  // Pass the target variable to the InflictDamage method

                    }
                }
                isAttacking = false;
                // Code to be executed after waiting for 3 seconds
            }
        }
    }

    void FixedUpdate()
    {
        // Movement
        Vector2 moveVelocity = moveInput.normalized * speed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

    }
}