using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] TextMeshPro pickupText;
    [SerializeField] AudioClip pickupSound;
    [SerializeField] Camera mainCamera;
    [Tooltip("Red or Green")][SerializeField] String potionColor;
    
    bool _insideTigger;
    Inventory _inventory;
    Interface _interface;
    CharacterSwap _characterSwap;

    void Start()
    { 
        _inventory = FindObjectOfType<Inventory>();
        _characterSwap = FindObjectOfType<CharacterSwap>();
        _interface = FindObjectOfType<Interface>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _insideTigger)
        {
            bool added = _inventory.AddPotion(potionColor);
            if (added)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        pickupText.enabled = true;
        _insideTigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        pickupText.enabled = false;
        _insideTigger = false;
    }
}
