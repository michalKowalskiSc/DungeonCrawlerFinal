using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

#pragma warning disable 618, 649
namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        //[SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        //[SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        //[SerializeField] private float m_JumpSpeed;
        //[SerializeField] private float m_StickToGroundForce;
        //[SerializeField] private float m_GravityMultiplier;
       // [SerializeField] private MouseLook m_MouseLook;
        //[SerializeField] private bool m_UseFovKick;
        //[SerializeField] private FOVKick m_FovKick = new FOVKick();
        //[SerializeField] private bool m_UseHeadBob;
        //[SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        //[SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        private Camera m_Camera;
        //private bool m_Jump;
        private float m_YRotation;
        private Vector3 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        //private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        //private bool m_Jumping;
        private AudioSource m_AudioSource;
        public Texture2D cursorArrow;

        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            //m_FovKick.Setup(m_Camera);
           // m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            //m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			//m_MouseLook.Init(transform , m_Camera.transform);
            Cursor.visible = true;
            Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        }


        // Update is called once per frame
        private void Update()
        {
            RotateView();
            Cursor.visible = true;

        }


        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;

            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

           // m_MouseLook.UpdateCursorLock();
        }


/*        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }*/


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

        public void moveForward(float speed) {
            print(m_Camera.transform.forward);
            m_Input = m_Camera.transform.forward;
            /* Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
             move = m_Camera.transform.TransformDirection(move);
             move = Vector3.ProjectOnPlane(move, Vector3.up);*/
            Vector3 desiredMove;

            if (m_Input.x != 0)
            {
                desiredMove = transform.forward * m_Camera.transform.forward.x;
            }
            else if (m_Input.y != 0)
            {
                desiredMove = transform.forward * m_Camera.transform.forward.y;
            }
            else if (m_Input.z != 0)
            {
                desiredMove = transform.forward * m_Camera.transform.forward.z;
            }
            else {
                desiredMove = transform.forward * 0;
            }

            print(desiredMove);
            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                                m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;

            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);
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
}
