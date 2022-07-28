using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    public Camera mainCam;
    public Camera attackCam;

    public static bool isAttack;

    //Define List of Cameras
    private List<Camera> cameras;

    void Start()
    {
        cameras.Add(mainCam);
        cameras.Add(attackCam);
    }

    void Update()
    {
        if (!isAttack)
        {
            SwapCamera(mainCam);
        }
        if (isAttack)
        {
            SwapCamera(attackCam);
        }
    }

    public void SwapCamera(Camera cam)
    {
        foreach (Camera c in cameras)
        {
            c.enabled = false;
        }
        cam.enabled = true;
    }

    public void SwapInPosition()
    {

    }
}
