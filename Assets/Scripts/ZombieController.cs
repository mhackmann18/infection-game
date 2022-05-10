using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private float attackSpeed = 2.6f;
    private float timeOfLastAttack = 0;
    private NavMeshAgent agent = null;
    private Animator anim = null;

    [SerializeField] private Transform target;
    [SerializeField] private LayerMask groundMask;

    // FOV variables
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public bool detectedPlayer;

    // Do an FOV check 5 times / second
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    // Sets canSeePlayer to true if the zombie can see the player, and false if not
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    private void Start()
    {
        GetReferences();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        if(canSeePlayer){
            detectedPlayer = true;
        } 
        if(detectedPlayer){
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(distanceToTarget < 10){
            // Zombie stays in place during attack animation
            if(Time.time < timeOfLastAttack + attackSpeed && Time.time > attackSpeed){
                agent.isStopped = true;
            } else {
                agent.isStopped = false;
            }

            // Move to player
            agent.SetDestination(target.position);
            RotateToTarget();

            if(distanceToTarget <= agent.stoppingDistance)
            {
                anim.SetFloat("Speed", 0f, 0.2f, Time.deltaTime);

                // Attack if last attack animation is over
                if(Time.time >= timeOfLastAttack + attackSpeed){
                    timeOfLastAttack = Time.time;
                    AttackTarget();
                } 
            } else {
                anim.SetFloat("Speed", 1f, 0.2f, Time.deltaTime);
            }
        } else {
            anim.SetFloat("Speed", 0f, 0.2f, Time.deltaTime);
            agent.isStopped = true;
        }
    }

    private void RotateToTarget()
    {
        // transform.LookAt(target);

        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation; 
    }

    private void AttackTarget()
    {
        anim.SetTrigger("attack");
    }

    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }
}
