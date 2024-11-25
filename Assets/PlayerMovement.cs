using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float max_walkspeed;
    public float max_runspeed;
    private float walkSpeed;
    public float runSpeed;
    public float jumpPower;
    public float gravity;
    public float lookSpeed;
    private bool is_attacking;
    public float lookXLimit;
    public float defaultHeight;
    public float crouchHeight;
    public float crouchSpeed;

    public Animator animator;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;
    private bool is_moving;
    public float atk_cooldown;
    private bool can_attack = true;
    public float max_lookspeed;

    void Start()
    {
        walkSpeed = max_walkspeed;
        runSpeed = max_runspeed;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Defining directions
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //creating a bool for running and assigning shift key to it 
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        //creating variables for speed on the x and y axis, dependant on the input from the input manager
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        //assigning movement direction on Y for jumping later
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //walking animation implementation
        if (moveDirection != Vector3.zero && characterController.isGrounded)
        {
            animator.SetBool("is_moving", true);
        }

        else
        {
            is_moving = false;
            animator.SetBool("is_moving", false);
        }

        //running animation for later
        if (isRunning)
        {
            animator.SetBool("is_running", true);
        }
        else
        {
            animator.SetBool("is_running", false);
        }

        //melee attack implementation with mouse input and a coroutine - also, melee attack animation
        if (Input.GetKeyDown(KeyCode.Mouse0) && can_attack)
        {
            animator.SetTrigger("attack");
            StartCoroutine(attack_cast());
        }

        //jumping functionality - adding a float value from jumpPower to the direction on the Y axis
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    public void WingJump()
    {
        jumpPower = 15;
    }

    IEnumerator attack_cast()
    {
        can_attack = false;
        is_attacking = true;
        lookSpeed = 0;
        StartCoroutine(attack_cooldown());
        yield return new WaitForSeconds(0.8f);
        lookSpeed = max_lookspeed;
        is_attacking = false;
    }

    IEnumerator attack_cooldown()
    {
        yield return new WaitForSeconds(atk_cooldown);
        can_attack = true;
    }
}
