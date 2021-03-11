using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    private SphereCollider sphereCollider;

    private float noiseRadius;
    public float NoiseRadius
    {
        set
        {
            noiseRadius = value;
        }
    }

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        //sphereCollider.radius = noiseRadius;
    }
}
