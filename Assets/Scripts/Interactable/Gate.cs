using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour, IInteractable
{
    private Animator animator;
    private Animator handle;

    [SerializeField] private AudioClip[] gateSounds;
    private AudioSource audioSource;

    private bool isActivate;

    [SerializeField] private bool isKeyGate;
    [SerializeField] private bool isKeyGateOpened;
    [SerializeField] private Inventory inven;

    [SerializeField] private NavmeshPathDraw navPath;
    [SerializeField] private int gateNum =0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        handle = transform.GetChild(0).GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        navPath = GetComponent<NavmeshPathDraw>();
        isActivate = false;

        PlayerPrefs.SetInt("Gate", 0);
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
        if (isKeyGate == true && isKeyGateOpened ==false)
        {
            hasKey();
        }
        else if(isKeyGate == true && isKeyGateOpened == true)
        {
            OpenGate();
        }
        else
        {
            OpenGate();
        }
    }

    void hasKey()
    {
        bool haskey = false;

        haskey = inven.FindItemWithName("Key");

        if(haskey == true)
        {
            isKeyGateOpened = true;

            PlayerPrefs.SetInt("Gate",this.gateNum);


            OpenGate();
            inven.UseItemWithName("Key");

        }
        else
        {
            AccessDenied();
        }
    }

    private void OpenGate()
    {
        handle.SetBool("IsPull", true);
        isActivate = true;
        audioSource.clip = gateSounds[0];
    }

    private void AccessDenied()
    {
        FindObjectOfType<EventMessage>().DisplayMessage("접근 거부됨, 더 높은 등급의 권한 필요");
        audioSource.clip = gateSounds[2];
        audioSource.Play();
    }
}
