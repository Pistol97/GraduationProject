using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private Animator animator;
    private Animator handle;

    [SerializeField] private AudioClip[] doorSound;
    private AudioSource audioSource;

    private bool isActivate;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        handle = transform.GetChild(0).GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isActivate = false;
    }

    private void Update()
    {
        if (isActivate && !handle.GetBool("IsPull"))
        {
            Debug.Log("Door Open");
            animator.SetBool("IsOpen", true);
            audioSource.Play();
            isActivate = false;
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Gate_Close") && !audioSource.isPlaying)
        {
            audioSource.clip = doorSound[1];
            audioSource.Play();
        }
    }

    //인터페이스 함수
    public void ObjectInteract()
    {
        handle.SetBool("IsPull", true);
        isActivate = true;
        audioSource.clip = doorSound[0];
    }
}
