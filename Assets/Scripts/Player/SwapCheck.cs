using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCheck : MonoBehaviour {
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			CharacterSwap cs = other.GetComponentInParent<CharacterSwap>();
			cs.OnRagdoll(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			CharacterSwap cs = other.GetComponentInParent<CharacterSwap>();
			cs.OnRagdoll(false);
		}
	}

}
