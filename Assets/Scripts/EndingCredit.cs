using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCredit : MonoBehaviour
{
    [SerializeField] GameObject Canvas;

    public void CreditEnd()
    {
        Canvas.SetActive(true);
    }

    public void ResetButton()
    {
        PlayerPrefs.DeleteAll();
    }
}
