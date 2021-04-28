﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("1인칭 카메라")]
    public float SensitivityX = 5.0f;
    public float SensitivityY = 5.0f;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private float rotationMinX = -80;
    private float rotationMaxX = 80;
    private void LateUpdate()
    {
        //게임 일시정지시 카메라 멈춤..회전한당,,
        if (Time.deltaTime == 0)
        {
            Vector3 cameraRotation = transform.forward;//이상하다,,
            transform.eulerAngles = cameraRotation;
            return;
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        NewFirstView(mouseX, mouseY);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.gameObject.layer = 8;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.gameObject.layer = 0;
        }
    }

    public void NewFirstView(float mouseX, float mouseY)
    {


        rotationY += mouseX * SensitivityX;//마우스 위아래는 카메라의 x축
        rotationX -= mouseY * SensitivityY;//마우스 좌우는 카메라의 y축
        rotationX = ClampAngle(rotationX, rotationMinX, rotationMaxX);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}