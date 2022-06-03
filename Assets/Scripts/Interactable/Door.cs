using UnityEngine;

public class Door : LockedObject, IInteractable
{
    private Animator animator;

    [SerializeField] private bool _isLockedDoor;

    [SerializeField] private string _necessaryKey;

    private void Awake()
    {
        animator = GetComponent<Animator>();

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

            AudioMgr.Instance.PlaySound("DoorLocked");
            return;
        }
    }

    public void ObjectInteract()
    {
        if (!animator.GetBool("IsOpen"))
        {
            animator.SetBool("IsOpen", true);
            AudioMgr.Instance.PlaySound("DoorOpen");
        }

        else
        {
            animator.SetBool("IsOpen", false);
            AudioMgr.Instance.PlaySound("DoorClose");
        }
    }


}
