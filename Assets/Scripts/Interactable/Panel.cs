using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour, IInteractable
{
    [SerializeField] private Inventory inven;

    //인터페이스 함수
    public void ObjectInteract()
    {
        if(inven.FindItemWithName("PanelKey"))
        {
            inven.UseItemWithName("PanelKey");
        }
    }
}
