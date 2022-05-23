using System;
using UnityEngine;

public class Ghost : MonoBehaviour {

	bool _isAvailable = true;
	public bool IsAvailable => _isAvailable;

	void Awake()
	{
		this.gameObject.SetActive(false);
	}

	public void SetUnavailable()
	{
		_isAvailable = false;
	}
}

