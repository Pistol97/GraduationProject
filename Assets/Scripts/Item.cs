using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//scriptableObject = 게임 오브젝트에 붙힐 필요없음
[CreateAssetMenu(fileName = "New Item =", menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;//아이템의 이름
    public ItemType itemType;//아이템의 유형
    public Sprite itemImage;//아이템의 이미지
    public GameObject itemPrefab;//아이템의 프리팹

    public enum ItemType
    {
        Useable,
        Ingredient,
        ETC
    }
}
