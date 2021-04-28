using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private Animator animator;
    private AudioSource audioSource;

    [SerializeField] private AudioClip[] doorSounds;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ObjectInteract()
    {

        if (!animator.GetBool("IsOpen"))
        {
            animator.SetBool("IsOpen", true);
            audioSource.clip = doorSounds[0];
            audioSource.Play();
        }

        else
        {
            animator.SetBool("IsOpen", false);
            audioSource.clip = doorSounds[1];
        }
    }
}
