using System.Collections;
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

    public RaycastHit Hit
    {
        get
        {
            return hit;
        }
    }

    //텍스트 출력
    [SerializeField]
    private Text actionText;

    public Text ActionText
    {
        get
        {
            return actionText;
        }

        set
        {
            actionText = value;
        }
    }

    [SerializeField]
    private Inventory inven;

    //테스트
    [Header("테스트")]
    public bool questcomplete = false;
    public string questItemName;
    [SerializeField] GameObject txt_QuestComplete;

    private GameObject _gameObject;

    [Header("아이템 습득시 소음 증가량")]
    public float addamount = 100f;

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
            if (hit.transform.CompareTag("Interactable"))
            {
                Debug.Log("Use Interactable Object");

                if("End_Gate" == hit.transform.name)
                {
                    StartCoroutine(transform.parent.GetComponent<Player>().Fade.GameEnd());
                }

                if (null != hit.transform.GetComponent<ILockedObject>())
                {
                    hit.transform.GetComponent<ILockedObject>().TryUnlock(inven.UseKey("열쇠"));
                    hit.transform.GetComponent<ILockedObject>().TryUnlock(inven.UseKey("패널키"));
                }

                hit.transform.GetComponent<IInteractable>().ObjectInteract();
            }
        }
    }
    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hit.transform)
            {
                Debug.Log(_gameObject.GetComponent<ItemPickUp>().item.itemName + "획득했습니다.");

                ////퀘스트 아이템인지 확인
                //if (_gameObject.GetComponent<ItemPickUp>().item.itemName == QuestDataController.GetInstance().GetQuestItem())
                //{
                //    QuestDataController.GetInstance().SetQuest(1);
                //    questcomplete = true;
                //    txt_QuestComplete.SetActive(true);
                //    AudioMgr.Instance.PlaySound("QuestComplete");
                //    Invoke("QuestCompleteSetActiveFalse", 2);
                //}

                inven.AcquireItem(_gameObject.GetComponent<ItemPickUp>().item);
                AudioMgr.Instance.PlaySound("PickUp");
                NoiseSystemManager.GetInstance().AddFear(addamount);//소음 수치 증가
                Destroy(_gameObject);
                InfoDisappear();
            }
        }
    }
    void QuestCompleteSetActiveFalse()
    {
        txt_QuestComplete.SetActive(false);
    }

    private void CheckItem()
    {
        var ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.transform.CompareTag("item"))
            {
                ItemInfoAppear();
            }
            if (hit.transform.CompareTag("Interactable"))
            {
                actionText.gameObject.SetActive(true);
                actionText.text = "사용 " + "<color=yellow>" + "(E)" + "</color>";
            }
            //if (hit.transform.CompareTag("Jumpable"))
            //{
            //    actionText.gameObject.SetActive(true);
            //    actionText.text = "뛰어넘기 " + "<color=yellow>" + "(F)" + "</color>";
            //}
        }
        else
        {
            InfoDisappear();
        }
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        _gameObject = hit.transform.gameObject;
        actionText.gameObject.SetActive(true);
        actionText.text = _gameObject.GetComponent<ItemPickUp>().item.itemName + "획득 " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        _gameObject = null;
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