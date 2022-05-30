using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			CharacterSwap cs = other.GetComponentInParent<CharacterSwap>();
			cs.Ragdoll(other.gameObject,true);
		}
	}
}
