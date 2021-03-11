using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private Noise noise;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //if(x != 0 || z != 0)
        //{
        //    noise.NoiseRadius = 5.0f;
        //}

        //else
        //{
        //    noise.NoiseRadius = 0.5f;
        //}

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * 5f * Time.deltaTime);
    }
}
