using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaptopFin : MonoBehaviour, IInteractable
{
    [Header("이벤트 메시지 작성")]
    [SerializeField] private GameObject finishMsg;
    [SerializeField] private Button finishButton;
    [SerializeField] private GameObject crosshair;
    public void ObjectInteract()
    {
        Time.timeScale = 0f;//시간 멈춤

        finishMsg.gameObject.SetActive(true);
        finishButton.gameObject.SetActive(true);
        crosshair.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
