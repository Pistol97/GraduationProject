using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("카메라 기본 속성")]
    private Transform myTransform = null;
    public GameObject Target = null;
    private Transform targetTransform = null;
    public GameObject player = null;

    public enum CameraViewPointState { FIRST, SECONE, THIRD}
    public CameraViewPointState CameraState = CameraViewPointState.FIRST;

    [Header("3인칭 카메라")]
    public float Distance = 5.0f; //타겟으로부터 떨어진 거리
    public float Height = 1.5f; //타겟의 위치보다 더 추가적인 높이
    public float HeightDamping = 2.0f;
    public float RotationDamping = 3.0f;

    [Header("2인칭 카메라")]
    public float RotateSpeed = 10.0f;

    [Header("1인칭 카메라")]
    public float SensitivityX = 5.0f;
    public float SensitivityY = 5.0f;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    public Transform FirstCameraSocket = null;

    private void Start()
    {
        myTransform = GetComponent<Transform>();
        if(Target != null)
        {
            targetTransform = Target.transform;
        }
    }
    /// <summary>
    /// 3인칭 카메라
    /// </summary>
    void ThirdView()
    {
        float wantedRotationAngle = targetTransform.eulerAngles.y;//현재 타겟의 y축 각도 값
        float wantedHeight = targetTransform.position.y + Height;//현재 타겟의 높이 + 추가 높이

        float currentRoationAngle = myTransform.eulerAngles.y;//현재 카메라의 y축 각도 값
        float currentHeight = myTransform.position.y;//현재 카메라의 높이 값

        //현재 각도에서 원하는 각도로 댐핑을 준다.
        //currentRoationAngle = Mathf.LerpAngle(currentRoationAngle, wantedRotationAngle, RotationDamping * Time.deltaTime);
        //현재 높이에서 원하는 높이로 댐핑을 준다.
        //currentHeight = Mathf.Lerp(currentHeight, wantedHeight, HeightDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0f, currentRoationAngle, 0f);

        myTransform.position = targetTransform.position;
        myTransform.position -= currentRotation * Vector3.forward * Distance;
        myTransform.position = new Vector3(myTransform.position.x, currentHeight, myTransform.position.z);

        Target.transform.LookAt(targetTransform);
    }
    /// <summary>
    /// 모델 뷰
    /// </summary>
    void SecondView()
    {
        myTransform.RotateAround(targetTransform.position, Vector3.up, RotateSpeed * Time.deltaTime);
        myTransform.LookAt(targetTransform);
    }
    /// <summary>
    /// 1인칭 뷰
    /// </summary>
    void FirstView()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX = myTransform.localEulerAngles.y + mouseX * SensitivityX;
        //마이너스 각도 조절 연산
        rotationX = (rotationX > 180.0f) ? rotationX - 360.0f : rotationX;

        rotationY = rotationY + mouseY * SensitivityY;
        rotationY = (rotationY > 180.0f) ? rotationY - 360.0f : rotationY;

        //카메라 회전 제한(수치조정~!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!)
        //rotationX = Mathf.Clamp(rotationX, -80, 80);

        myTransform.localEulerAngles = new Vector3(-rotationY, rotationX, 0f);

        myTransform.position = FirstCameraSocket.position;

        //메인카메라가 바라보는 방향
        Vector3 dir = transform.localRotation * Vector3.forward;
        //카메라가 바라보는 방향으로 플레이어도 바라보게
        player.transform.localRotation = transform.localRotation;
        //플레이어의 Rotation.x값 0
        player.transform.localRotation = new Quaternion(0, transform.localRotation.y, 0, transform.localRotation.w);

    }
    private void LateUpdate()
    {
        if(Target == null)
        {
            return;
        }
        if(targetTransform == null)
        {
            targetTransform = Target.transform;
        }
        switch(CameraState)
        {
            case CameraViewPointState.THIRD:
                ThirdView();
                break;
            case CameraViewPointState.SECONE:
                SecondView();
                break;
            case CameraViewPointState.FIRST:
                FirstView();
                break;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.gameObject.layer = 8;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.gameObject.layer = 0;
        }
    }
}
