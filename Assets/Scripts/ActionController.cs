using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;//습득가능한 거리

    private bool pickupActivated = false;//습득 가능할 시 true

    private RaycastHit hitInfo; //충돌체 정보 저장

    //아이템 레이어에만 반응하도록 레이어 마스크를 설정
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory inven;

    private void Start()
    {
        Cursor.visible = false;                     //마우스 커서가 보이지 않게 함
        Cursor.lockState = CursorLockMode.Locked;   //마우스 커서를 고정시킴
    }

    private void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CanPickUp()
    {
        if(pickupActivated)
        {
            if(hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득했습니다.");
                inven.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    private void CheckItem()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if(hitInfo.transform.tag == "item")
            {
                ItemInfoAppear();
            }
        }
        else
        {
            InfoDisappear();
        }
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득 " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
}
