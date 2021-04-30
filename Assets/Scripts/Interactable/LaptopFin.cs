using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaptopFin : MonoBehaviour,IInteractable
{
    [Header("이벤트 메시지 작성")]
    [SerializeField] private GameObject finishMsg;
    [SerializeField] private Button finishButton;
    [SerializeField] private GameObject crosshair;
    public void ObjectInteract()
    {
        if (!Gate.level1)
        { 
            FindObjectOfType<EventMessage>().DisplayMessage("접근권한없음");
            return;
        }

        if (!Gate.level2)
        {
            FindObjectOfType<EventMessage>().DisplayMessage("접근권한없음");
            return;
        }

        Time.timeScale = 0f;//시간 멈춤

        finishMsg.gameObject.SetActive(true);
        finishButton.gameObject.SetActive(true);
        crosshair.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
