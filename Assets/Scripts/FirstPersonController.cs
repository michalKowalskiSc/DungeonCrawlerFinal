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
    private Boolean isMoving = false;
    private Transform target;

    // Use this for initialization 
    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Camera = Camera.main;
        //m_OriginalCameraPosition = m_Camera.transform.localPosition;
        //m_StepCycle = 0f;
        //m_NextStep = m_StepCycle/2f;
        m_AudioSource = GetComponent<AudioSource>();
        Cursor.visible = true;
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        this.p_Camera = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
    }


    // Update is called once per frame
    private void Update()
    {
        RotateView();
        Cursor.visible = true;

        // Move our position a step closer to the target.
        float step = this.m_WalkSpeed * Time.deltaTime; // calculate distance to move
        if (this.target != null) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                // Swap the position of the cylinder.
                GameObject.Destroy(this.target.gameObject);
                this.target = null;
                this.isMoving = false;
            }      
        }      


    }


    private void FixedUpdate()
    {
        //float speed;
        //GetInput(out speed);
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


    /*private void ProgressStepCycle(float speed)
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

    */
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


    /*private void UpdateCameraPosition(float speed)
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
    */

/*    private void GetInput(out float speed)
    {
       // speed = m_WalkSpeed;
    }*/


    private void RotateView() 
    {
        // m_MouseLook.LookRotation (transform, m_Camera.transform);
    } 


/*    private void OnControllerColliderHit(ControllerColliderHit hit)
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
*/

    public void moveForward(float speed) {
        if (!this.isMoving) {
            this.isMoving = true;
            //m_Input = m_Camera.transform.forward;
            Vector3 desiredMove;
            // wektory do poruszania obiektu
            Vector3 forMove = new Vector3(0, 0, 1);
            Vector3 rightMove = new Vector3(1, 0, 0);
            Vector3 backMove = new Vector3(0, 0, -1);
            Vector3 leftMove = new Vector3(-1, 0, 0);

            // zmienne do przesuniêcia kamery i awatara
            float xTarget = this.m_CharacterController.transform.position.x;
            float yTarget = this.m_CharacterController.transform.position.y;
            float zTarget = this.m_CharacterController.transform.position.z;
            float xCamera = xTarget;
            float yCamera = 1.5f;
            float zCamera = zTarget;
            float y = (float)Math.Round(this.p_Camera.transform.rotation.y, 1);

            if (y == 0) // kamera do przodu
            {
                zTarget += 1;
            }
            else if (y == 0.7f) // kamera w prawo
            {
                xTarget += 1;
            }
            else if (y == 1)// kamera do ty³u
            {
                zTarget -= 1;
            }
            else if (y == -0.7f)
            {
                xTarget -= 1;
            }

            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            this.target = cylinder.transform;
            this.target.transform.localScale = new Vector3(0.15f, 1.0f, 0.15f);
            this.target.transform.position = new Vector3(xTarget, yTarget, zTarget);
            Camera.main.transform.position = new Vector3(xCamera, yCamera, zCamera);
        }
        
        
        // get a normal for the surface that is being touched to move along it
        //RaycastHit hitInfo;
        //Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
        //                    m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);

        //desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        //m_MoveDir.x = desiredMove.x * speed;
        //m_MoveDir.z = desiredMove.z * speed;

        //m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

        //ProgressStepCycle(speed);
        //UpdateCameraPosition(speed);
    }
    public void rotateBack(float speed) {
        if (!this.isMoving) {
            m_Camera.transform.Rotate(0, 180, 0);
        }        
    }

    public void rotateLeft(float speed) 
    {
        if (!this.isMoving) {
            m_Camera.transform.Rotate(0, 270, 0);
        }

    }

    public void rotateRight(float speed)
    {
        if (!this.isMoving) {
            m_Camera.transform.Rotate(0, 90, 0);
        }            
    }

    private void OnTriggerEnter(Collider other) { 
 
    }

    private void OnCollisionEnter(Collision collision) { 
    }
}

