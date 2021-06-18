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

    [SerializeField]
    private GameObject go_QuickSlotParent;

    //슬롯들
    public Slot[] Slots
    {
        get;
        private set;
    }

    //퀵슬롯들
    public Slot[] QuickSlots
    {
        get;
        private set;
    }

    private void Start()
    {
        Slots = go_SlotParent.GetComponentsInChildren<Slot>();
        QuickSlots = go_QuickSlotParent.GetComponentsInChildren<Slot>();
    }

    private void Update()
    {
        TryOpenInventory();
        InputNumber();
    }

    private void InputNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseItem(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseItem(5);
        }
    }

    private void UseItem(int _num)
    {
        //itemtype이 useable이 아니면 리턴
        if (QuickSlots[_num - 1].item.itemType.ToString() != "Useable")
        {
            FindObjectOfType<EventMessage>().DisplayMessage("사용 할 수 없는 아이템");
            AudioMgr.Instance.PlaySound("Error");
            return;
        }
        if (QuickSlots[_num - 1].itemCount > 0)
        {
            if ("EnergyCell" == QuickSlots[_num - 1].item.itemName)
            {
                FindObjectOfType<Player>().UseCell(50);
                AudioMgr.Instance.PlaySound("Use_Battery");
            }
            FindObjectOfType<EventMessage>().DisplayMessage(QuickSlots[_num - 1].item.itemName + " 사용");
            QuickSlots[_num - 1].SetSlotCount(-1);
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
                Time.timeScale = 0f;
                OpenInventory();
            }
            else
            {
                CloseInventory();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1.0f;//기본 시간
            }
        }
    }

    private void OpenInventory()
    {
        AudioMgr.Instance.PlaySound("Open_Inventory");
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        AudioMgr.Instance.PlaySound("Close_Inventory");
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item item, int count = 1)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            //이미 존재하는 아이템일 경우
            if (Slots[i].item != null)
            {
                if (Slots[i].item.itemName == item.itemName)
                {
                    Slots[i].SetSlotCount(count);
                    return;
                }
            }

            //아이템 새로 추가
            else
            {
                Slots[i].AddItem(item, count);
                return;
            }
        }

    }
}
