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

    [Header("선택 비선택 시각적으로 보이게,,,")]
    [SerializeField]
    private Material highlightMaterial;//마우스 올렸을때의 material

    [SerializeField]
    private Material defaultMaterial;//마우스 올리기전 기본 material

    private Transform _selection;//하이라이트될 오브젝트 트렌스폼

    private void Start()
    {
        //화면 중간 벡터, 화면의 중간부분을 찾아서 레이를 쏘기 위함
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);

        Cursor.visible = false;                     //마우스 커서가 보이지 않게 함
        Cursor.lockState = CursorLockMode.Locked;   //마우스 커서를 고정시킴
    }

    private void Update()
    {
        //하이라이트 비활성
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }

        CheckItem();
        TryAction();
    }
    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
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
            var selection = hit.transform;

            if (hit.transform.tag == "item")
            {
                ItemInfoAppear();

                var selectionRenderer = selection.GetComponent<Renderer>();

                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }

                _selection = selection;
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
}
