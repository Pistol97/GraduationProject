using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    Vector3 ScreenCenter;

    [SerializeField]
    private float range;//습득가능한 거리

    private bool pickupActivated = false;//습득 가능할 시 true

    private RaycastHit hit; //충돌체 정보 저장

    //아이템 레이어에만 반응하도록 레이어 마스크를 설정
    [SerializeField]
    private LayerMask layerMask;

    //텍스트 출력
    [SerializeField]
    private Text actionText;

    [SerializeField]
    private Inventory inven;

    private void Start()
    {
        //화면 중간 벡터, 화면의 중간부분을 찾아서 레이를 쏘기 위함
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);

        Cursor.visible = false;                     //마우스 커서가 보이지 않게 함
        Cursor.lockState = CursorLockMode.Locked;   //마우스 커서를 고정시킴
    }
    private void FixedUpdate()
    {
        CheckItem();
        CheckInteractable();
    }

    private void Update()
    {
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //CheckItem();
            CanPickUp();
        }
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hit.transform != null)
            {
                Debug.Log(hit.transform.GetComponent<ItemPickUp>().item.itemName + "획득했습니다.");
                inven.AcquireItem(hit.transform.GetComponent<ItemPickUp>().item);
                Destroy(hit.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    private void CheckItem()
    {
        var ray = Camera.main.ScreenPointToRay(ScreenCenter);

        if (Physics.Raycast(ray, out hit, range, layerMask))
        {
            if (hit.transform.tag == "item")
            {
                ItemInfoAppear();
            }
        }
        else
        {
            InfoDisappear();
        }
    }

    //싱호작용 오브젝트
    private void CheckInteractable()
    {
        var ray = Camera.main.ScreenPointToRay(ScreenCenter);
        if(Physics.Raycast(ray, out hit, range))
        {
            if(hit.transform.CompareTag("Interactable"))
            {
                actionText.gameObject.SetActive(true);
                actionText.text = "사용 " + "<color=yellow>" + "(E)" + "</color>";

                if(Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.GetComponent<IInteractable>().ObjectInteract();
                }
            }
        }
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hit.transform.GetComponent<ItemPickUp>().item.itemName + "획득 " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * range);
    }

    public Vector3 GetFront()
    {
        return transform.forward;
    }
}
