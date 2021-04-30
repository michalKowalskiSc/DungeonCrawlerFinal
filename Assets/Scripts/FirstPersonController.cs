using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;


[RequireComponent(typeof (CharacterController))]
[RequireComponent(typeof (AudioSource))] 

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float m_WalkSpeed; 
    // [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
    //[SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.

    private Camera m_Camera;
    //private float m_YRotation; 
    //private Vector3 m_Input; 
    //private Vector3 m_MoveDir = Vector3.zero; 
    private CharacterController m_CharacterController;
    //private CollisionFlags m_CollisionFlags;
    //private Vector3 m_OriginalCameraPosition;
    //private int m_NextStepSound;
    //Camera p_Camera;
    private AudioSource m_AudioSource; 
    public Texture2D cursorArrow;
    private Boolean isMoving = false;
    private Vector3 targetPosition;
    private int dir;
    //private bool playsteps;
    //float distance;
    //int platLayer;
    public Sprite sprDisarmDefault;
    public Sprite sprDisarmGreen;

    // Use this for initialization 
    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Camera = Camera.main;
        //m_NextStepSound = 1;
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.Play();
        Cursor.visible = true;
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        //this.p_Camera = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();

        //Distance is slightly larger than the
        //distance = m_CharacterController.radius + 0.2f;

        //First add a Layer name to all platforms (I used MovingPlatform)
        //Now this script won't run on regular objects, only platforms.
        //platLayer = LayerMask.NameToLayer("MovingPlatform");
    }


    // Update is called once per frame
    private void Update()
    {
        RotateView();
        Cursor.visible = true;
    }

    private void FixedUpdate()
    {
        if (this.isMoving)// && this.target != null)
        {
            if (this.dir == 0)
            {
                if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out _, 0.5f))
                {
                    print("There is something in front of the object! F");
                    this.isMoving = false;
                }
                else {
                    transform.position += transform.forward * Time.deltaTime;
                    playFootstepSounds();
                }                
            }
            else if (this.dir == 90)
            {
                if (Physics.SphereCast(transform.position, 0.3f, transform.right, out _, 0.5f))
                {
                    print("There is something in front of the object! R");
                    this.isMoving = false;
                }
                else
                {
                    transform.position += transform.right * Time.deltaTime;
                    playFootstepSounds();
                }
            }
            else if (this.dir == 180)
            {
                if (Physics.SphereCast(transform.position, 0.3f, -transform.forward, out _, 0.5f))
                {
                    print("There is something in front of the object! B");
                    this.isMoving = false;
                }
                else
                {
                    transform.position += -transform.forward * Time.deltaTime;
                    playFootstepSounds();
                }
            }
            else if (this.dir == 270)
            {
                if (Physics.SphereCast(transform.position, 0.3f, -transform.right, out _, 0.5f))
                {
                    print("There is something in front of the object! L");
                    this.isMoving = false;
                }
                else
                {
                    transform.position += -transform.right * Time.deltaTime;
                    playFootstepSounds();
                }
            }

            if ((Vector3.Distance(transform.position, targetPosition) < 0.001f))// && (this.target != null))
            {
                this.isMoving = false;
            }
        }
    }

        private void playFootstepSounds()    {
        if (m_AudioSource.isPlaying == false) {
            m_AudioSource.volume = Random.Range(0.8f, 1f);
            m_AudioSource.pitch = Random.Range(0.8f, 1.1f);
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
        }
    }

    public void cameraUp() {
        if (!this.isMoving && (m_Camera.transform.rotation.x > -0.45f))
        { 
            m_Camera.transform.Rotate(-10, 0, 0);
        } 
    }

    public void cameraDown()
    {
        if (!this.isMoving && (m_Camera.transform.rotation.x < 0.45f))
        {
            m_Camera.transform.Rotate(10, 0, 0);
        }
    }

    /*    private void GetInput(out float speed)
    {
        // speed = m_WalkSpeed;
    }*/


    private void RotateView() 
    {
        // m_MouseLook.LookRotation (transform, m_Camera.transform);
    } 

    public void moveForward(float speed) {
        if (this.isMoving == false) {            
            // zmienne do przesuniêcia kamery i awatara
            float xTarget = this.m_CharacterController.transform.position.x;
            float yTarget = this.m_CharacterController.transform.position.y;
            float zTarget = this.m_CharacterController.transform.position.z;

            // kierunek y kamery
            float y = (float)Math.Round(m_Camera.transform.rotation.eulerAngles.y, 1);

            // reset ustawienia kamery
            m_Camera.transform.rotation = Quaternion.Euler(0, y, 0);

            if (y == 0) // kamera do przodu 0
            {
                zTarget += 1;
                this.dir = 0;
            }
            else if (y == 90) // kamera w prawo 90
            {
                xTarget += 1;
                this.dir = 90;
            }
            else if (y == 180)// kamera do ty³u 180
            {
                zTarget -= 1;
                this.dir = 180;
            }
            else if (y == 270) // kamera w lewo 270
            {
                xTarget -= 1;
                this.dir = 270;
            }

            this.isMoving = true;
            this.targetPosition = new Vector3(xTarget, yTarget, zTarget);
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trap") {
            GameObject btnDisarm = GameObject.Find("ButtonDisarmTrap");
            btnDisarm.GetComponent<UnityEngine.UI.Image>().overrideSprite = this.sprDisarmGreen;
        }
    }


    public void OnTriggerExit(Collider other) {
        if (other.tag == "Trap")
        {
            GameObject btnDisarm = GameObject.Find("ButtonDisarmTrap");
            btnDisarm.GetComponent<UnityEngine.UI.Image>().overrideSprite = this.sprDisarmDefault;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
    }


}

