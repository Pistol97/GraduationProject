using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraAction : MonoBehaviour
{
    public GameObject player;

    public float offsetX = 0f;
    public float offsetY = 25f;
    public float offsetZ = -35f;

    Vector3 cameraPosition;

    public float followSpeed = 2.0f;

    public Camera cameraObj;

    //void Update()
    //{
    //    RotateCamera();
    //}

    private void LateUpdate()
    {
        cameraPosition.x = player.transform.position.x + offsetX;
        cameraPosition.y = player.transform.position.y + offsetY;
        cameraPosition.z = player.transform.position.z + offsetZ;

        transform.position = Vector3.Lerp(transform.position, cameraPosition, followSpeed * Time.deltaTime);

        //RotateCamera();
    }

    void RotateCamera()
    {
        if (Input.GetMouseButton(0))
        {
            cameraObj.transform.RotateAround(player.transform.position,
                                            Vector3.up,
                                            Input.GetAxis("Mouse X") * followSpeed);
            cameraObj.transform.RotateAround(player.transform.position,
                                            Vector3.right,
                                            -Input.GetAxis("Mouse Y") * followSpeed);
        }
    }
}
