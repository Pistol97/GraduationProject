using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : LockedObject, IInteractable
{
    [SerializeField] private bool _isLockedPanel;
    [SerializeField] private string _necessaryKey;

    [SerializeField] private GameObject[] EventObjects;

    [SerializeField] private GameObject door;

    private void Awake()
    {
        _isLocked = _isLockedPanel;
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
            FindObjectOfType<EventMessage>().DisplayMessage("Need Panel Key");
            return;
        }
    }

    //인터페이스 함수
    public void ObjectInteract()
    {
        foreach (var obj in EventObjects)
        {
            Destroy(obj);
        }

        GetComponent<AudioSource>().Play();
        FindObjectOfType<EventMessage>().DisplayMessage("Reset alert system");
        door.GetComponent<LockedObject>().IsLocked = false;

        GetComponent<BoxCollider>().enabled = false;

    }
}
