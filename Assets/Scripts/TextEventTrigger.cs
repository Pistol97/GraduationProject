using UnityEngine;

public class TextEventTrigger : MonoBehaviour
{
    [SerializeField] private string dialogue;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<EventMessage>().DisplayMessage(dialogue);
            Destroy(gameObject);
        }
    }
}
