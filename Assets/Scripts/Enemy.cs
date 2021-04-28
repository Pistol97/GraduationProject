using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Rigidbody rigid;
    NavMeshAgent nav;

    public Transform target;
    public bool isChase;

    private Animator animator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ChaseDistance();

        if(isChase)
        nav.SetDestination(target.position);
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    void ChaseDistance()
    {
        if(Vector3.Distance(target.position,gameObject.transform.position)<=10)
        {
            animator.SetBool("IsWalk", true);
            isChase = true;
        }
        else
        {
            animator.SetBool("IsWalk", false);
            isChase = false;
        }
    }

    void FreezeVelocity()
    {
        if(isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
}
