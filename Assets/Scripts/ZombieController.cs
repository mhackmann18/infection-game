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

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        MoveToTarget();
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
