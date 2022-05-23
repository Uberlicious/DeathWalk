using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Mover : MonoBehaviour {
    // These variables change our movement speeds.
    [SerializeField] float moveSpeed = 50;
    [SerializeField] [Range(1,10)] float jumpVelocity = 2f;
    [SerializeField] Vector2 turn;

    Rigidbody _rb;
    
    // Start is called before the first frame update
    void Start() {
        PrintInstructions();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        MovePlayer();
        MouseCamera();
        Jump();
    }

    void MouseCamera()
    {
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        transform.localRotation = quaternion.Euler(-turn.y, turn.x, 0);
    }

    void PrintInstructions() {
        Debug.Log("Welcome to the game.\nSuper Fun\nMuch Cool\nVery wow.");
    }

    void MovePlayer() {
        float xValue = (Input.GetAxis("Horizontal") * Time.deltaTime) * moveSpeed;
        float zValue = (Input.GetAxis("Vertical") * Time.deltaTime) * moveSpeed;

        transform.Translate(xValue, 0, zValue);
    }

    void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            _rb.velocity = Vector3.up * jumpVelocity;
        }
    }

}