using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour {
	[Header("Game Over Screen")]
	[SerializeField] TextMeshProUGUI youDied;
	[SerializeField] TextMeshProUGUI countdownText;

	[Header("Bottle Pickup")]
	[SerializeField] Image potionImage;
	[SerializeField] Sprite redBottle;
	[SerializeField] Sprite greenBottle;

	float _countdown;

	void Awake()
	{
		int interfaceObj = FindObjectsOfType<Interface>().Length;
		if (interfaceObj > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}
	
	public void DisplayDeathText(float time)
	{
		_countdown = time;
		// StartCoroutine(UpdateDeathTimer());
		ToggleDeathText();
		LeanTween.value(countdownText.gameObject, updateDeathTimerCallback, 5f, 0f, time).setOnComplete(ToggleDeathText);
	}

	void updateDeathTimerCallback(float f, float v)
	{
		countdownText.text = $"Reset in {Mathf.Ceil(f)}";
	}
	
	void ToggleDeathText()
	{
		bool deathText = youDied.gameObject.activeSelf;
		youDied.gameObject.SetActive(!deathText);
	}

	public void ShowBottle(String color)
	{
		if (color == "Green")
		{
			potionImage.sprite = greenBottle;
		}
		else if (color == "Red")
		{
			potionImage.sprite = redBottle;
		}
		potionImage.enabled = true;
	}

	public void HideBottle()
	{
		potionImage.enabled = false;
	}
}
