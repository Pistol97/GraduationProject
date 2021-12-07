using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;//획득한 아이템
    public int itemCount;//아이템의 개수
    public Image itemImage;//아이템 이미지

    [SerializeField]
    private Text textCount;
    [SerializeField]
    private GameObject go_CountImage;

    [SerializeField]
    private ToolTip toolTipSlot;

    private void Start()
    {
        toolTipSlot = FindObjectOfType<ToolTip>();
    }

    /// <summary>
    /// 이미지의 투명도 조절
    /// </summary>
    private void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }
    /// <summary>
    /// 아이템 획득
    /// </summary>
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = _item.itemImage;

        go_CountImage.SetActive(true);
        textCount.text = itemCount.ToString();

        SetColor(1);

        //if (_item.itemName == QuestDataController.GetInstance().GetQuestItem())
        //{
        //    QuestDataController.GetInstance().SetQuest(1);
        //}
    }
    /// <summary>
    /// 아이템 개수 조정
    /// </summary>
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        textCount.text = itemCount.ToString();

        if(itemCount<=0)
        {
            ClearSlot();
        }
    }
    /// <summary>
    /// 슬롯 초기화
    /// </summary>
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        textCount.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       //if(eventData.button == PointerEventData.InputButton.Right)
       // {
       //     if(item != null)
       //     {
       //         AudioMgr.Instance.PlaySound("Inventory_Click");
       //         //소모
       //         Debug.Log(item.itemName + "을 사용하였습니다.");
       //         SetSlotCount(-1);
       //     }
       // }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            AudioMgr.Instance.PlaySound("Inventory_Drag");
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (DragSlot.instance.dragSlot!= null)
        {
            AudioMgr.Instance.PlaySound("Inventory_Drop");
            ChangeSlot();
        }
    }

    private void ChangeSlot()
    {
        Item tempItem = item;
        int tempItmeCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (tempItem != null)
        {
            if(tempItem.name == item.name)
            {
                SetSlotCount(tempItmeCount);
                DragSlot.instance.dragSlot.ClearSlot();
                return;
            }

            DragSlot.instance.dragSlot.AddItem(tempItem, tempItmeCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }

    //마우스가 슬롯에 들어갈 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item != null)
        toolTipSlot.ShowToolTip(item, transform.position);
    }

    //마우스가 슬롯에서 빠져나올때
    public void OnPointerExit(PointerEventData eventData)
    {
        toolTipSlot.HideToolTip();
    }
}
