using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item _item;//획득한 아이템
    public int _itemCount;//아이템의 개수
    public Image _itemImage;//아이템 이미지

    [SerializeField]
    private Text textCount;
    [SerializeField]
    private GameObject go_CountImage;

    private void Start()
    {
        _itemImage = GetComponent<Image>();
        go_CountImage = transform.GetChild(1).gameObject;
        textCount = go_CountImage.transform.GetChild(0).GetComponent<Text>();
    }

    /// <summary>
    /// 이미지의 투명도 조절
    /// </summary>
    /// <param name="alpha"></param>
    private void SetColor(float alpha)
    {
        Color color = _itemImage.color;
        color.a = alpha;
        _itemImage.color = color;
    }
    /// <summary>
    /// 아이템 획득
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public void AddItem(Item item, int count = 1)
    {
        _item = item;
        _itemCount = count;
        _itemImage.sprite = item.itemImage;

        go_CountImage.SetActive(true);
        textCount.text = _itemCount.ToString();

        SetColor(1);
    }
    /// <summary>
    /// 아이템 개수 조정
    /// </summary>
    /// <param name="count"></param>
    public void SetSlotCount(int count)
    {
        _itemCount += count;
        textCount.text = _itemCount.ToString();

        if(_itemCount<=0)
        {
            ClearSlot();
        }
    }
    /// <summary>
    /// 슬롯 초기화
    /// </summary>
    private void ClearSlot()
    {
        _item = null;
        _itemCount = 0;
        _itemImage.sprite = null;
        SetColor(0);

        textCount.text = "0";
        go_CountImage.SetActive(false);
    }
}
