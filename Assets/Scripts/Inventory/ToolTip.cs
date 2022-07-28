using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    [SerializeField] private GameObject toolTip;
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemDescription;

    public void ShowToolTip(Item _item, Vector3 _position)
    {
        toolTip.SetActive(true);
        _position += new Vector3(toolTip.GetComponent<RectTransform>().rect.width*0.7f,
                                 -toolTip.GetComponent<RectTransform>().rect.height * 0.7f, 0f);
        toolTip.transform.position = _position;


        itemName.text = _item.itemName;
        itemDescription.text = _item.itemDescription;
    }

    public void HideToolTip()
    {
        toolTip.SetActive(false);
    }

}
