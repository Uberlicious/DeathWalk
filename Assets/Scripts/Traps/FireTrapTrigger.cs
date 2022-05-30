using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireTrapTrigger
	: MonoBehaviour {

	[SerializeField] float fireRunTime = 2f;

	float _timeStarted;
	public float TimeStarted => _timeStarted;
	List<ParticleSystem> _particle = new List<ParticleSystem>();
	AudioSource _audioSource;
	
	void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		GetParticleSystems(transform);
		StopFire();
	}

	void GetParticleSystems(Transform parent)
	{
		if (parent == null) return;
		foreach (Transform child in parent)
		{
			ParticleSystem particles = child.GetComponent<ParticleSystem>();
			if (particles != null)
			{
				_particle.Add(particles);
			}
			GetParticleSystems(child);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.CompareTag("Player") && other.gameObject.layer != 7) return;
		StartCoroutine(StartFire());
	}

	IEnumerator StartFire()
	{
		_timeStarted = Time.time;
		_audioSource.Play();
		foreach (ParticleSystem particles in _particle)
		{
			ParticleSystem.EmissionModule em = particles.emission;
			em.enabled = true;
		}
		yield return new WaitForSeconds(fireRunTime);
		StopFire();
	}

	void StopFire()
	{
		foreach (ParticleSystem particles in _particle)
		{
			ParticleSystem.EmissionModule em = particles.emission;
			em.enabled = false;
		}
		_audioSource.Stop();
	}
}
