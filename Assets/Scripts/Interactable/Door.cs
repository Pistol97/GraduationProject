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

            AudioManager.Instance.PlaySound("Unlock");

            FindObjectOfType<EventMessage>().DisplayMessage("Use " + _necessaryKey);
        }

        else
        {
            FindObjectOfType<EventMessage>().DisplayMessage("It's locked");

            AudioManager.Instance.PlaySound("DoorLocked");
            return;
        }
    }

    public void ObjectInteract()
    {
        if (!animator.GetBool("IsOpen"))
        {
            animator.SetBool("IsOpen", true);
            AudioManager.Instance.PlaySound("DoorOpen");
        }

        else
        {
            animator.SetBool("IsOpen", false);
            AudioManager.Instance.PlaySound("DoorClose");
        }
    }


}
