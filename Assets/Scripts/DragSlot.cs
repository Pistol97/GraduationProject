using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;

    public Slot dragSlot;

    //아이템 이미지
    [SerializeField]
    private Image _imageItem;

    private void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image itemmImage)
    {
        _imageItem.sprite = itemmImage.sprite;
        SetColor(1);
    }

    public void SetColor(float alpha)
    {
        Color color = _imageItem.color;
        color.a = alpha;
        _imageItem.color = color;
    }
}
