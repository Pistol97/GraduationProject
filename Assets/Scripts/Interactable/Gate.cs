using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour, IInteractable
{
    public static bool level1 = false;
    public static bool level2 = false;

    [SerializeField] private bool isLevel1;
    [SerializeField] private bool isLevel2;

    [Header("할당 퀘스트 번호")]
    [SerializeField] private bool isStageDoor;
    [SerializeField] private bool quest1;
    [SerializeField] private bool quest2;
    [SerializeField] private bool quest3;

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

        if(isStageDoor == true)
        {
            QuestStageUnlock();
        }
    }

    void QuestStageUnlock()
    {
        int currentQuestNum = QuestDataController.GetInstance().GetCurrentQuestNpcNum();
        
        switch(currentQuestNum)
        {
            case 1:
                if(quest1 == true)
                {
                    handle.SetBool("IsPull", true);
                    isActivate = true;
                    audioSource.clip = gateSounds[0];
                }
                else
                {
                    AccessDenied();
                }
                break;
            case 2:
                if (quest2 == true)
                {
                    handle.SetBool("IsPull", true);
                    isActivate = true;
                    audioSource.clip = gateSounds[0];
                }
                else
                {
                    AccessDenied();
                }
                break;
            case 3:
                if (quest3 == true)
                {
                    handle.SetBool("IsPull", true);
                    isActivate = true;
                    audioSource.clip = gateSounds[0];
                }
                else
                {
                    AccessDenied();
                }
                break;
            default:
                break;
        }
    }

    private void AccessDenied()
    {
        FindObjectOfType<EventMessage>().DisplayMessage("접근 거부됨, 더 높은 등급의 권한 필요");
        audioSource.clip = gateSounds[2];
        audioSource.Play();
    }
}
