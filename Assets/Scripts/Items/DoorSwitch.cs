using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorSwitch : MonoBehaviour {

    [Header("Connected Door Options")]
    [SerializeField] GameObject connectedDoor;
    [Tooltip("Used if door does not have mover script")]
    [SerializeField] float rotateTo;
    [Tooltip("Used if door does not have mover script")]
    [SerializeField] float rotateSpeed;

    [Header("Button Options")]
    [SerializeField] TextMeshPro hoverText;
    [SerializeField] float buttonPushDistance = -.04f;
    [SerializeField] float buttonPushSpeed = .1f;
    [SerializeField] bool isPushButton = true;
    [SerializeField] bool isLever;
    [SerializeField] float leverSpeed;

    bool _nearSwitch;
    AudioSource _doorAudio;
    Mover _doorMover;
    bool _switchUsed;

    void Start()
    {
        _doorAudio = connectedDoor.GetComponent<AudioSource>();
        _doorMover = connectedDoor.GetComponent<Mover>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _nearSwitch && !_switchUsed)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        ButtonAnimation();
        if (!_doorMover)
        {
            LeanTween.rotateY(connectedDoor, connectedDoor.transform.localRotation.y + rotateTo, rotateSpeed);
        }
        if (_doorMover)
        {
            _doorMover.MoveObject();
        }
        _switchUsed = true;
        if (_doorAudio == null) return;
        _doorAudio.Play();
    }


    void ButtonAnimation()
    {
        if (isPushButton)
        {
            LeanTween.moveLocalZ(gameObject, buttonPushDistance, buttonPushSpeed).setLoopPingPong(1);
        }
        if (isLever)
        {
            LeanTween.rotateX(gameObject, transform.localRotation.eulerAngles.x + 90f, leverSpeed);
        }
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _nearSwitch = true;
        if (hoverText == null) return;
        hoverText.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _nearSwitch = false;
        if (hoverText == null) return;
        hoverText.enabled = false;
    }
}
