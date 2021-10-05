using TMPro;
using UnityEngine;

public class TextReveal : MonoBehaviour
{
    private TextMeshPro tm;
    private Sonar sonar;

    private void Awake()
    {
        tm = GetComponentInChildren<TextMeshPro>();
        tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, 0f);
    }

    private void Update()
    {
        if(sonar)
        {
            tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, sonar.dissappear);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sonar"))
        {
            Debug.Log("Text revealing");
            sonar = other.GetComponent<Sonar>();
        }
    }
}
