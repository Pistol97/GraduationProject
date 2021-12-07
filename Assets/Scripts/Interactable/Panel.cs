using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour, IInteractable, ILockedObject
{
    [SerializeField] private bool _isLocked;
    [SerializeField] private string _necessaryKey;

    [SerializeField] private GameObject[] EventObjects;

    public void TryUnlock(string name)
    {
        if (name == _necessaryKey)
        {
            _isLocked = false;

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
        if (name == _necessaryKey)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    //인터페이스 함수
    public void ObjectInteract()
    {
        if (_isLocked)
        {
            FindObjectOfType<EventMessage>().DisplayMessage("Need Panel Key");
            //audioSource.clip = doorSounds[2];
            //audioSource.Play();
        }

        else
        {
            foreach(var obj in EventObjects)
            {
                Destroy(obj);
            }
        }
    }
}
