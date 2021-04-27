using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;


[RequireComponent(typeof (CharacterController))]
[RequireComponent(typeof (AudioSource))] 

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float m_WalkSpeed; 
    [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
    [SerializeField] private float m_StepInterval;
    [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.

    private Camera m_Camera;
    private float m_YRotation; 
    private Vector3 m_Input; 
    private Vector3 m_MoveDir = Vector3.zero; 
    private CharacterController m_CharacterController;
    private CollisionFlags m_CollisionFlags;
    private Vector3 m_OriginalCameraPosition;
    private float m_StepCycle;
    private float m_NextStep;
    Camera p_Camera;
    private AudioSource m_AudioSource; 
    public Texture2D cursorArrow;
    float TotalCooldown;
    float ActualCooldown;
    bool OnCooldown;

    // Use this for initialization 
    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Camera = Camera.main;
        m_OriginalCameraPosition = m_Camera.transform.localPosition;
        m_StepCycle = 0f;
        m_NextStep = m_StepCycle/2f;
        m_AudioSource = GetComponent<AudioSource>();
        Cursor.visible = true;
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        this.p_Camera = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
        this.TotalCooldown = 1;
        this.ActualCooldown = 1;
        this.OnCooldown = true;
    }


    // Update is called once per frame
    private void Update()
    {
        RotateView();
        Cursor.visible = true;

        if (this.ActualCooldown > 0)
        {
            this.OnCooldown = true;
            this.ActualCooldown -= Time.deltaTime;
        }
        else if (this.ActualCooldown <= 0)
        {
            this.OnCooldown = false;
        }
        if (this.OnCooldown == false)
        {
            this.ActualCooldown = this.TotalCooldown;
            this.MovethePlayer();
        }
    }


    private void FixedUpdate()
    {
        float speed;
        GetInput(out speed);
        // always move along the camera forward as it is the direction that it being aimed at
        //Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

        // get a normal for the surface that is being touched to move along it
        //RaycastHit hitInfo;
        //Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
       //                     m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        //desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        //m_MoveDir.x = desiredMove.x*speed;
        //m_MoveDir.z = desiredMove.z*speed;

        //m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

        //ProgressStepCycle(speed);
        //UpdateCameraPosition(speed);

        // m_MouseLook.UpdateCursorLock();
    }


    private void ProgressStepCycle(float speed)
    {
        if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
        {
            m_StepCycle += (m_CharacterController.velocity.magnitude + (speed* m_RunstepLenghten))*
                            Time.fixedDeltaTime;
        }

        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        m_NextStep = m_StepCycle + m_StepInterval;

        PlayFootStepAudio();
    }


    private void PlayFootStepAudio()
    {
        if (!m_CharacterController.isGrounded)
        {
            return;
        }
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, m_FootstepSounds.Length);
        m_AudioSource.clip = m_FootstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;
    }


    private void UpdateCameraPosition(float speed)
    {
        Vector3 newCameraPosition;
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
        {
            newCameraPosition = m_Camera.transform.localPosition;
        }
        else
        {
            newCameraPosition = m_Camera.transform.localPosition;
        }
        m_Camera.transform.localPosition = newCameraPosition;
    }


    private void GetInput(out float speed)
    {
        speed = m_WalkSpeed;
    }


    private void RotateView() 
    {
        // m_MouseLook.LookRotation (transform, m_Camera.transform);
    } 


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        //dont move the rigidbody if the character is on top of it
        if (m_CollisionFlags == CollisionFlags.Below) 
        {
            return; 
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
    }

    private void MovethePlayer() { 
    
    }

    public void moveForward(float speed) {
        m_Input = m_Camera.transform.forward;
        Vector3 desiredMove;
        // wektory do poruszania obiektu
        Vector3 forMove = new Vector3(0, 0, 1);
        Vector3 rightMove = new Vector3(1, 0, 0);
        Vector3 backMove = new Vector3(0, 0, -1);
        Vector3 leftMove = new Vector3(-1, 0, 0);

        Quaternion camSettingFor = new Quaternion();
        camSettingFor.w = 1;
        Quaternion camSettingRight = new Quaternion();
        camSettingRight.w = (float)-0.7071068;
        camSettingRight.y = (float)-0.7071068;
        Quaternion camSettingBackr = new Quaternion();
        camSettingBackr.y = 1;
        Quaternion camSettingLeft = new Quaternion();
        camSettingLeft.w = (float)0.7071068;
        camSettingLeft.y = (float)-0.7071068;

        Quaternion p_CameraRot = this.p_Camera.transform.rotation;

        if (p_CameraRot == camSettingFor)
        {
            // kamera do przodu
            transform.Translate(forMove * this.m_WalkSpeed * 1);
            //desiredMove = transform.forward * m_Camera.transform.forward.y;
        }
        else if (p_CameraRot == camSettingRight)
        {
            // kamera w prawo
            transform.Translate(rightMove * this.m_WalkSpeed * 1);
            //desiredMove = transform.forward * m_Camera.transform.forward.y;
        }
        else if (p_CameraRot == camSettingBackr)
        {
            // kamera do ty³u
            transform.Translate(backMove * this.m_WalkSpeed * 1);
            // desiredMove = transform.forward * m_Camera.transform.forward.z;
        }
        else if (p_CameraRot == camSettingLeft)
        {
            // kamera w lewo
            transform.Translate(leftMove * this.m_WalkSpeed * 1);
            // desiredMove = transform.forward * m_Camera.transform.forward.z;
        }
        else {
            //desiredMove = transform.forward * 0;
        }

        // get a normal for the surface that is being touched to move along it
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                            m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            
        //desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        //m_MoveDir.x = desiredMove.x * speed;
        //m_MoveDir.z = desiredMove.z * speed;

        //m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

        //ProgressStepCycle(speed);
        //UpdateCameraPosition(speed);
    }
    public void rotateBack(float speed) { 
        m_Camera.transform.Rotate(0, 180, 0);
    }

    public void rotateLeft(float speed) 
    { 
        m_Camera.transform.Rotate(0, 270, 0); 
    }

    public void rotateRight(float speed)
    {
        m_Camera.transform.Rotate(0, 90, 0);
    }
}

