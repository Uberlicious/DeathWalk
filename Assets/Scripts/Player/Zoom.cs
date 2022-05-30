using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Zoom : MonoBehaviour {
    
    [SerializeField] CinemachineFreeLook freeCamera;
    [SerializeField] float zoomAcceleration = 1f;
    [SerializeField] float defaultZoom = 3f;
    [SerializeField] float minZoom = .1f;
    [SerializeField] float maxZoom = 5f;

    float _currentZoom;
    CinemachineFreeLook.Orbit[] _orbits;

    void Awake()
    {
        _orbits = freeCamera.m_Orbits;
        _currentZoom = defaultZoom;
        SetDefaults();
    }

    void LateUpdate()
    {
        UpdateZoom();
    }

    void SetDefaults()
    {
        freeCamera.m_Orbits[1].m_Radius = defaultZoom;
        freeCamera.m_Orbits[0].m_Height = defaultZoom;
    }
    
    void UpdateZoom()
    {
        var zoom = Input.GetAxis("Mouse ScrollWheel");
        if (zoom != 0)
        {
            var clamp = Mathf.Clamp(_currentZoom -= zoom * zoomAcceleration, minZoom, maxZoom);
            freeCamera.m_Orbits[1].m_Radius = clamp;
            _currentZoom = freeCamera.m_Orbits[1].m_Radius;

            freeCamera.m_Orbits[0].m_Height = freeCamera.m_Orbits[1].m_Radius;
        }
    }
}
