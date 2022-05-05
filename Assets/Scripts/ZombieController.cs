using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    // [SerializeField] private float stoppingDistance = 2;
    private NavMeshAgent agent = null;
    private Animator anim = null;

    [SerializeField] private Transform target;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        MoveToTarget();
        RotateToTarget();
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
        anim.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);

        // float distanceToTarget = Vector3.Distance(target.position, transform.position);
        // if(distanceToTarget <= 2f);
        // {
        //     // Debug.Log(distanceToTarget.ToString());
        //     anim.SetFloat("Speed", 0f, .8f, Time.deltaTime);
        // }
    }

    private void RotateToTarget()
    {
        // transform.LookAt(target);

        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation; 
    }

    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }
}
