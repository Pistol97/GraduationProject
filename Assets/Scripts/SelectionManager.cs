﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    Vector3 screenCenter;

    [SerializeField]
    private float range;//습득가능한 거리

    private bool pickupActivated = false;//습득 가능할 시 true

    private RaycastHit hit; //충돌체 정보 저장

    //텍스트 출력
    [SerializeField]
    private Text actionText;

    [SerializeField]
    private Inventory inven;

    //테스트
    [Header("테스트")]
    public bool questcomplete = false;
    public string questItemName;

    private void Start()
    {
        //화면 중간 벡터, 화면의 중간부분을 찾아서 레이를 쏘기 위함
        screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);

        Cursor.visible = false;                     //마우스 커서가 보이지 않게 함
        Cursor.lockState = CursorLockMode.Locked;   //마우스 커서를 고정시킴
    }
    private void FixedUpdate()
    {
        CheckItem();
    }

    private void Update()
    {
        TryAction();
        questItemName = QuestDataController.GetInstance().GetQuestItem();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CanPickUp();
            CheckItem();
        }
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hit.transform != null)
            {
                Debug.Log(hit.transform.GetComponent<ItemPickUp>().item.itemName + "획득했습니다.");

                //퀘스트 아이템인지 확인
                if (hit.transform.GetComponent<ItemPickUp>().item.itemName == QuestDataController.GetInstance().GetQuestItem())
                {
                    QuestDataController.GetInstance().SetQuest(1);
                    questcomplete = true;
                }

                inven.AcquireItem(hit.transform.GetComponent<ItemPickUp>().item);
                Destroy(hit.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    private void CheckItem()
    {
        var ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.transform.tag == "item")
            {
                ItemInfoAppear();
            }
            else if (hit.transform.CompareTag("Interactable"))
            {
                actionText.gameObject.SetActive(true);
                actionText.text = "사용 " + "<color=yellow>" + "(E)" + "</color>";

                if (Input.GetKeyDown(KeyCode.E))
                {

                    Debug.Log("상호작용 클릭");
                    hit.transform.GetComponent<IInteractable>().ObjectInteract();
                    
                }
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
