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

	public void DisplayDeathText(float time)
	{
		youDied.gameObject.SetActive(true);
		_countdown = time;
		StartCoroutine(UpdateDeathTimer());
	}

	IEnumerator UpdateDeathTimer()
	{
		while (_countdown > 0)
		{
			countdownText.text = $"Reset in: {_countdown}";
			_countdown--;
			yield return new WaitForSeconds(1f);
		}
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
