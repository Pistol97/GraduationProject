using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCamera : MonoBehaviour 
{
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool lockCursor = true;

    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;
    private bool m_cursorIsLocked = true;

    private Vector3 m_MoveDir = Vector3.zero;

    [SerializeField]
    private float m_WalkSpeed;

    private Vector2 m_Input;
    private Camera m_Camera;
    private Vector3 m_OriginalCameraPosition;

    private void Start()
    {
        m_CharacterTargetRot = this.transform.transform.localRotation;
        m_CameraTargetRot = Camera.main.transform.localRotation;


        m_Camera = Camera.main;

    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    private void GetInput(out float speed)
    {

        // Read input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        // set the desired speed to be walking or running
        speed = m_WalkSpeed;
        m_Input = new Vector2(horizontal, vertical);

        // normalize input if it exceeds 1 in combined length:
        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }

       
    }

    private void UpdateCameraPosition(float speed)
    {
        Vector3 newCameraPosition;
      
      
          
            newCameraPosition = m_Camera.transform.localPosition;
            newCameraPosition.y = m_Camera.transform.localPosition.y;
       

    }

    private void Update()
    {
       

        float speed;
        GetInput(out speed);

        Vector3 desiredMove = Camera.main.transform.forward * m_Input.y + transform.right * m_Input.x;

        m_MoveDir.x = desiredMove.x * speed;
        m_MoveDir.y = desiredMove.y * speed;
        m_MoveDir.z = desiredMove.z * speed;

        this.transform.position += m_MoveDir * Time.fixedDeltaTime;
       // m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);
        UpdateCameraPosition(speed);
        LookRotation();
    }

    public void LookRotation()
    {
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

        if (clampVerticalRotation)
            m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);


        this.transform.transform.localRotation = m_CharacterTargetRot;
        Camera.main.transform.localRotation = m_CameraTargetRot;
       

       

        UpdateCursorLock();
    }

}
