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

    public float enemySightLength = 10f;
    public float currentEnemySightLength = 10f;
    public float enemyAttackLength = 3f;
    public int enemySightLevel = 1;

    [SerializeField]
    private Transform target;
    private Animator animator;
    public bool isChase = false;

    private AudioSource[] audioSources;

    [SerializeField] private AudioClip[] idle;

    private AudioClip[] walk;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSources = GetComponents<AudioSource>();

        nav.speed = 1.0f;
    }
    private void Start()
    {
        currentEnemySightLength = enemySightLength;
    }

    private void Update()
    {
        ChaseDistance();
        ChangeIdle();

        enemySightLevel = NoiseSystemManager.GetInstance().GetFearLevel();
        SightLevel();
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
        if (Vector3.Distance(target.position, gameObject.transform.position) <= currentEnemySightLength)
        {
            animator.SetBool("IsWalk", true);
            nav.SetDestination(target.position);
            isChase = true;
            if (Vector3.Distance(target.position, gameObject.transform.position) <= enemyAttackLength)
            {
                animator.SetBool("IsAttack", true);
            }
            else
            {
                animator.SetBool("IsAttack", false);
            }
        }
        else
        {
            nav.SetDestination(this.transform.position);
            animator.SetBool("IsWalk", false);
            isChase = false;
        }
    }

    void FreezeVelocity()
    {
        if (isChase)
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
