using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    //필요한 컴포넌트
    [SerializeField]
    private GameObject go_InventoryBase;

    [SerializeField]
    private GameObject go_SlotParent;

    //슬롯들
    private Slot[] slots;

    private void Start()
    {
        slots = go_SlotParent.GetComponentsInChildren<Slot>();
    }

    private void Update()
    {
        //TryOpenInventory();
        InputNumber();
    }

    private void InputNumber()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseItem(4);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseItem(5);
        }
    }

    private void UseItem(int _num)
    {
        //itemtype이 useable이 아니면 리턴
        if(slots[_num-1].item.itemType.ToString()!="Useable")
        {
            FindObjectOfType<EventMessage>().DisplayMessage("사용 할 수 없는 아이템");
            return;
        }
        if(slots[_num-1].itemCount>0)
        {
            if("EnergyCell" == slots[_num - 1].item.itemName)
            {
                FindObjectOfType<Player>().UseCell(50);
            }
            FindObjectOfType<EventMessage>().DisplayMessage(slots[_num - 1].item.itemName + " 사용");
            slots[_num - 1].SetSlotCount(-1);
        }
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryActivated = !inventoryActivated;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            if (inventoryActivated)
            {
                //OpenInventory();
            }
            else
            {
                //CloseInventory();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item item, int count = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //이미 존재하는 아이템일 경우
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == item.itemName)
                {
                    slots[i].SetSlotCount(count);
                    return;
                }
            }
            
            //아이템 새로 추가
            else
            {
                slots[i].AddItem(item, count);
                return;
            }
        }

    }
}
