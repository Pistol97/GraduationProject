using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour, IInteractable
{
    public static bool level1 = false;
    public static bool level2 = false;

    [SerializeField] private bool isLevel1;
    [SerializeField] private bool isLevel2;

    private Animator animator;
    private Animator handle;

    [SerializeField] private AudioClip[] gateSounds;
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

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Gate_Close") && !audioSource.isPlaying)
        {
            audioSource.clip = gateSounds[1];
            audioSource.Play();
        }
    }

    //인터페이스 함수
    public void ObjectInteract()
    {
        if (isLevel1)
        {
            if (level1)
            {
                handle.SetBool("IsPull", true);
                isActivate = true;
                audioSource.clip = gateSounds[0];
            }

            else
            {
                AccessDenied();
            }
        }

        else if (isLevel2)
        {
            if (level2)
            {
                handle.SetBool("IsPull", true);
                isActivate = true;
                audioSource.clip = gateSounds[0];
            }

            else
            {
                AccessDenied();
            }
        }

    }

    private void AccessDenied()
    {
        FindObjectOfType<EventMessage>().DisplayMessage("안열어줌");
        audioSource.clip = gateSounds[2];
        audioSource.Play();
    }
}
