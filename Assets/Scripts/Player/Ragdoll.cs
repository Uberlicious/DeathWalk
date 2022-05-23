using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Ragdoll : MonoBehaviour {
	[SerializeField] float extraRagdollVelocity = 1f;

	List<Rigidbody> _rbs = new List<Rigidbody>();
	
	void OnEnable()
	{
		GetRigidbodies(gameObject);
	}

	void GetRigidbodies(GameObject obj)
	{
		if (obj == null) return;
		foreach (Transform child in obj.transform)
		{
			Rigidbody rb = child.GetComponent<Rigidbody>();
			if (rb != null)
			{
				_rbs.Add(rb);
				Debug.Log(rb);
			}
			GetRigidbodies(child.gameObject);
		}
	}

	public void SetRagdollVelocity(Vector3 velocity)
	{
		Debug.Log(velocity);
		foreach (Rigidbody rb in _rbs)
		{
			rb.velocity = velocity * extraRagdollVelocity;
			Debug.Log(rb.velocity);
		}
	}
}
