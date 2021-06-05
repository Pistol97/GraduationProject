using UnityEngine;

public class TextEventTrigger : MonoBehaviour
{
    [SerializeField] private string dialogue;

    private EventMessage _eventMessage;

    private void Start()
    {
        _eventMessage = FindObjectOfType<EventMessage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _eventMessage.DisplayMessage(dialogue);
            Destroy(gameObject);
        }
    }
}
