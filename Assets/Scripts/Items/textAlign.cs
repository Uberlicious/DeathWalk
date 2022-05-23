using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textAlign : MonoBehaviour {

	Camera _mainCamera;
	TextMeshPro _text;
	Vector3 _targetPoint;
	
	void OnEnable()
	{
		_mainCamera = FindObjectOfType<Camera>();
		_text = GetComponent<TextMeshPro>();
		_text.enabled = false;
	}


	void Update()
	{
		if (_mainCamera == null) return;
		transform.LookAt(transform.position - _mainCamera.transform.position, Vector3.up);
		transform.rotation = Quaternion.LookRotation(_mainCamera.transform.forward);
	}
}
