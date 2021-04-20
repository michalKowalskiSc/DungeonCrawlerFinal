using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    private CharacterController m_CharacterController; 
    private bool moveForward;
    private bool moveBack;
    private bool moveLeft;
    private bool moveRight;
    private float horizontalMove;
    private float verticalMove;


    // Start is called before the first frame update
    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        moveForward = false;
        moveBack = false;
        moveLeft = false;
        moveRight = false;
    }

    // buttons actions
    public void PointerDownForward() {
        moveForward = true;
    }
    public void PointerUpForward() {
        moveForward = false;
    }   
    public void PointerDownBack() {
        moveBack = true;
    }
    public void PointerUpBack() {
        moveBack = false;
    }    
    public void PointerDownLeft() {
        moveLeft = true;
    }
    public void PointerUpLeft() {
        moveLeft = false;
    }    
    public void PointerDownRight() {
        moveRight = true;
    }
    public void PointerUpRight() {
        moveRight = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void MovePlayer() {
        if (moveForward)
        {
            horizontalMove = -speed;
        } else if (moveBack) {
            horizontalMove = speed;
        } else if (moveLeft) {
            verticalMove = -speed;
        } else if (moveRight) {
            verticalMove = speed;
        } else
        {
            horizontalMove = 0;
            verticalMove = 0;
        }
    }

    void FixedUpdate() {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(5, 0.0f, 5);
        //m_CharacterController.AddForce(movement * speed * Time.deltaTime);

    }
}
