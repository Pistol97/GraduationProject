using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMouse : MonoBehaviour
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] int buttonIndex;

    private void OnMouseEnter()
    {
        menuButtonController.SetIndex(buttonIndex);
        Debug.Log("클릭");
    }
}
