using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughObject : MonoBehaviour {
	Material _startingMaterial;
	MeshRenderer _meshRenderer;
	
	void Start()
	{
		_meshRenderer = GetComponent<MeshRenderer>();
		if (_meshRenderer == null) return;
		_startingMaterial = _meshRenderer.material;
	}

	void SetTransparent(Material mat)
	{
		_meshRenderer.material = mat;
	}

	void ResetMaterial()
	{
		_meshRenderer.material = _startingMaterial;
	}
}
