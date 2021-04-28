using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour, IInteractable
{
    private MeshRenderer renderer;
    [SerializeField] private Material turnOn;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();    
    }

    public void ObjectInteract()
    {
        renderer.material = turnOn;
        gameObject.tag = "Untagged";
        GetComponent<AudioSource>().Play();
    }
}
