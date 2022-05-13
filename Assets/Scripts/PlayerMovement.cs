using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private float jumpHeight;

    private CharacterController controller;
    public Animator anim;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private float attackTime = 1.58f;
    private float timeOfLastAttack = 0;

    public PlayerHealth playerHealth;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if(pauseMenu.GameIsPaused){return;}

        Move();

        // Attack if attack timer is over
        if(Input.GetKeyDown(KeyCode.Mouse0) && (Time.time >= timeOfLastAttack + attackTime || Time.time < attackTime))
        {
            Attack();
        }

        // Change animator parameters to update animations
        if(isGrounded){
            anim.SetBool("IsFalling", false);
            anim.SetBool("IsGrounded", true);
            anim.SetBool("IsJumping", false);
        } else {
            anim.SetBool("IsGrounded", false);
            if(velocity.y < 0){
                anim.SetBool("IsFalling", true);
            }
        }
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);
        
        // Only allow movement control when player is grounded
        if(isGrounded){
            if(moveDirection != Vector3.zero && Vector3.Dot(transform.forward, moveDirection) < 0){
                moveSpeed = walkSpeed * .8f;
                anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
            }
                else if(moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                // Walk
                Walk();
            } else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                // Run
                Run();
            } else if(moveDirection == Vector3.zero)
            {
                // Idle
                Idle();
            }
            
            // Don't let player move when landing animation is active
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Landing")){
                moveSpeed = 0;
            }

            moveDirection *= moveSpeed;

            if(Input.GetKeyDown(KeyCode.Space) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Landing")){
                Jump();
            }
        } else {
            // If player is airborne reduce speed slightly
            moveDirection *= (moveSpeed * .75f);
        }


        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", .33f, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.66f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        anim.SetBool("IsJumping", true);
        isGrounded = false;
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    private void Attack(){
        // Attack cooldown timer
        timeOfLastAttack = Time.time;
        anim.SetTrigger("Attack");

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies){
            Debug.Log("Hit!");
            enemy.GetComponent<EnemyHealth>().TakeDamage(34);
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
