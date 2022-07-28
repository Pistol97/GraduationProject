using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour, IInteractable
{
    private Animator animator;

    [SerializeField] private bool _isLockedGate;

    [SerializeField] private NavmeshPathDraw navPath;
    [SerializeField] private int gateNum = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navPath = GetComponent<NavmeshPathDraw>();

        PlayerPrefs.SetInt("Gate", 0);
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Gate_Close"))
        {
            AudioManager.Instance.PlaySound("GateClose");
        }

    }

    //인터페이스 함수
    public void ObjectInteract()
    {
        if (_isLockedGate)
        {
            AccessDenied();
        }

        else
        {
            OpenGate();
        }
    }

    private void OpenGate()
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Gate_Open") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Gate_Close"))
        {
            Debug.Log("Door Open");
            animator.SetBool("IsOpen", true);

            AudioManager.Instance.PlaySound("GateOpen");
        }
    }

    private void AccessDenied()
    {
        FindObjectOfType<EventMessage>().DisplayMessage("Access Denied");
        
        AudioManager.Instance.PlaySound("Error");
    }
}
