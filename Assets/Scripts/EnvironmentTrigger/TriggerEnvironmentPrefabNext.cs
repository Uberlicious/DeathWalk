using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class TriggerEnvironmentPrefabNext : MonoBehaviour {
	[SerializeField] GameObject floorToLoad;
	[SerializeField] GameObject previousFloor;
	[SerializeField] bool hidePrevious;
	[SerializeField] int floorIndex;

	SceneManage _sm;

	void Start()
	{
		_sm = FindObjectOfType<SceneManage>();
	}

	void OnTriggerEnter(Collider other)
	{
		bool loadedThisCollision = false;
		if (!other.gameObject.CompareTag("Player") || other.gameObject.layer == 7) return;
		if (_sm.LoadedLevels.Count == floorIndex)
		{
			bool loadSuccess = _sm.LoadNextFloor(floorToLoad);
			if (loadSuccess) loadedThisCollision = true;
		}
		if (hidePrevious)
		{
			_sm.TogglePreviousFloor(floorIndex);
		}
		if (!loadedThisCollision && !hidePrevious)
		{
			_sm.ToggleNextFloor(floorIndex);
		}
	}
}
