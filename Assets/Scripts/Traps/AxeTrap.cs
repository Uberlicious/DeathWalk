using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrap : MonoBehaviour {

	[SerializeField] float velocityModifier = 300f;
	
	Rigidbody _rb;
	Vector3 velocity;
	Vector3 previousPosition;

	void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		previousPosition = transform.localPosition;
	}

	void LateUpdate()
	{
		velocity = (previousPosition - transform.position) / Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			CharacterSwap cs = other.GetComponentInParent<CharacterSwap>();
			cs.Ragdoll(other.gameObject,true, velocity / 300f);
		}
	}
}
