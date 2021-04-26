using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("이동 관련 변수")]
    [Tooltip("기본이동속도")]//유니티 인스펙터에서 아래 변수에 마우스를 올렸을때 문자열이 뜸
    public float MoveSpeed = 2.0f; //이동속도
    [Tooltip("달리기속도")]
    public float RunSpeed = 3.5f;//달리기속도
    public float DirectionRotateSpeed = 100.0f;//이동방향을 변경하기 위한 속도
    public float BodyRotateSpeed = 2.0f;//몸통의 방향을 변경하기 위한 속도

    [Range(0.01f, 5.0f)]//밑의 변수는 Range()안의 범위의 수만 가질 수 있다
    public float VelocityChangeSpeed = 0.1f;//속도가 변경되기 위한 속도(0이되면 안됌)
    private Vector3 CurrentVelocity = Vector3.zero;
    private Vector3 MoveDirection = Vector3.zero;
    private CharacterController myCharacterController = null;
    private CollisionFlags collisionFlags = CollisionFlags.None;
    private float gravity = 9.8f; //중력값
    private float verticalSpeed = 0.0f; //수직 속도

    // Start is called before the first frame update
    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();


    }

    // Update is called once per frame
    void Update()
    {
        //이동
        Move();
        //회전
        BodyDirectionChange();
        //중력적용
        ApplyGravity();
    }
    /// <summary>
    /// 이동 관련 함수
    /// </summary>
    private void Move()
    {
        //메인 카메라 태그를 가지고 있는 카메라의 트랜스폼 컴포넌트.
        Transform CameraTransform = Camera.main.transform;
        //카메라가 바라보는 방향이 월드상에서는 어떤 방향인지 얻어옴
        Vector3 forward = CameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0.0f;
        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

        float vertical = Input.GetAxis("Vertical");//키보드의 위,아래,w,s, -1~1
        float horizontal = Input.GetAxis("Horizontal");//키보드의 좌,우,a,d, -1~1
        //이동을 원하는 방향
        Vector3 targetDirection = horizontal * right + vertical * forward;
        //현재 이동하는 방향에서 원하는 방향으로 조금씩 회전을 하게 된다.
        MoveDirection = Vector3.RotateTowards(MoveDirection, targetDirection, DirectionRotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000.0f);
        //크기를 없앤 방향만 가져온다.
        MoveDirection = MoveDirection.normalized;
        //이동 속도
        float speed = MoveSpeed;

        //중력 벡터
        Vector3 gravityVector = new Vector3(0.0f, verticalSpeed, 0.0f);

        //이번 프레임에 움직일 양
        Vector3 moveAmount = (MoveDirection * speed * Time.deltaTime)+gravityVector;
        //실제 이동
        collisionFlags = myCharacterController.Move(moveAmount);

    }
    /// <summary>
    /// 캐릭터의 이동 관련 변수 화면에 표시
    /// </summary>
    private void OnGUI()
    {
        //충돌정보
        GUILayout.Label("충돌 :" + collisionFlags.ToString());

        GUILayout.Label("현재 속도 :" + GetVelocitySpeed().ToString());

        if (myCharacterController != null&& myCharacterController.velocity != Vector3.zero)
        {
            //현재 내 캐릭터가 이동하는 방향(+크기)
            GUILayout.Label("current Velocity Vector :" + myCharacterController.velocity.ToString());
            //현재 내 속도
            GUILayout.Label("current Velocity Magnitude :" + myCharacterController.velocity.magnitude.ToString());
        }
    }
    /// <summary>
    /// 현재 내 캐릭터의 이속을 얻어온다.
    /// </summary>
    /// <returns></returns>
    private float GetVelocitySpeed()
    {
        //멈춰있다면
        if(myCharacterController.velocity==Vector3.zero)
        {
            //현재 속도 = 0
            CurrentVelocity = Vector3.zero;
        }
        else
        {
            Vector3 goalVelocity = myCharacterController.velocity;
            goalVelocity.y = 0.0f;
            CurrentVelocity = Vector3.Lerp(CurrentVelocity, goalVelocity,
            VelocityChangeSpeed * Time.fixedDeltaTime);
        }

        //currentVelocity의 크기 리턴
        return CurrentVelocity.magnitude;
    }
    /// <summary>
    /// 몸통의 방향을 이동방향으로 바꾼다.
    /// </summary>
    void BodyDirectionChange()
    {
        //움직임이 있다면
        if(GetVelocitySpeed()>0.0f)
        {
            Vector3 newForward = myCharacterController.velocity;
            newForward.y = 0.0f;
            transform.forward = Vector3.Lerp(transform.forward, newForward, BodyRotateSpeed * Time.deltaTime);
        }
    }
    /// <summary>
    /// 중력 적용
    /// </summary>
    void ApplyGravity()
    {
        //CollidedBelow가 세팅되었다면(바닥에 붙었다면)
        if((collisionFlags & CollisionFlags.CollidedBelow)!=0)
        {
            verticalSpeed = 0.0f;
        }else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }
    }

}
