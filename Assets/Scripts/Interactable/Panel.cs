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
            FindObjectOfType<EventMessage>().DisplayMessage(name + "를 사용했다");
        }

        else
        {
            return;
        }
    }

    //인터페이스 함수
    public void ObjectInteract()
    {
        if (_isLocked)
        {
            FindObjectOfType<EventMessage>().DisplayMessage("조작하려면 열쇠가 필요하다");
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
