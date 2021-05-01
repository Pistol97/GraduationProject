using UnityEngine;

public class Laptop : MonoBehaviour, IInteractable
{
    private MeshRenderer renderer;
    [SerializeField] private Material turnOn;

    [Header("이벤트 메시지 작성")]
    [SerializeField] private string message;

    [SerializeField] private bool level1Access;
    [SerializeField] private bool level2Access;

    [SerializeField] private Inventory inven;

    [SerializeField] private ItemPickUp item;

    public bool isKey;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    public void ObjectInteract()
    {
        renderer.material = turnOn;
        gameObject.tag = "Untagged";
        GetComponent<AudioSource>().Play();
        FindObjectOfType<EventMessage>().DisplayMessage(message);

        if (level1Access)
        {
            Gate.level1 = true;
        }

        else if (level2Access)
        {
            Gate.level2 = true;
        }
        if(isKey)
        {
            inven.AcquireItem(item.item);
        }
    }
}
