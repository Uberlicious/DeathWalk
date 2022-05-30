using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Ragdoll : MonoBehaviour {
	[SerializeField] float extraRagdollVelocity = 1f;

	List<Rigidbody> _rbs = new List<Rigidbody>();
	bool _onRagdoll = false;
	public bool OnRagdoll => _onRagdoll;

	void OnEnable()
	{
		if (FindObjectsOfType<Ragdoll>().Length > 2)
		{
			Debug.Log(FindObjectsOfType<Ragdoll>().Length);
			Destroy(transform.parent.gameObject);
		}
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
			}
			GetRigidbodies(child.gameObject);
		}
	}

	public void SetRagdollVelocity(Vector3 velocity)
	{
		foreach (Rigidbody rb in _rbs)
		{
			rb.velocity = velocity * extraRagdollVelocity;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		Debug.Log($"trigger: {other.gameObject}");
		if (other.gameObject.CompareTag("Player"))
		{
			_onRagdoll = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		Debug.Log($"trigger exit: {other.gameObject}");
		if (other.gameObject.CompareTag("Player"))
		{
			_onRagdoll = false;
		}
	}
}
