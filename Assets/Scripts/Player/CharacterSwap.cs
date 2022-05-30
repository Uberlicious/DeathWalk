using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CharacterSwap : MonoBehaviour {

    [SerializeField] CinemachineFreeLook camera;
    [SerializeField] GameObject mainCharacter;
    [SerializeField] Ragdoll mainCharacterRagdoll;
    [SerializeField] Ragdoll ghostRandoll;
    [SerializeField] List<Ghost> ghosts = new List<Ghost>();
    [SerializeField] Effects globalVolume;
    
    Ragdoll _mainCharacterRagdollInstance;
    Ragdoll _ghostRagdollInstance;
    GameObject _activeCharacter;
    ThirdPersonMovement mc_movement;
    Inventory _inventory;
    SceneManage _sceneManage;
    PassThroughParent[] _passThroughParent;
    bool _onRagdoll;

    void Start()
    {
        camera.Follow = mainCharacter.transform;
        camera.LookAt = mainCharacter.transform;
        mc_movement = mainCharacter.GetComponent<ThirdPersonMovement>();
        _inventory = GetComponent<Inventory>();
        _sceneManage = FindObjectOfType<SceneManage>();
        GetPassThroughParents();
        SetGhostsInnactive();
    }

    public void GetPassThroughParents()
    {
        _passThroughParent = FindObjectsOfType<PassThroughParent>();
    }

    void SetGhostsInnactive()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && _inventory.HavePotion)
        {
            if (mainCharacter.activeInHierarchy)
            {
                Ragdoll(null,false);
            }
            else
            {
                SwapBackToMain();
            }
        }
    }

    public void Ragdoll(GameObject character, bool isDead)
    {
        if (mainCharacter.activeInHierarchy)
        {
            mainCharacter.SetActive(false);
            if (_mainCharacterRagdollInstance != null) return;
            _mainCharacterRagdollInstance = Instantiate(mainCharacterRagdoll, mainCharacter.transform.position, mainCharacter.transform.rotation);
            _mainCharacterRagdollInstance.gameObject.transform.position = mainCharacter.transform.position;
            _mainCharacterRagdollInstance.gameObject.transform.rotation = mainCharacter.transform.rotation;
            _mainCharacterRagdollInstance.gameObject.SetActive(true);
            _mainCharacterRagdollInstance.SetRagdollVelocity(mc_movement.OverallVelocity);
            if (!isDead) SwapToGhost();
        }
        else
        {
            if (character == null) return;
            if (_ghostRagdollInstance != null) return;
            character.gameObject.SetActive(false);
            _ghostRagdollInstance = Instantiate(ghostRandoll, character.transform.position, character.transform.rotation);
            _ghostRagdollInstance.gameObject.transform.position = character.transform.position;
            _ghostRagdollInstance.gameObject.transform.rotation = character.transform.rotation;
            _ghostRagdollInstance.gameObject.SetActive(true);
            _mainCharacterRagdollInstance.SetRagdollVelocity(mc_movement.OverallVelocity);
        }

        if (isDead)
        {
            _inventory.RemovePotion();
            _sceneManage.ProcessDeath();
            if (_passThroughParent == null) return;
            ResetTransparencyOnPassThrough();
        }
    }
    
    public void Ragdoll(GameObject character, bool isDead, Vector3 velocity)
    {
        if (mainCharacter.activeInHierarchy)
        {
            mainCharacter.SetActive(false);
            if (_mainCharacterRagdollInstance != null) return;
            _mainCharacterRagdollInstance = Instantiate(mainCharacterRagdoll, mainCharacter.transform.position, mainCharacter.transform.rotation);
            _mainCharacterRagdollInstance.gameObject.transform.position = mainCharacter.transform.position;
            _mainCharacterRagdollInstance.gameObject.transform.rotation = mainCharacter.transform.rotation;
            _mainCharacterRagdollInstance.gameObject.SetActive(true);
            _mainCharacterRagdollInstance.SetRagdollVelocity(velocity);
            if (!isDead) SwapToGhost();
        }
        else
        {
            if (character == null) return;
            if (_ghostRagdollInstance != null) return;
            character.gameObject.SetActive(false);
            _ghostRagdollInstance = Instantiate(ghostRandoll, character.transform.position, character.transform.rotation);
            _ghostRagdollInstance.gameObject.transform.position = character.transform.position;
            _ghostRagdollInstance.gameObject.transform.rotation = character.transform.rotation;
            _ghostRagdollInstance.gameObject.SetActive(true);
            _ghostRagdollInstance.SetRagdollVelocity(velocity);
        }

        if (isDead)
        {
            _inventory.RemovePotion();
            _sceneManage.ProcessDeath();
            if (_passThroughParent == null) return;
            ResetTransparencyOnPassThrough();
        }
    }

    public void SwapBackToMain()
    {
        if (!_onRagdoll) return;
        Debug.Log("OnRagdoll");
        DestroyRagdoll();
        mainCharacter.transform.position = _activeCharacter.gameObject.transform.position;
        mainCharacter.transform.rotation = _activeCharacter.gameObject.transform.rotation;
        mainCharacter.SetActive(true);
        mc_movement.enabled = true;
        camera.Follow = mainCharacter.transform;
        camera.LookAt = mainCharacter.transform;
        _activeCharacter.gameObject.SetActive(false);
        _activeCharacter = mainCharacter;
        _inventory.RemovePotion();
        globalVolume.ToggleGhostEffects(false);
        if (_passThroughParent == null) return;
        ResetTransparencyOnPassThrough();
    }

    void DestroyRagdoll()
    {
        _mainCharacterRagdollInstance.enabled = false;
        Destroy(_mainCharacterRagdollInstance.gameObject);
    }

    void SwapToGhost()
    {
        foreach (Ghost ghost in ghosts)
        {
            if (ghost.IsAvailable)
            {
                ghost.transform.position = mainCharacter.transform.position;
                ghost.transform.rotation = mainCharacter.transform.rotation;
                mc_movement.enabled = false;
                ghost.GetComponent<ThirdPersonMovement>().enabled = true;
                ghost.gameObject.SetActive(true);
                camera.Follow = ghost.transform;
                camera.LookAt = ghost.transform;
                _activeCharacter = ghost.gameObject;
                _inventory.RemovePotion();
                globalVolume.ToggleGhostEffects(true);
                if (_passThroughParent == null) return;
                SetTransparencyOnPassThrough();
            }
        }
    }

    void SetTransparencyOnPassThrough()
    {
        foreach (PassThroughParent parent in _passThroughParent)
        {
            parent.SendTransparent();
        }
    }
    
    void ResetTransparencyOnPassThrough()
    {
        foreach (PassThroughParent parent in _passThroughParent)
        {
            parent.ResetMaterials();
        }
    }

    public void OnRagdoll(bool status)
    {
        _onRagdoll = status;
    }
}
