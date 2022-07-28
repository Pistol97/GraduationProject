using UnityEngine;

public class TextEventTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] _eventObjects;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _audioSource.Play();

            foreach(var obj in _eventObjects)
            {
                obj.SetActive(true);
            }

            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnDestroy()
    {
        foreach(var obj in _eventObjects)
        {
            Destroy(obj);
        }
    }
}
