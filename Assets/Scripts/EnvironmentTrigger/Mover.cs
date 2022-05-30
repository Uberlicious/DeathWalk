using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Mover : MonoBehaviour {
    
    [SerializeField] Vector3 movementAmount;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] bool loopMovement = true;
    [SerializeField] bool runAtAwake = false;
    [SerializeField] bool hasSound = false;
    [SerializeField] bool loopAudio = false;

    AudioSource _audioSource;
    
    void Start()
    {
        if (hasSound)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        if (runAtAwake)
        {
            MoveObject();
        }
    }

    public void MoveObject()
    {
        if (loopMovement)
        {
            LeanTween.move(gameObject, transform.position + movementAmount, movementSpeed).setLoopPingPong();
        }
        else
        {
            LeanTween.move(gameObject, transform.position + movementAmount, movementSpeed);
        }

        if (hasSound)
        {
            if (loopAudio)
            {
                _audioSource.Play();
            }
            else
            {
                StartCoroutine(PlayAudio());
            }
        }
    }

    IEnumerator PlayAudio()
    {
        _audioSource.Play();
        yield return new WaitForSeconds(movementSpeed);
        _audioSource.Stop();
    }
}