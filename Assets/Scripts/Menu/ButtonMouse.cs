using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMouse : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] int buttonIndex;
    [SerializeField] string sceneName;

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneMgr.OpenScene(sceneName);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(menuButtonController.index!=buttonIndex)
        {
            menuButtonController.SetIndex(buttonIndex);
            Debug.Log("선택");
        }
    }

    private void OnMouseEnter()
    {
        menuButtonController.SetIndex(buttonIndex);
        Debug.Log("클릭");
    }
}
