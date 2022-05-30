using System;
using UnityEngine;

public class FireTrapFire : MonoBehaviour {

	[SerializeField] float ragdollVelocity = 2f;
	[SerializeField] float trapLeeway = .2f;

	FireTrapTrigger _trapTrigger;
	float _particleActiveTime;

	void Start()
	{
		_trapTrigger = GetComponentInParent<FireTrapTrigger>();
	}
	
	void OnParticleCollision(GameObject other)
	{
		if (Time.time - _trapTrigger.TimeStarted < trapLeeway) return;
		if (!other.gameObject.CompareTag("Player")) return;
		CharacterSwap cs = other.GetComponentInParent<CharacterSwap>();
		cs.Ragdoll(other.gameObject,true, Vector3.up * ragdollVelocity);
	}
}
