using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    Rigidbody rigid;
    NavMeshAgent nav;

    public float enemySpeed = 1;
    public float enemySightLength = 10f;
    public float currentEnemySightLength = 10f;
    public float enemyAttackLength = 3f;
    public int enemySightLevel = 1;

    [SerializeField]
    private Transform target;
    private Animator animator;
    public bool isChase = false;

    [Header("Patrol 관련 변수")]
    [SerializeField]
    private bool isPatrolZombie;
    [SerializeField]
    private Transform[] patrolPoints;
    [SerializeField]
    private int currentPoint;
    public bool isPatrol = false;
    
    private AudioSource[] audioSources;

    [SerializeField] private AudioClip[] idle;

    private AudioClip[] walk;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSources = GetComponents<AudioSource>();

        nav.speed = enemySpeed;
    }
    private void Start()
    {
        rigid.centerOfMass = Vector3.zero;
        rigid.inertiaTensorRotation = Quaternion.identity;
        currentEnemySightLength = enemySightLength;
        currentPoint = 0;
    }

    private void Update()
    {
        ChaseDistance();

        enemySightLevel = NoiseSystemManager.GetInstance().GetFearLevel();
        SightLevel();

        ChangeIdle();
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    void SightLevel()
    {
        switch(enemySightLevel)
        {
            case 1:
                currentEnemySightLength = enemySightLength;
                break;
            case 2:
                currentEnemySightLength = enemySightLength * 1.5f;
                break;
            case 3:
                currentEnemySightLength = enemySightLength * 2f;
                break;
            case 4:
                currentEnemySightLength = enemySightLength * 2.5f;
                break;
        }
    }

    void ChaseDistance()
    {
        //nav.stoppingDistance = 2;

        if (Vector3.Distance(target.position, gameObject.transform.position) <= currentEnemySightLength)
        {
            animator.SetBool("IsWalk", true);
            nav.SetDestination(new Vector3(target.position.x, 0, target.position.z));
            //this.transform.LookAt(target);
            isChase = true;
            isPatrol = false;
            if (Vector3.Distance(target.position, gameObject.transform.position) <= enemyAttackLength)
            {
                animator.SetBool("IsWalk", false);
                animator.SetBool("IsAttack", true);

                Debug.Log("공격중");
            }
            else
            {
                animator.SetBool("IsAttack", false);
                animator.SetBool("IsWalk", true);
            }
        }
        else
        {
            if (isPatrolZombie)
            {
                PatrolPoint();
            }
            else
            {
                nav.SetDestination(this.transform.position);
                animator.SetBool("IsWalk", false);
            }

            isChase = false;
        }
    }
    void PatrolPointNew()
    {
        animator.SetBool("IsWalk", true);
        nav.stoppingDistance = 0;
        isPatrol = true;

        if (patrolPoints[currentPoint].position.x != transform.position.x && patrolPoints[currentPoint].position.z != transform.position.z)
        {
            nav.SetDestination(patrolPoints[currentPoint].transform.position);
        }
        else
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }

    void PatrolPoint()
    {
        animator.SetBool("IsWalk", true);
        isPatrol = true;

        if (Vector3.Distance(patrolPoints[currentPoint].position, transform.position) >= nav.stoppingDistance)
        {
            nav.SetDestination(patrolPoints[currentPoint].transform.position);
        }
        else
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }

    void FreezeVelocity()
    {
        if (isChase||isPatrol)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void ChangeIdle()
    {
        if (!audioSources[0].isPlaying)
        {
            audioSources[0].clip = idle[Random.Range(0, idle.Length - 1)];
            audioSources[0].Play();
        }
        else
        {
            return;
        }
    }

    public void PlayWalk()
    {
        walk = Resources.LoadAll<AudioClip>("Sound/FX/Zombie");

        int i = Random.Range(0, walk.Length - 1);
        audioSources[1].clip = walk[i];
        audioSources[1].Play();
    }

}
