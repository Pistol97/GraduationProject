using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : MonoBehaviour,IInteractable
{
    [SerializeField]
    private GameObject Bar_Fear;
    [SerializeField]
    private GameObject Bar_Sonar;
    [SerializeField]
    private GameObject SonarDisplay;

    public void ObjectInteract()
    {
        Debug.Log("HELMET");
        Bar_Fear.gameObject.SetActive(true);
        Bar_Sonar.gameObject.SetActive(true);
        SonarDisplay.gameObject.SetActive(true);
        Destroy(this.transform.gameObject);
    }
}
