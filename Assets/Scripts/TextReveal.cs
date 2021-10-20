using TMPro;
using UnityEngine;

public class TextReveal : MonoBehaviour
{
    private TextMeshPro _tm;
    private Sonar _sonar;

    private void Awake()
    {
        _tm = GetComponentInChildren<TextMeshPro>();
        _tm.color = new Color(_tm.color.r, _tm.color.g, _tm.color.b, 0f);
    }

    private void Update()
    {
        if(_sonar)
        {
            _tm.color = new Color(_tm.color.r, _tm.color.g, _tm.color.b, _sonar.dissappear);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sonar"))
        {
            Debug.Log("Text revealing");
            _sonar = other.GetComponent<Sonar>();
        }
    }
}
