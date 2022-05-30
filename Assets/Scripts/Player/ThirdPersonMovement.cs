using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour {

    [Header("General")]
    public CharacterController controller;
    public Transform mainCamera;
    public Transform groundCheck;
    public Animator animator = null;
    public bool isDead = false;
    
    [Header("Movement")]
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    [Header("Jumping")]
    public bool canJump = true;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public int jumpLeeway = 3;

    Vector3 _velocity;
    Vector3 _overallVelocity;
    public Vector3 OverallVelocity => _overallVelocity;
    Vector3 _moveDir;
    bool _isGrounded = true;
    bool _canMove;
    bool jumpCheck;
    int jumpTimer = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnEnable()
    {
        animator.SetBool("Grounded", true);
    }

    void OnDisable()
    {
        _canMove = false;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        jumpCheck = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        switch (jumpCheck)
        {
            case true:
                _isGrounded = true;
                jumpTimer = 0;
                break;
            
            case false:
            {
                if (jumpTimer >= jumpLeeway)
                {
                    _isGrounded = false;
                }
                jumpTimer++;
                break;
            }
        }

        if (!_canMove && _isGrounded) _canMove = _isGrounded;
        animator.SetBool("Grounded", _isGrounded);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float horizontal = _canMove ? Input.GetAxisRaw("Horizontal") : 0f;
        float vertical = _canMove ? Input.GetAxisRaw("Vertical") : 0f;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            _moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _overallVelocity = _moveDir.normalized;
            controller.Move(_moveDir.normalized * (speed * Time.deltaTime));
            animator.SetFloat("MoveSpeed", direction.magnitude);
        }
        else
        {
            animator.SetFloat("MoveSpeed", 0f);
        }

        if (Input.GetButtonDown("Jump") && _isGrounded && canJump)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            _isGrounded = false;
            animator.SetBool("Grounded", _isGrounded);
        }

        _overallVelocity = _moveDir + new Vector3(0f, _velocity.y);
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }
}
