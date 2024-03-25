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
    private NPCController npc;
    public Player player;


    [SerializeField]
    private AudioClip[] mines;

    [SerializeField]
    private AudioClip[] chops;


    [SerializeField]
    private AudioClip[] hits;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("NPC"))
            {

                npc = hit.collider.gameObject.GetComponent<NPCController>(); // Assign to class-level variable
                if (npc != null)
                {

                    animator.SetFloat("Speed", 0);
                    speed = 0;
                    npc.ActivateDialogue();
                    // Set isAttacking to false to ensure the player stops attacking when initiating dialogue

                }
            }
            if (hit.collider != null && hit.collider.CompareTag("Quest"))
            {
                GameEventsManager.instance.inputEvents.SubmitPressed();
            }
            if (hit.collider != null &&( hit.collider.CompareTag("Object")|| hit.collider.CompareTag("Enemy")))
            {
                player.GetHungry(1);
                StartCoroutine(AttackAnimation());
            }
            else
            {

                
                StartCoroutine(AttackAnimation());
            }
        }

        // Check if the player is in dialogue
        if (!InDialogue())
        {

            // Input
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Determine the primary movement direction
            if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput) && !isAttacking)
            {
                animator.SetTrigger("IsWalking");
                moveInput = new Vector2(horizontalInput, 0f);
            }

            if (Mathf.Abs(horizontalInput) < Mathf.Abs(verticalInput) && !isAttacking)
            {
                animator.SetTrigger("IsWalking");
                moveInput = new Vector2(0f, verticalInput);
            }

            // Update Animator parameters
            animator.SetFloat("Horizontal", moveInput.x);
            animator.SetFloat("Vertical", moveInput.y);
            animator.SetFloat("Speed", moveInput.sqrMagnitude);
        }
        else
        {
            // If in dialogue, stop walking
            animator.SetFloat("Speed", 0);
            speed = 0;
            animator.SetBool("IsWalking", true);
        }
    }

    private bool InDialogue()
    {
        if (npc != null)
            return npc.DialogueActive();
        else
            return false;
    }

    IEnumerator AttackAnimation()
    {
        animator.ResetTrigger("IsWalking");
        isAttacking = true;
        speed = 0;

        // Stop walking animation

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

                    // Play a random sound from the array of sounds
                    if (hits.Length > 0)
                    {
                        int randomIndex = Random.Range(0, hits.Length);
                        SoundManager.instance.PlaySound(hits[randomIndex]);
                    }
                    // Pass the target variable to the InflictDamage method
                }
            }
            if (hit.collider.CompareTag("Object") && IsInDistance(hit.collider.transform.position))
            {
                MineableObject mineableObject = hit.collider.GetComponent<MineableObject>();
                if (mineableObject != null)
                {
                    // Mine the object
                    mineableObject.Mine(player.currentMiningPower, player.currentChoppingPower);
                    // Play a random sound from the array of sounds
                    if (mines.Length > 0)
                    {
                        int randomIndex = Random.Range(0, mines.Length);
                        SoundManager.instance.PlaySound(mines[randomIndex]);
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.4f);

        animator.ResetTrigger("AttackDown");
        animator.ResetTrigger("AttackUp");
        animator.ResetTrigger("AttackRight");
        animator.ResetTrigger("AttackLeft");
        animator.SetTrigger("IsWalking");
        isAttacking = false;
        speed = 5;
    }

    void InflictDamage(IDamagable target)
    {

        player.InflictDamage(target);


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
