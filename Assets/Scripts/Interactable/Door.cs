using UnityEngine;

public class Door : LockedObject, IInteractable
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

        _isLocked = _isLockedDoor;
    }

    public override void TryUnlock(Inventory inventory)
    {
        if (inventory.FindItemWithName(_necessaryKey) && _isLocked)
        {
            inventory.UseKey(_necessaryKey);

            _isLocked = false;

            AudioMgr.Instance.PlaySound("Unlock");
            FindObjectOfType<EventMessage>().DisplayMessage("Use " + _necessaryKey);
        }

        else
        {
            FindObjectOfType<EventMessage>().DisplayMessage("It's locked");

            audioSource.clip = doorSounds[2];
            audioSource.Play();
            return;
        }
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
