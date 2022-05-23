using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour {
	[SerializeField] GameObject chestLid;
	[SerializeField] TextMeshPro hoverText;
	[SerializeField] GameObject item;

	bool _insideTrigger;
	bool _lidClosed = true;
	AudioSource _audioSource;

	void Start()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && _lidClosed && _insideTrigger)
		{
			OpenChest();
		}
	}

	void OpenChest()
	{
		hoverText.gameObject.SetActive(false);
		_audioSource.Play();
		LeanTween.rotateX(chestLid, -90f, 1f);
		_lidClosed = false;
		FloatItem();
	}

	void FloatItem()
	{
		item.SetActive(true);
		LTSeq seq = LeanTween.sequence();
		seq.append(LeanTween.moveY(item, 0.7f, 1f));
		item.GetComponent<SphereCollider>().enabled = true;
		seq.append(LeanTween.moveY(item, 0.5f, 1f).setLoopType(LeanTweenType.pingPong));
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.CompareTag("Player")) return;
		hoverText.enabled = true;
		_insideTrigger = true;
	}

	void OnTriggerExit(Collider other)
	{
		if (!other.gameObject.CompareTag("Player")) return;
		hoverText.enabled = false;
		_insideTrigger = false;
	}
}
