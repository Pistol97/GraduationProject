using UnityEngine;

public class Door : MonoBehaviour, IInteractable, ILockedObject
{
    private Animator animator;
    private AudioSource audioSource;

    [SerializeField] private bool _isLockedDoor;
    [SerializeField] private string _necessaryKey;

    [SerializeField] private AudioClip[] doorSounds;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TryUnlock(string name)
    {
        if(name == _necessaryKey)
        {
            _isLockedDoor = false;

            AudioMgr.Instance.PlaySound("Unlock");
            FindObjectOfType<EventMessage>().DisplayMessage("Use " + name);
        }

        else
        {
            return;
        }
    }

    public bool IsPair(string name)
    {
        if(name == _necessaryKey)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public void ObjectInteract()
    {
        if(_isLockedDoor)
        {
            FindObjectOfType<EventMessage>().DisplayMessage("It's locked");
            audioSource.clip = doorSounds[2];
            audioSource.Play();
        }


        else
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


}
